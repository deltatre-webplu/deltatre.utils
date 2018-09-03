using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deltatre.Utils.Concurrency.Extensions;

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
	}
}
