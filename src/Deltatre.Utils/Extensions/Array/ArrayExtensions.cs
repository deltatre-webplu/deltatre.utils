using System;
using System.Linq;
using Deltatre.Utils.Randomization;

namespace Deltatre.Utils.Extensions.Array
{
	/// <summary>
	/// Extension methods for arrays
	/// </summary>
	public static class ArrayExtensions
	{
		/// <summary>
		/// Call this method to get a randomic permutation of a given array
		/// </summary>
		/// <typeparam name="T">The type of elements contained in the array to be shuffled</typeparam>
		/// <param name="source">The array to be shuffled</param>
		/// <returns>A randomic permutation of the source array. Each possible permutation has the exact same likelihood</returns>
		public static T[] Shuffle<T>(this T[] source)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			if (source.Length == 0 || source.Length == 1)
				return source.ToArray();

			var result = source.ToArray();

			for (var i = result.Length - 1; i >= 1; i--)
			{
				var j = RandomGenerator.Instance.Next(0, i);
				var temp = result[j];
				result[j] = result[i];
				result[i] = temp;
			}

			return result;
		}
	}
}
