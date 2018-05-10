using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Deltatre.Utils.Dto;

namespace Deltatre.Utils.Functional
{
	public static class Helpers
	{
		/// <summary>
		/// This helper retrieves a projection of the first item of a sequence which satisfies a predicate.
		/// </summary>
		/// <typeparam name="TSource">The type of items inside the source sequence</typeparam>
		/// <typeparam name="TResult">The type of the projection being retrieved from the sequence</typeparam>
		/// <param name="source">The source from which the projection is retrieved</param>
		/// <param name="predicate">The predicate that must be satisfied</param>
		/// <param name="projector"></param>
		/// <returns>The result of the get operation being performed</returns>
		public static GetItemResult<TResult> GetFirst<TSource, TResult>(
			IEnumerable<TSource> source, 
			Func<TSource, bool> predicate,
			Func<TSource, TResult> projector)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			if (predicate == null)
				throw new ArgumentNullException(nameof(predicate));

			if (projector == null)
				throw new ArgumentNullException(nameof(projector));

			var firstMatch = source.FirstOrDefault(predicate);

			var notFound = EqualityComparer<TSource>.Default.Equals(firstMatch, default(TSource));
			if (notFound)
			{
				return GetItemResult<TResult>.CreateForItemNotFound();
			}

			var projection = projector(firstMatch);
			return GetItemResult<TResult>.CreateForItemFound(projection);
		}
	}
}
