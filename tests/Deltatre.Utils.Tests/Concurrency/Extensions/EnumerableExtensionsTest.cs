using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Deltatre.Utils.Concurrency.Extensions;
using System.Collections.Concurrent;

namespace Deltatre.Utils.Tests.Concurrency.Extensions
{
	[TestFixture]
	public class EnumerableExtensionsTest
	{
		[Test]
		public void ForEachAsync_Throws_ArgumentNullException_When_Source_Is_Null()
		{
			// ARRANGE
			const int maxDegreeOfParallelism = 4;
			Func<string, Task> operation = _ => Task.FromResult(true);

			// ACT
			Assert.ThrowsAsync<ArgumentNullException>(() => 
				EnumerableExtensions.ForEachAsync(null, maxDegreeOfParallelism, operation));
		}

		[Test]
		public void ForEachAsync_Throws_ArgumentNullException_When_Operation_Is_Null()
		{
			// ARRANGE
			const int maxDegreeOfParallelism = 4;
			var source = new[] { "hello", "world" };

			// ACT
			Assert.ThrowsAsync<ArgumentNullException>(() =>
				source.ForEachAsync(maxDegreeOfParallelism, null));
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
				source.ForEachAsync(maxDegreeOfParallelism, operation));
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
				source.ForEachAsync(maxDegreeOfParallelism, operation));
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
			await source.ForEachAsync(maxDegreeOfParallelism, operation).ConfigureAwait(false);

			// ASSERT
			CollectionAssert.AreEquivalent(new[] { "foo", "bar", "buzz" }, processedItems);
		}
	}
}
