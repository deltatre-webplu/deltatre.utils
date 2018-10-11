using System;
using Deltatre.Utils.Types;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Types
{
  [TestFixture]
  public class NonEmptyStringTest
  {
    [TestCase(null)]
    [TestCase("")]
    [TestCase("    ")]
    public void Ctor_Throws_ArgumenException_When_Value_Is_Null_Or_White_Space(string value)
    {
      // ACT
      Assert.Throws<ArgumentException>(() => new NonEmptyString(value));
    }

    [Test]
    public void Ctor_Is_Able_To_Create_New_Instance()
    {
      // ACT
      var result = new NonEmptyString("hello world");

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreEqual("hello world", result.Value);
    }

    [Test]
    public void Implicit_Cast_To_String_Is_Supported()
    {
      // ARRANGE
      var instance = new NonEmptyString("foo bar");

      // ACT
      string result = instance;

      // ASSERT
      Assert.AreEqual("foo bar", result);
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("    ")]
    public void Explicit_Cast_Throws_InvalidCastExcpetion_When_Value_Is_Null_Or_White_Space(string value)
    {
      // ACT
      var exception = Assert.Throws<InvalidCastException>(() =>
      {
        NonEmptyString result = (NonEmptyString)value;
      });

      // ASSERT
      Assert.IsNotNull(exception);
      Assert.IsNotNull(exception.InnerException);
      Assert.IsInstanceOf<ArgumentException>(exception.InnerException);
    }

    [Test]
    public void Explicit_Cast_From_String_Is_Available()
    {
      // ACT
      const string value = "Test String";
      NonEmptyString result = (NonEmptyString)value;

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreEqual("Test String", result.Value);
    }

    [Test]
    public void Equals_Compares_Instances_By_Value()
    {
      // ARRANGE
      var instance1 = new NonEmptyString("foo");
      var instance2 = new NonEmptyString("foo");
      var instance3 = new NonEmptyString("bar");
      var instance4 = new NonEmptyString("FOO");
      var instance5 = new NonEmptyString("fOo");

      // ACT
      var result1 = instance1.Equals(instance2);
      var result2 = instance1.Equals(instance3);
      var result3 = instance1.Equals(instance4);
      var result4 = instance1.Equals(instance5);

      // ASSERT
      Assert.IsTrue(result1);
      Assert.IsFalse(result2);
      Assert.IsFalse(result3);
      Assert.IsFalse(result4);
    }

    [Test]
    public void Equals_Is_Reflexive()
    {
      // ARRANGE
      var instance = new NonEmptyString("foo");

      // ACT
      var result = instance.Equals(instance);

      // ASSERT
      Assert.IsTrue(result);
    }

    [Test]
    public void Equals_Is_Simmetric()
    {
      // ARRANGE
      var left = new NonEmptyString("foo");
      var right = new NonEmptyString("foo");

      // ACT
      var result1 = left.Equals(right);
      var result2 = right.Equals(left);

      // ASSERT
      Assert.IsTrue(result1);
      Assert.IsTrue(result2);
    }

    [Test]
    public void Equals_Is_Transitive()
    {
      // ARRANGE
      var instance1 = new NonEmptyString("foo");
      var instance2 = new NonEmptyString("foo");
      var instance3 = new NonEmptyString("foo");

      // ACT
      var result1 = instance1.Equals(instance2);
      var result2 = instance2.Equals(instance3);
      var result3 = instance1.Equals(instance3);

      // ASSERT
      Assert.IsTrue(result1);
      Assert.IsTrue(result2);
      Assert.IsTrue(result3);
    }

    [Test]
    public void Equals_Returns_False_When_Obj_Is_Null()
    {
      // ARRANGE
      var instance = new NonEmptyString("foo");

      // ACT
      var result = instance.Equals(null);

      // ASSERT
      Assert.IsFalse(result);
    }

    [Test]
    public void Equals_Returns_False_When_Obj_Is_Not_Of_Type_NonEmptyString()
    {
      // ARRANGE
      var instance = new NonEmptyString("foo");

      // ACT
      var result1 = instance.Equals(13);
      var result2 = instance.Equals(13M);
      var result3 = instance.Equals("foo");
      var result4 = instance.Equals(new DateTime(2015, 1, 1));
      var result5 = instance.Equals(new TestClass());
      var result6 = instance.Equals(new TestStruct());

      // ASSERT
      Assert.IsFalse(result1);
      Assert.IsFalse(result2);
      Assert.IsFalse(result3);
      Assert.IsFalse(result4);
      Assert.IsFalse(result5);
      Assert.IsFalse(result6);
    }

    [Test]
    public void When_Two_Instances_Are_Equals_Then_They_Have_Same_Hash_Code()
    {
      // ARRANGE
      var left = new NonEmptyString("foo");
      var right = new NonEmptyString("foo");

      // ACT
      var result1 = left.GetHashCode();
      var result2 = right.GetHashCode();

      // ASSERT
      Assert.AreEqual(result1, result2);
    }

    [Test]
    public void Equal_Operator_Compares_Instances_By_Value()
    {
      // ARRANGE
      var instance1 = new NonEmptyString("foo");
      var instance2 = new NonEmptyString("foo");
      var instance3 = new NonEmptyString("bar");
      var instance4 = new NonEmptyString("FOO");
      var instance5 = new NonEmptyString("fOo");

      // ACT
      var result1 = instance1 == instance2;
      var result2 = instance1 == instance3;
      var result3 = instance1 == instance4;
      var result4 = instance1 == instance5;

      // ASSERT
      Assert.IsTrue(result1);
      Assert.IsFalse(result2);
      Assert.IsFalse(result3);
      Assert.IsFalse(result4);
    }

    [Test]
    public void Equal_Operator_Is_Reflexive()
    {
      // ARRANGE
      var instance = new NonEmptyString("foo");

      // ACT
      var result = instance == instance;

      // ASSERT
      Assert.IsTrue(result);
    }

    [Test]
    public void Equal_Operator_Is_Simmetric()
    {
      // ARRANGE
      var left = new NonEmptyString("foo");
      var right = new NonEmptyString("foo");

      // ACT
      var result1 = left == right;
      var result2 = right == left;

      // ASSERT
      Assert.IsTrue(result1);
      Assert.IsTrue(result2);
    }

    [Test]
    public void Equal_Operator_Is_Transitive()
    {
      // ARRANGE
      var instance1 = new NonEmptyString("foo");
      var instance2 = new NonEmptyString("foo");
      var instance3 = new NonEmptyString("foo");

      // ACT
      var result1 = instance1 == instance2;
      var result2 = instance2 == instance3;
      var result3 = instance1 == instance3;

      // ASSERT
      Assert.IsTrue(result1);
      Assert.IsTrue(result2);
      Assert.IsTrue(result3);
    }

    [Test]
    public void NotEqual_Operator_Is_Coherent_With_Equal_Operator()
    {
      // ARRANGE
      var instance1 = new NonEmptyString("foo");
      var instance2 = new NonEmptyString("foo");
      var instance3 = new NonEmptyString("bar");

      // ACT
      var result1 = instance1 != instance2;
      var result2 = instance1 != instance3;

      // ASSERT
      Assert.IsFalse(result1);
      Assert.IsTrue(result2);
    }

    public class TestClass { }

    public struct TestStruct { }
  }
}
