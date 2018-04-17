using System;
using System.Collections.Generic;
using System.Linq;

namespace Deltatre.Utils.Extensions.Enumerable
{
	public static class EnumerableExtensions
	{
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
		{
			if (source == null)
				return true;

			return !source.Any();
		}

		/// <exception cref="System.ArgumentNullException"> Throws ArgumenNullException when parameter source is null </exception>
		/// <exception cref="System.ArgumentNullException"> Throws ArgumenNullException when parameter toBeDone is null </exception>
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> toBeDone)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			if (toBeDone == null)
				throw new ArgumentNullException(nameof(toBeDone));

			foreach (var item in source)
				toBeDone(item);
		}
	}
}

