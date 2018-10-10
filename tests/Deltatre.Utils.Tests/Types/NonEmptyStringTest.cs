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
    public void Implicit_Cast_To_String_Is_Supported_From_Null_Instance()
    {
      // ARRANGE
      NonEmptyString instance = null;

      // ACT
      string result = instance;

      // ASSERT
      Assert.IsNull(result);
    }

    [Test]
    public void Implicit_Cast_To_String_Is_Supported_From_Non_Null_Instance()
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
      NonEmptyString result = null;
      var exception = Assert.Throws<InvalidCastException>(() => result = (NonEmptyString)value);

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
  }
}
