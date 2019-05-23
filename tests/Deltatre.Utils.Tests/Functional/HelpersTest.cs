using System;
using NUnit.Framework;
using static Deltatre.Utils.Functional.Helpers;

namespace Deltatre.Utils.Tests.Functional
{
	[TestFixture]
	public class HelpersTest
	{
		[Test]
		public void GetiFirst_WithProjection_Throws_When_Source_Is_Null()
		{
			// ARRANGE
			Func<string, bool> startsWithLetterMCaseInsensitive = item =>
				item != null
				&& item.StartsWith("M", StringComparison.InvariantCultureIgnoreCase);

			Func<string, int> toLength = item => item.Length;

			// ACT
			Assert.Throws<ArgumentNullException>(() => GetFirst(null, startsWithLetterMCaseInsensitive, toLength));
		}

		[Test]
		public void GetiFirst_WithProjection_Throws_When_Predicate_Is_Null()
		{
			// ARRANGE
			var source = new[] { "foo", "bar" };

			Func<string, int> toLength = item => item.Length;

			// ACT
			Assert.Throws<ArgumentNullException>(() => GetFirst(source, null, toLength));
		}

		[Test]
		public void GetiFirst_WithProjection_Throws_When_Projection_Is_Null()
		{
			// ARRANGE
			var source = new[] { "foo", "bar" };

			Func<string, bool> startsWithLetterMCaseInsensitive = item =>
				item != null
				&& item.StartsWith("M", StringComparison.InvariantCultureIgnoreCase);

			Func<string, int> toLength = null;

			// ACT
			Assert.Throws<ArgumentNullException>(() => GetFirst(source, startsWithLetterMCaseInsensitive, toLength));
		}

		[Test]
		public void GetiFirst_WithProjection_Returns_Result_For_Item_Not_Found_When_None_Of_Items_Satisfies_Predicate()
		{
			// ARRANGE
			Func<string, bool> startsWithLetterMCaseInsensitive = item =>
				item != null
				&& item.StartsWith("M", StringComparison.InvariantCultureIgnoreCase);

			Func<string, int> toLength = item => item.Length;

			var source = new[] { "foo", "bar" };

			// ACT
			var result = GetFirst(source, startsWithLetterMCaseInsensitive, toLength);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual(default(int), result.Output);
		}

		[Test]
		public void GetiFirst_WithProjection_Returns_Result_For_Item_Found_When_One_Of_Items_Satisfies_Predicate()
		{
			// ARRANGE
			Func<string, bool> startsWithLetterMCaseInsensitive = item =>
				item != null
				&& item.StartsWith("M", StringComparison.InvariantCultureIgnoreCase);

			Func<string, int> toLength = item => item.Length;

			var source = new[] { "foo", "minimum" };

			// ACT
			var result = GetFirst(source, startsWithLetterMCaseInsensitive, toLength);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(7, result.Output);
		}

		[Test]
		public void GetiFirst_WithProjection_Returns_Result_For_First_Item_Found_When_Many_Of_Items_Satisfies_Predicate()
		{
			// ARRANGE
			Func<string, bool> startsWithLetterMCaseInsensitive = item =>
				item != null
				&& item.StartsWith("M", StringComparison.InvariantCultureIgnoreCase);

			Func<string, int> toLength = item => item.Length;

			var source = new[] { "fooz", "minimum", "Max" };

			// ACT
			var result = GetFirst(source, startsWithLetterMCaseInsensitive, toLength);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(7, result.Output);
		}

		[Test]
		public void GetiFirst_Throws_When_Source_Is_Null()
		{
			// ARRANGE
			Func<string, bool> startsWithLetterMCaseInsensitive = item =>
				item != null
				&& item.StartsWith("M", StringComparison.InvariantCultureIgnoreCase);

			// ACT
			Assert.Throws<ArgumentNullException>(() => GetFirst(null, startsWithLetterMCaseInsensitive));
		}

		[Test]
		public void GetiFirst_Throws_When_Predicate_Is_Null()
		{
			// ARRANGE
			var source = new[] { "foo", "bar" };

			// ACT
			Assert.Throws<ArgumentNullException>(() => GetFirst(source, null));
		}

		[Test]
		public void GetiFirst_Returns_Result_For_Item_Not_Found_When_None_Of_Items_Satisfies_Predicate()
		{
			// ARRANGE
			Func<string, bool> startsWithLetterMCaseInsensitive = item =>
				item != null
				&& item.StartsWith("M", StringComparison.InvariantCultureIgnoreCase);

			var source = new[] { "foo", "bar" };

			// ACT
			var result = GetFirst(source, startsWithLetterMCaseInsensitive);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual(default(string), result.Output);
		}

		[Test]
		public void GetiFirst_Returns_Result_For_Item_Found_When_One_Of_Items_Satisfies_Predicate()
		{
			// ARRANGE
			Func<string, bool> startsWithLetterMCaseInsensitive = item =>
				item != null
				&& item.StartsWith("M", StringComparison.InvariantCultureIgnoreCase);

			var source = new[] { "foo", "minimum" };

			// ACT
			var result = GetFirst(source, startsWithLetterMCaseInsensitive);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual("minimum", result.Output);
		}

		[Test]
		public void GetiFirst_Returns_Result_For_First_Item_Found_When_Many_Of_Items_Satisfies_Predicate()
		{
			// ARRANGE
			Func<string, bool> startsWithLetterMCaseInsensitive = item =>
				item != null
				&& item.StartsWith("M", StringComparison.InvariantCultureIgnoreCase);

			var source = new[] { "fooz", "minimum", "Max" };

			// ACT
			var result = GetFirst(source, startsWithLetterMCaseInsensitive);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual("minimum", result.Output);
		}
	}
}
