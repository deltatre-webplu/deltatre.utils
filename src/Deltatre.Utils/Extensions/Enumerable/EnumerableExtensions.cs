using System;
using System.Collections.Generic;
using System.Linq;
using Deltatre.Utils.Types;

namespace Deltatre.Utils.Extensions.Enumerable
{
	/// <summary>
	/// Extension methods for enumerables
	/// </summary>
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Checks whether a sequence is null or empty
		/// </summary>
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
		{
			if (source == null)
				return true;

			return !source.Any();
		}

    /// <summary>
    /// Performs the specified action on each element of the provided <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <param name="source">The sequence of items on which to execute the specified action</param>
    /// <param name="toBeDone">The action to be executed for each item in <paramref name="source"/></param>
    /// <exception cref="ArgumentNullException"> Throws ArgumenNullException when parameter source is null </exception>
    /// <exception cref="ArgumentNullException"> Throws ArgumenNullException when parameter toBeDone is null </exception>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> toBeDone)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			if (toBeDone == null)
				throw new ArgumentNullException(nameof(toBeDone));

			foreach (var item in source)
				toBeDone(item);
		}

		/// <summary>
		/// Checks whether a sequence of items contains duplicate elements.
		/// </summary>
    /// <param name="source">The sequence of items to be checked for duplicates.</param>
    /// <param name="comparer">The equality comparer to be used when checking for duplicates in <paramref name="source"/>. Pass null if you want to use the default equality comparer for type <typeparamref name="T"/>.</param>
    /// <returns>A boolean value indicating whether or not <paramref name="source"/> contains duplicate elements.</returns>
		/// <exception cref="ArgumentNullException">Throws ArgumenNullException when parameter source is null</exception>
		/// <remarks> Use default equality comparer if parameter comparer is not specified </remarks>
		public static bool HasDuplicates<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			var equalityComparer = comparer ?? EqualityComparer<T>.Default;

			var set = new HashSet<T>(equalityComparer);

			var hasDuplicates = !source.All(set.Add);
			return hasDuplicates;
		}

		/// <summary>
		/// Creates an instance of <see cref="NonEmptySequence{T}"/> containing the items of the provided sequence.
		/// </summary>
    /// <param name="source">The sequence of items to be used to create an instance of <see cref="NonEmptySequence{T}"/>.</param>
    /// <typeparam name="T">The type of the items in <paramref name="source"/></typeparam>
    /// <returns>An instance of <see cref="NonEmptySequence{T}"/> containing the items of <paramref name="source"/>.</returns>
		/// <exception cref="ArgumentNullException">Throws <see cref="ArgumentNullException"/> when <paramref name="source"/> is null</exception>
		public static NonEmptySequence<T> ToNonEmptySequence<T>(this IEnumerable<T> source)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			return new NonEmptySequence<T>(source);
		}

    /// <summary>
    /// Creates an instance of <see cref="NonEmptyList{T}"/> containing the items of the provided sequence.
    /// </summary>
    /// <typeparam name="T">The type of the items in <paramref name="source"/></typeparam>
    /// <param name="source">The sequence of items to be used to create an instance of <see cref="NonEmptyList{T}"/>.</param>
    /// <returns>An instance of <see cref="NonEmptyList{T}"/> containing the items of <paramref name="source"/>.</returns>
    /// <exception cref="ArgumentNullException">Throws <see cref="ArgumentNullException"/> when <paramref name="source"/> is null.</exception>
    public static NonEmptyList<T> ToNonEmptyList<T>(this IEnumerable<T> source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(source));

      return new NonEmptyList<T>(source);
    }

    /// <summary>
    /// Creates an hash set from a sequence of items. The default equality comparer for the type argument will be used.
    /// </summary>
    /// <typeparam name="T">The type of the items inside the starting sequence</typeparam>
    /// <param name="source">The starting sequence</param>
    /// <returns>An hash set containing the unique elements of the starting sequence</returns>
    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source) => 
			source.ToHashSet(EqualityComparer<T>.Default);

		/// <summary>
		/// Creates an hash set from a sequence of items.
		/// </summary>
		/// <typeparam name="T">The type of the items inside the starting sequence</typeparam>
		/// <param name="source">The starting sequence</param>
		/// <param name="comparer">The equality comparer used by the hash set to find the unique elements inside the starting sequence</param>
		/// <returns>An hash set containing the unique elements of the starting sequence</returns>
		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer) => 
			new HashSet<T>(source, comparer);

		/// <summary>
		/// Split a sequence in batches of fixed size
		/// </summary>
		/// <typeparam name="T">The type of the items inside the starting sequence</typeparam>
		/// <param name="source">The starting sequence</param>
		/// <param name="batchSize">The size of batches to be obtained by splitting the starting sequence</param>
		/// <returns>A sequence containing the batches obtained by splitting the starting sequence</returns>
		/// <exception cref="ArgumentNullException">Throws ArgumentNullException when parameter source is null</exception>
		/// <exception cref="ArgumentOutOfRangeException">Throws ArgumentOutOfRangeException when parameter batchSize is less than or equal to zero</exception>
		public static IEnumerable<IEnumerable<T>> SplitInBatches<T>(
			this IEnumerable<T> source,
			int batchSize)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			if(batchSize <= 0)
      {
        throw new ArgumentOutOfRangeException(
					nameof(batchSize),
					$"Batch size is expected to be a positive integer, but was {batchSize}.");
      }

      return SplitInBatchesImplementation(source, batchSize);
		}

		private static IEnumerable<IEnumerable<T>> SplitInBatchesImplementation<T>(
			IEnumerable<T> source,
			int batchSize)
		{
			var batch = new List<T>(batchSize);

			foreach (var item in source)
			{
				batch.Add(item);
				if (batch.Count == batchSize)
				{
					yield return batch;
					batch = new List<T>(batchSize);
				}
			}

			if (batch.Count > 0)
				yield return batch;
		}
	}
}

