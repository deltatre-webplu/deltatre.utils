using System;
using System.Collections.Generic;
using Deltatre.Utils.Extensions.Enumerable;
using NUnit.Framework;
using Linq = System.Linq;

namespace Deltatre.Utils.Tests.Extensions.Enumerable
{
  [TestFixture]
  public class EnumerableExtensionsTest
  {
    [Test]
    public void IsNullOrEmpty_Should_Return_True_When_Source_Is_Null()
    {
      // ACT
      var result = EnumerableExtensions.IsNullOrEmpty<string>(null);

      // ASSERT
      Assert.IsTrue(result);
    }

    [Test]
    public void IsNullOrEmpty_Should_Return_True_When_Source_Is_Empty()
    {
      // ACT
      var result = Linq.Enumerable.Empty<string>().IsNullOrEmpty();

      // ASSERT
      Assert.IsTrue(result);
    }

    [Test]
    public void IsNullOrEmpty_Should_Return_False_When_Source_Is_Not_Null_Or_Empty()
    {
      // ACT
      var result = new [] { "test" }.IsNullOrEmpty();

      // ASSERT
      Assert.IsFalse(result);
    }

    [Test]
    public void ForEach_Throws_When_Source_Is_Null()
    {
      // ACT
      Assert.Throws<ArgumentNullException>(() => EnumerableExtensions.ForEach<string>(null, _ => { }));
    }

    [Test]
    public void ForEach_Throws_When_ToBeDone_Is_Null()
    {
      // ARRANGE
      var source = Linq.Enumerable.Empty<string>();

      // ACT
      Assert.Throws<ArgumentNullException>(() => source.ForEach(null));
    }

    [Test]
    public void ForEach_Calls_ToBeDone_For_Each_Item_In_Source()
    {
      // ARRANGE
      var source = new[] {"this", "is", "a", "test"};

      // ACT
      var strings = new List<string>();
      source.ForEach(item => strings.Add(item));

      // ASSERT
      CollectionAssert.AreEqual(new List<string> {"this", "is", "a", "test"}, strings);
    }

	  [Test]
	  public void HasDuplicates_Throws_When_Source_Is_Null()
	  {
		  // ACT
		  Assert.Throws<ArgumentNullException>(() => EnumerableExtensions.HasDuplicates<string>(null));
	  }

	  [Test]
	  public void HasDuplicates_Returns_False_When_Items_In_Source_Are_Distinct()
	  {
			// ARRANGE
		  var target = new[] {"foo", "bar"};

		  // ACT
		  var result = target.HasDuplicates();

			// ASSERT
			Assert.IsFalse(result);
	  }

	  [Test]
	  public void HasDuplicates_Returns_True_When_Source_Has_Duplicates()
	  {
		  // ARRANGE
		  var target = new[] { "foo", "bar", "foo", "hello", "hello", "world", "bar", "webplu", "foo" };

		  // ACT
		  var result = target.HasDuplicates();

		  // ASSERT
		  Assert.IsTrue(result);
	  }

	  [Test]
	  public void HasDuplicates_Returns_False_When_Items_In_Source_Are_Distinct_Based_On_Default_Comparer()
	  {
		  // ARRANGE
		  var target = new[] { "foo", "bar", "HELLO", "WorlD" };
		  var comparer = StringComparer.InvariantCultureIgnoreCase;

		  // ACT
		  var result = target.HasDuplicates(comparer);

		  // ASSERT
		  Assert.IsFalse(result);
	  }

	  [Test]
	  public void HasDuplicates_Returns_True_When_Source_Has_Duplicates_Based_On_Default_Comparer()
	  {
		  // ARRANGE
		  var target = new[] { "foo", "bar", "FOO", "FOO", "baR", "hello", "world", "HellO", "webplu" };
		  var comparer = StringComparer.InvariantCultureIgnoreCase;

		  // ACT
		  var result = target.HasDuplicates(comparer);

		  // ASSERT
		  Assert.IsTrue(result);
	  }

	}
}

