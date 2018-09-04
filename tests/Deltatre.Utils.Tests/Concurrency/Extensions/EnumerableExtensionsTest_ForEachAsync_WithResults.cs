using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deltatre.Utils.Concurrency.Extensions;
using System.Collections.Concurrent;
using System.Threading;

namespace Deltatre.Utils.Tests.Concurrency.Extensions
{
	public partial class EnumerableExtensionsTest
	{
		[Test]
		public void ForEachAsync_WithResults_WithResults_Throws_ArgumentNullException_When_Source_Is_Null()
		{
			// ARRANGE
			const int maxDegreeOfParallelism = 4;
			Func<string, Task<bool>> operation = _ => Task.FromResult(true);

			// ACT
			Assert.ThrowsAsync<ArgumentNullException>(() =>
				EnumerableExtensions.ForEachAsync(null, maxDegreeOfParallelism, operation));
		}

		[Test]
		public void ForEachAsync_WithResults_WithResults_Throws_ArgumentNullException_When_Operation_Is_Null()
		{
			// ARRANGE
			const int maxDegreeOfParallelism = 4;
			var source = new[] { "hello", "world" };
			Func<string, Task<bool>> operation = null;

			// ACT
			Assert.ThrowsAsync<ArgumentNullException>(() =>
				source.ForEachAsync(maxDegreeOfParallelism, operation));
		}

		[Test]
		public void ForEachAsync_WithResults_WithResults_Throws_ArgumentOutOfRangeException_When_MaxDegreeOfParallelism_Is_Less_Than_Zero()
		{
			// ARRANGE
			const int maxDegreeOfParallelism = -3;
			var source = new[] { "hello", "world" };
			Func<string, Task<bool>> operation = _ => Task.FromResult(true);

			// ACT
			Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
				source.ForEachAsync(maxDegreeOfParallelism, operation));
		}

		[Test]
		public void ForEachAsync_WithResults_WithResults_Throws_ArgumentOutOfRangeException_When_MaxDegreeOfParallelism_Is_Equals_Zero()
		{
			// ARRANGE
			const int maxDegreeOfParallelism = 0;
			var source = new[] { "hello", "world" };
			Func<string, Task<bool>> operation = _ => Task.FromResult(true);

			// ACT
			Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
				source.ForEachAsync(maxDegreeOfParallelism, operation));
		}

		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task ForEachAsync_WithResults_Executes_The_Operation_For_Each_Item_Inside_Source_Collection(int maxDegreeOfParallelism)
		{
			// ARRANGE
			var source = new[] { "foo", "bar", "buzz" };

			var processedItems = new ConcurrentBag<string>();
			Func<string, Task<string>> operation = item =>
			{
				processedItems.Add(item);
				return Task.FromResult(item);
			};

			// ACT 
			await source.ForEachAsync(maxDegreeOfParallelism, operation).ConfigureAwait(false);

			// ASSERT
			CollectionAssert.AreEquivalent(new[] { "foo", "bar", "buzz" }, processedItems);
		}

		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task ForEachAsync_WithResults_Executes_A_Number_Of_Concurrent_Operations_Less_Than_Or_Equal_To_MaxDegreeOfParallelism(int maxDegreeOfParallelism)
		{
			// ARRANGE
			var source = new[] { "foo", "bar", "buzz" };

			var threadIds = new ConcurrentDictionary<int, byte>();

			Func<string, Task<string>> operation = item =>
			{
				var threadId = Thread.CurrentThread.ManagedThreadId;
				threadIds.TryAdd(threadId, 0);
				return Task.FromResult(item);
			};

			// ACT 
			await source.ForEachAsync(maxDegreeOfParallelism, operation).ConfigureAwait(false);

			// ASSERT
			Assert.IsTrue(threadIds.Count <= maxDegreeOfParallelism);
		}

		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task ForEachAsync_WithResults_Produces_Result_For_Each_Item_Inside_Source_Collection(int maxDegreeOfParallelism)
		{
			// ARRANGE
			var source = new[] { "foo", "bar", "buzz" };

			Func<string, Task<string>> operation = item => Task.FromResult(item.ToUpperInvariant());

			// ACT 
		 	var results = await source.ForEachAsync(maxDegreeOfParallelism, operation).ConfigureAwait(false);

			// ASSERT
			CollectionAssert.AreEquivalent(new[] { "FOO", "BAR", "BUZZ" }, results);
		}

		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		public async Task ForEachAsync_WithResults_Produces_Results_In_Source_Order(int maxDegreeOfParallelism)
		{
			// ARRANGE
			var source = new[] { "foo", "bar", "buzz" };

			Func<string, Task<string>> operation = item => Task.FromResult(item.ToUpperInvariant());

			// ACT 
			var results = await source.ForEachAsync(maxDegreeOfParallelism, operation).ConfigureAwait(false);

			// ASSERT
			CollectionAssert.AreEqual(new[] { "FOO", "BAR", "BUZZ" }, results);
		}
	}
}
