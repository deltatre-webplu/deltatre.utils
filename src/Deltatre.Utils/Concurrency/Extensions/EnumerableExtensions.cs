using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace Deltatre.Utils.Concurrency.Extensions
{
	/// <summary>
	/// Extension methods for enumerables
	/// </summary>
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Executes an asynchronous operation for each item inside a source sequence. These operations are run concurrently in a parallel fashion. The invokation returns a task which completes when all of the asynchronous operations (one for each item inside the source sequence) complete. It is possible to constrain the maximum number of parallel operations.
		/// </summary>
		/// <typeparam name="T">The type of the items inside <paramref name="source"/></typeparam>
		/// <param name="source">The source sequence</param>
		/// <param name="operation">The asynchronous operation to be executed for each item inside <paramref name="source"/></param>
		/// <param name="maxDegreeOfParallelism">The maximum number of operations that are able to run in parallel. If null, no limits will be set for the maximum number of parallel operations (same behaviour as Task.WhenAll)</param>
		/// <returns>A task which completes when all of the asynchronous operations (one for each item inside <paramref name="source"/>) complete</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="operation"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="maxDegreeOfParallelism"/> is less than or equal to zero.</exception>
		public static Task ForEachAsync<T>(
			this IEnumerable<T> source,
			Func<T, Task> operation,
			int? maxDegreeOfParallelism = null)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			if (operation == null)
				throw new ArgumentNullException(nameof(operation));

			EnsureValidMaxDegreeOfParallelism(maxDegreeOfParallelism);

			return (maxDegreeOfParallelism == null)
				? ApplyOperationToAllItems(source, operation)
				: ApplyOperationToAllItemsWithConstrainedParallelism(source, operation, maxDegreeOfParallelism.Value);
		}

		private static Task ApplyOperationToAllItems<T>(
			IEnumerable<T> items,
			Func<T, Task> operation)
		{
			var tasks = items.Select(operation);
			return Task.WhenAll(tasks);
		}

		private static async Task ApplyOperationToAllItemsWithConstrainedParallelism<T>(
			IEnumerable<T> items,
			Func<T, Task> operation, 
			int maxDegreeOfParallelism)
		{
			using (var throttler = new SemaphoreSlim(maxDegreeOfParallelism))
			{
				var tasks = new List<Task>();

				foreach (var item in items)
				{
					await throttler.WaitAsync().ConfigureAwait(false);

#pragma warning disable IDE0039 // Use local function
					Func<Task> bodyOfNewTask = async () =>
#pragma warning restore IDE0039 // Use local function
					{
						try
						{
							await operation(item).ConfigureAwait(false);
						}
						finally
						{
							throttler.Release();
						}
					};

					tasks.Add(Task.Run(bodyOfNewTask));
				}

				await Task.WhenAll(tasks).ConfigureAwait(false);
			}
		}

		/// <summary>
		/// Executes an asynchronous operation for each item inside a source sequence. These operations are run concurrently in a parallel fashion. The invokation returns a task whose result is a sequence containing the results of all the asynchronous operations (in source sequence order). It is possible to constrain the maximum number of parallel operations.
		/// </summary>
		/// <typeparam name="TSource">The type of the items inside the source sequence</typeparam>
		/// <typeparam name="TResult">The type of the object produced by invoking <paramref name="operation"/> on any item of <paramref name="source"/></typeparam>
		/// <param name="source">The source sequence</param>
		/// <param name="operation">The asynchronous operation to be executed for each item inside <paramref name="source"/>. This operation will produce a result of type <typeparamref name="TResult"/></param>
		/// <param name="maxDegreeOfParallelism">The maximum number of operations that are able to run in parallel. If null, no limits will be set for the maximum number of parallel operations (same behaviour as Task.WhenAll)</param>
		/// <returns>A task which completes when all of the asynchronous operations (one for each item inside <paramref name="source"/>) complete. This task will produce a sequence of objects of type <typeparamref name="TResult"/> which are the results (in source sequence order) of applying <paramref name="operation"/> to all items in <paramref name="source"/></returns> 
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="operation"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="maxDegreeOfParallelism"/> is less than or equal to zero.</exception>
		public static Task<TResult[]> ForEachAsync<TSource, TResult>(
			this IEnumerable<TSource> source,
			Func<TSource, Task<TResult>> operation,
			int? maxDegreeOfParallelism = null)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			if (operation == null)
				throw new ArgumentNullException(nameof(operation));

			EnsureValidMaxDegreeOfParallelism(maxDegreeOfParallelism);

			return (maxDegreeOfParallelism == null)
				? ApplyOperationToAllItems(source, operation)
				: ApplyOperationToAllItemsWithConstrainedParallelism(source, operation, maxDegreeOfParallelism.Value);
		}

		private static Task<TResult[]> ApplyOperationToAllItems<TItem, TResult>(
			IEnumerable<TItem> items,
			Func<TItem, Task<TResult>> operation)
		{
			var tasks = items.Select(operation);
			return Task.WhenAll(tasks);
		}

		private static async Task<TResult[]> ApplyOperationToAllItemsWithConstrainedParallelism<TItem, TResult>(
			IEnumerable<TItem> items,
			Func<TItem, Task<TResult>> operation,
			int maxDegreeOfParallelism)
		{
			var resultsByPositionInSource = new ConcurrentDictionary<long, TResult>();

			using (var throttler = new SemaphoreSlim(maxDegreeOfParallelism))
			{
				var tasks = new List<Task>();

				foreach (var itemWithIndex in items.Select((item, index) => new { item, index }))
				{
					await throttler.WaitAsync().ConfigureAwait(false);

#pragma warning disable IDE0039 // Use local function
					Func<Task> bodyOfNewTask = async () =>
#pragma warning restore IDE0039 // Use local function
					{
						try
						{
							var item = itemWithIndex.item;
							var positionInSource = itemWithIndex.index;

							var result = await operation(item).ConfigureAwait(false);

							resultsByPositionInSource.TryAdd(positionInSource, result);
						}
						finally
						{
							throttler.Release();
						}
					};

					tasks.Add(Task.Run(bodyOfNewTask));
				}

				await Task.WhenAll(tasks).ConfigureAwait(false);
			}

			return Enumerable
				.Range(0, resultsByPositionInSource.Count)
				.Select(position => resultsByPositionInSource[position])
				.ToArray();
		}

		private static void EnsureValidMaxDegreeOfParallelism(int? maxDegreeOfParallelism)
		{
			if (maxDegreeOfParallelism <= 0)
			{
				throw new ArgumentOutOfRangeException(
					nameof(maxDegreeOfParallelism),
					$"Invalid value for the maximum degree of parallelism: {maxDegreeOfParallelism}. The maximum degree of parallelism must be a positive integer.");
			}
		}
	}
}
