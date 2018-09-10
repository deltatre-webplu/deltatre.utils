using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Deltatre.Utils.Concurrency.Extensions;
using System.Collections.Concurrent;
using System.Threading;
using System.Linq;

namespace Deltatre.Utils.Tests.Concurrency.Extensions
{
	[TestFixture]
	public partial class EnumerableExtensionsTest
	{
		[Test]
		public void ForEachAsync_Throws_ArgumentNullException_When_Source_Is_Null()
		{
			// ARRANGE
			const int maxDegreeOfParallelism = 4;
			Func<string, Task> operation = _ => Task.FromResult(true);

			// ACT
			Assert.ThrowsAsync<ArgumentNullException>(() => 
				EnumerableExtensions.ForEachAsync(null, operation, maxDegreeOfParallelism));
		}

		[Test]
		public void ForEachAsync_Throws_ArgumentNullException_When_Operation_Is_Null()
		{
			// ARRANGE
			const int maxDegreeOfParallelism = 4;
			var source = new[] { "hello", "world" };

			// ACT
			Assert.ThrowsAsync<ArgumentNullException>(() =>
				source.ForEachAsync(null, maxDegreeOfParallelism));
		}

		[Test]
		public void ForEachAsync_Throws_ArgumentOutOfRangeException_When_MaxDegreeOfParallelism_Is_Less_Than_Zero()
		{
			// ARRANGE
			const int maxDegreeOfParallelism = -3;
			var source = new[] { "hello", "world" };
			Func<string, Task> operation = _ => Task.FromResult(true);

			// ACT
			Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
				source.ForEachAsync(operation, maxDegreeOfParallelism));
		}

		[Test]
		public void ForEachAsync_Throws_ArgumentOutOfRangeException_When_MaxDegreeOfParallelism_Is_Equals_Zero()
		{
			// ARRANGE
			const int maxDegreeOfParallelism = 0;
			var source = new[] { "hello", "world" };
			Func<string, Task> operation = _ => Task.FromResult(true);

			// ACT
			Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
				source.ForEachAsync(operation, maxDegreeOfParallelism));
		}

		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task ForEachAsync_Executes_The_Operation_For_Each_Item_Inside_Source_Collection(int maxDegreeOfParallelism)
		{
			// ARRANGE
			var source = new[] { "foo", "bar", "buzz" };

			var processedItems = new ConcurrentBag<string>();
			Func<string, Task> operation = item => 
			{
				processedItems.Add(item);
				return Task.FromResult(true);
			};

			// ACT 
			await source.ForEachAsync(operation, maxDegreeOfParallelism).ConfigureAwait(false);

			// ASSERT
			CollectionAssert.AreEquivalent(new[] { "foo", "bar", "buzz" }, processedItems);
		}

		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task ForEachAsync_Executes_A_Number_Of_Concurrent_Operations_Less_Than_Or_Equal_To_MaxDegreeOfParallelism(int maxDegreeOfParallelism)
		{
			// ARRANGE
			var source = new[] { "foo", "bar", "buzz" };

			var timeRanges = new ConcurrentBag<(DateTime start, DateTime end)>();

			Func<string, Task> operation = async _ =>
			{
				var start = DateTime.Now;

				await Task.Delay(500).ConfigureAwait(false);

				timeRanges.Add((start, DateTime.Now));
			};

			// ACT 
			await source.ForEachAsync(operation, maxDegreeOfParallelism).ConfigureAwait(false);

			// ASSERT
			var timeRangesArray = timeRanges.ToArray();

			for (int i = 0; i < timeRanges.Count; i++)
			{
				var current = timeRangesArray[i];
				var others = GetOthers(timeRangesArray, i);
				var overlaps = 0;

				foreach (var item in others)
				{
					if (AreOverlapping(current, item))
					{
						overlaps++;
					}
				}

				Assert.IsTrue(overlaps <= maxDegreeOfParallelism);
			}
		}
	}
}
