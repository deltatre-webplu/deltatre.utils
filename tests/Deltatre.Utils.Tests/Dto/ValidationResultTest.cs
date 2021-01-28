using Deltatre.Utils.Dto;
using NUnit.Framework;
using System;

namespace Deltatre.Utils.Tests.Dto
{
  [TestFixture]
  public sealed class ValidationResultTest
  {
    [Test]
    public void Valid_Returns_Instance_For_Valid_Validation_Result()
    {
      // ACT
      var result = ValidationResult<string>.Valid;

      // ASSERT
      Assert.IsNotNull(result);
      Assert.IsTrue(result.IsValid);
      Assert.IsTrue(result.Errors.IsEmpty);
      Assert.AreEqual(0, result.Errors.Length);
    }

    [Test]
    public void Invalid_Returns_Instance_For_Invalid_Validation_Result()
    {
      // ARRANGE
      var errors = new[]
      {
        "first name is missing",
        "last name is invalid"
      };

      // ACT
      var result = ValidationResult<string>.Invalid(errors);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.IsFalse(result.IsValid);
      Assert.AreEqual(2, result.Errors.Length);
      CollectionAssert.AreEqual(errors, result.Errors);
    }

    [Test]
    public void Invalid_Throws_ArgumentNullException_When_Errors_Is_Null()
    {
      // ACT
      var exception = Assert.Throws<ArgumentNullException>(
        () => ValidationResult<string>.Invalid(null)
      );

      // ASSERT
      Assert.IsNotNull(exception);
      Assert.AreEqual("errors", exception.ParamName);
    }

    [Test]
    public void Invalid_Throws_ArgumentException_When_Errors_Is_Empty_Collection()
    {
      // ARRANGE
      var errors = Array.Empty<string>();

      // ACT
      var exception = Assert.Throws<ArgumentException>(
        () => ValidationResult<string>.Invalid(errors)
      );

      // ASSERT
      Assert.IsNotNull(exception);
      Assert.AreEqual("errors", exception.ParamName);
    }
  }
}
