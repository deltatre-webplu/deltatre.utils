using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Linq;
using System.IO;

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
		/// <param name="maxDegreeOfParallelism">The maximum number of operations that are able to run in parallel</param>
		/// <param name="operation">The asynchronous operation to be executed for each item inside <paramref name="source"/></param>
		/// <returns>A task which completes when all of the asynchronous operations (one for each item inside <paramref name="source"/>) complete</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="operation"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="maxDegreeOfParallelism"/> is less than or equal to zero.</exception>
		public static Task ForEachAsync<T>(
			this IEnumerable<T> source,
			int maxDegreeOfParallelism,
			Func<T, Task> operation)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			if (operation == null)
				throw new ArgumentNullException(nameof(operation));

			if (maxDegreeOfParallelism <= 0)
			{
				throw new ArgumentOutOfRangeException(
					nameof(maxDegreeOfParallelism),
					$"Invalid value for the maximum degree of parallelism: {maxDegreeOfParallelism}. The maximum degree of parallelism must be a positive integer.");
			}

			var tasks = from partition in Partitioner.Create(source).GetPartitions(maxDegreeOfParallelism)
									select Task.Run(async () =>
									{
										using (partition)
										{
											while (partition.MoveNext())
											{
												await operation(partition.Current).ConfigureAwait(false);
											}
										}
									});
			return Task.WhenAll(tasks);
		}

		/// <summary>
		/// Executes an asynchronous operation for each item inside a source sequence. These operations are run concurrently in a parallel fashion. The invokation returns a task whose result is a sequence containing the results of all the asynchronous operations (in source sequence order). It is possible to constrain the maximum number of parallel operations.
		/// </summary>
		/// <typeparam name="TSource">The type of the items inside the source sequence</typeparam>
		/// <typeparam name="TResult">The type of the object produced by invoking <paramref name="operation"/> on any item of <paramref name="source"/></typeparam>
		/// <param name="source">The source sequence</param>
		/// <param name="maxDegreeOfParallelism">The maximum number of operations that are able to run in parallel</param>
		/// <param name="operation">The asynchronous operation to be executed for each item inside <paramref name="source"/>. This operation will produce a result of type <typeparamref name="TResult"/></param>
		/// <returns>A task which completes when all of the asynchronous operations (one for each item inside <paramref name="source"/>) complete. This task will produce a sequence of objects of type <typeparamref name="TResult"/> which are the results (in source sequence order) of applying <paramref name="operation"/> to all items in <paramref name="source"/></returns> 
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="operation"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="maxDegreeOfParallelism"/> is less than or equal to zero.</exception>
		public static async Task<IEnumerable<TResult>> ForEachAsync<TSource, TResult>(
			this IEnumerable<TSource> source,
			int maxDegreeOfParallelism,
			Func<TSource, Task<TResult>> operation)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			if (operation == null)
				throw new ArgumentNullException(nameof(operation));

			if (maxDegreeOfParallelism <= 0)
			{
				throw new ArgumentOutOfRangeException(
					nameof(maxDegreeOfParallelism),
					$"Invalid value for the maximum degree of parallelism: {maxDegreeOfParallelism}. The maximum degree of parallelism must be a positive integer.");
			}

			var resultsByPositionInSource = new ConcurrentDictionary<long, TResult>();

			var tasks = from partition in Partitioner.Create(source).GetOrderablePartitions(maxDegreeOfParallelism)
									select Task.Run(async () =>
									{
										using (partition)
										{
											while (partition.MoveNext())
											{
												var positionInSource = partition.Current.Key;
												var item = partition.Current.Value;

												var result = await operation(item).ConfigureAwait(false);

												resultsByPositionInSource.TryAdd(positionInSource, result);
											}
										}
									});
			await Task.WhenAll(tasks).ConfigureAwait(false);

			return Enumerable.Range(0, resultsByPositionInSource.Count)
				.Select(position => resultsByPositionInSource[position]);
		}
	}
}
