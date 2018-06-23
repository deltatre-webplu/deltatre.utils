using System;
using Deltatre.Utils.Extensions.Array;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Extensions.Array
{
	[TestFixture]
	public class ArrayExtensionsTest
	{
		[Test]
		public void Shuffle_Throws_When_Source_Is_Null()
		{
			// ARRANGE
			string[] source = null;

			// ACT
			Assert.Throws<ArgumentNullException>(() => source.Shuffle());
		}

		[Test]
		public void Shuffle_Returns_Empty_Array_When_Source_Is_Empty_Array()
		{
			// ARRANGE
			var source = new string[0];

			// ACT
			var result = source.Shuffle();

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsEmpty(result);
		}

		[Test]
		public void Shuffle_Returns_Source_When_Source_Is_Array_With_One_Item()
		{
			// ARRANGE
			var source = new[] {"foo"};

			// ACT
			var result = source.Shuffle();

			// ASSERT
			Assert.IsNotNull(result);
			CollectionAssert.AreEqual(source, result);
		}

		[Test]
		public void Shuffle_Returns_Permutation_Of_Source_When_Source_Is_Array_With_More_Than_One_Item()
		{
			// ARRANGE
			var source = new[] { "foo", "bar", "buzz" };

			// ACT
			var result = source.Shuffle();

			// ASSERT
			Assert.IsNotNull(result);
			CollectionAssert.AreNotEqual(source, result);
			CollectionAssert.AreEquivalent(source, result);

			// check that source sequence is not changed
			CollectionAssert.AreEqual(new[] { "foo", "bar", "buzz" }, source);
		}
	}
}
