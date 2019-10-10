using Deltatre.Utils.Types;
using NUnit.Framework;
using System;
using System.Linq;

namespace Deltatre.Utils.Tests.Types
{
  [TestFixture]
  public class NonEmptyListTest
  {
    [Test]
    public void Ctor_Throws_ArgumentNullException_When_Items_Is_Null()
    {
      // ACT
      var exception = Assert.Throws<ArgumentNullException>(() => new NonEmptyList<string>(null));

      // ASSERT
      Assert.IsNotNull(exception);
      Assert.AreEqual("items", exception.ParamName);
    }

    [Test]
    public void Ctor_Throws_When_Items_Is_Empty_Sequence()
    {
      // ACT
      var exception = Assert.Throws<ArgumentException>(() => new NonEmptyList<string>(Enumerable.Empty<string>()));

      // ASSERT
      Assert.IsNotNull(exception);
      Assert.AreEqual("items", exception.ParamName);
    }

    [Test]
    public void Ctor_Creates_An_Enumerable_Instance()
    {
      // ARRANGE
      var items = new[] { "foo", "bar" };

      // ACT
      var result = new NonEmptyList<string>(items);

      // ASSERT
      Assert.IsNotNull(result);
      CollectionAssert.AreEqual(new[] { "foo", "bar" }, result);
    }

    [Test]
    public void Count_Returns_Number_Of_Items_In_List()
    {
      // ARRANGE
      var items = new[] { "foo", "bar" };
      var target = new NonEmptyList<string>(items);

      // ACT
      var result = target.Count;

      // ASSERT
      Assert.AreEqual(2, result);
    }

    [Test]
    public void Indexer_Can_Be_Used_To_Access_List_Items()
    {
      // ARRANGE
      var items = new[] { "foo", "bar" };
      var target = new NonEmptyList<string>(items);

      // ACT
      var firstItem = target[0];
      var secondItem = target[1];

      // ASSERT
      Assert.AreEqual("foo", firstItem);
      Assert.AreEqual("bar", secondItem);
    }
  }
}
