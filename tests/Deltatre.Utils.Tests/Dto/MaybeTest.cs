using Deltatre.Utils.Dto;
using NUnit.Framework;
using System;

namespace Deltatre.Utils.Tests.Dto
{
  [TestFixture]
  public sealed class MaybeTest
  {
    [Test]
    public void Ctor_Allows_To_Create_New_Instance() 
    {
      // ARRANGE
      var value = new Person("Enrico");

      // ACT
      var result = new Maybe<Person>(value);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.IsTrue(result.HasValue);
      Assert.AreSame(value, result.Value);
    }

    [Test]
    public void None_Is_The_Empty_Instance()
    {
      // ACT
      var result = Maybe<Person>.None;

      // ASSERT
      Assert.IsNotNull(result);
      Assert.IsFalse(result.HasValue);
    }

    [Test]
    public void Value_Throws_InvalidOperationException_When_Instance_Is_Empty()
    {
      // ARRANGE
      var target = Maybe<Person>.None;

      // ACT
      var exception = Assert.Throws<InvalidOperationException>(
        () => _ = target.Value
      );

      // ASSERT
      Assert.IsNotNull(exception);
      Assert.AreEqual("Accessing the value of an empty Maybe instance is not allowed", exception.Message);
    }

    [Test]
    public void Value_Does_Not_Throw_When_Instance_Is_Not_Empty()
    {
      // ARRANGE
      var value = new Person("Enrico");
      var target = new Maybe<Person>(value);

      // ACT
      Assert.DoesNotThrow(
        () => _ = target.Value
      );
    }

    [Test]
    public void Value_Returns_Wrapped_Value_When_Instance_Is_Not_Empty()
    {
      // ARRANGE
      var value = new Person("Enrico");
      var target = new Maybe<Person>(value);

      // ACT
      var result = target.Value;

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreSame(value, result);
    }

    [Test]
    public void HasValue_Returns_True_When_Instance_Is_Not_Empty()
    {
      // ARRANGE
      var value = new Person("Enrico");
      var target = new Maybe<Person>(value);

      // ACT
      var result = target.HasValue;

      // ASSERT
      Assert.IsTrue(result);
    }

    [Test]
    public void HasValue_Returns_False_When_Instance_Is_Empty()
    {
      // ARRANGE
      var target = Maybe<Person>.None;

      // ACT
      var result = target.HasValue;

      // ASSERT
      Assert.IsFalse(result);
    }
  }
}
