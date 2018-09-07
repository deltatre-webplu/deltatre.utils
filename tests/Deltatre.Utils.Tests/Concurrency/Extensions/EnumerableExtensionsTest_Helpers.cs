using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deltatre.Utils.Tests.Concurrency.Extensions
{
	public partial class EnumerableExtensionsTest
	{
		private static bool AreOverlapping(
			(DateTime start, DateTime end) first,
			(DateTime start, DateTime end) second)
		{
			return first.end > second.start && second.end > first.start;
		}

		private static T[] GetOthers<T>(T[] items, int index)
		{
			var numberOfElementsBefore = index;
			var itemsToSkip = index + 1;

			return items.Take(numberOfElementsBefore).Concat(items.Skip(itemsToSkip)).ToArray();
		}
	}
}
