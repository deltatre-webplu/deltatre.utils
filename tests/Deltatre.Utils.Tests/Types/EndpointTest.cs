using System;
using Deltatre.Utils.Types;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Types
{
  [TestFixture]
  public class EndpointTest
  {
    [Test]
    public void CreateInclusive_Should_Create_Inclusive_Endpoint()
    {
      // ARRANGE
      var endpointValue = DateTimeOffset.Now;

      // ACT
      var endpoint = Endpoint<DateTimeOffset>.Inclusive(endpointValue);

      // ARRANGE
      Assert.IsNotNull(endpoint);
      Assert.AreEqual(endpointValue, endpoint.Value);
      Assert.AreEqual(EndPointStatus.Inclusive, endpoint.Status);
    }

    [Test]
    public void CreateExclusive_Should_Create_Exclusive_Endpoint()
    {
      // ARRANGE
      var endpointValue = DateTimeOffset.Now;

      // ACT
      var endpoint = Endpoint<DateTimeOffset>.Exclusive(endpointValue);

      // ARRANGE
      Assert.IsNotNull(endpoint);
      Assert.AreEqual(endpointValue, endpoint.Value);
      Assert.AreEqual(EndPointStatus.Exclusive, endpoint.Status);
    }

    [Test]
    public void Generic_CompareTo_Should_Correctly_Handle_GreaterThan()
    {
      // ARRANGE
      var now = DateTime.Now;
      var biggerValue = new DateTimeOffset(now);
      var smallerValue = new DateTimeOffset(now).AddDays(-1);

      var biggerEndpoint = Endpoint<DateTimeOffset>.Inclusive(biggerValue);
      var smallerEndpoint = Endpoint<DateTimeOffset>.Exclusive(smallerValue);

      // ACT
      var result = biggerEndpoint.CompareTo(smallerEndpoint);

      // ASSERT
      Assert.Greater(result, 0);
    }

    [Test]
    public void Generic_CompareTo_Should_Correctly_Handle_LowerThan()
    {
      // ARRANGE
      var now = DateTime.Now;
      var biggerValue = new DateTimeOffset(now);
      var smallerValue = new DateTimeOffset(now).AddDays(-1);

      var biggerEndpoint = Endpoint<DateTimeOffset>.Exclusive(biggerValue);
      var smallerEndpoint = Endpoint<DateTimeOffset>.Inclusive(smallerValue);

      // ACT
      var result = smallerEndpoint.CompareTo(biggerEndpoint);

      // ASSERT
      Assert.Less(result, 0);
    }

    [Test]
    public void Generic_CompareTo_Should_Correctly_Handle_EqualThan()
    {
      // ARRANGE
      var now = DateTime.Now;
      var firstValue = new DateTimeOffset(now);
      var secondValue = new DateTimeOffset(now);

      var firstEndpoint = Endpoint<DateTimeOffset>.Exclusive(firstValue);
      var secondEndpoint = Endpoint<DateTimeOffset>.Inclusive(secondValue);

      // ACT
      var result = firstEndpoint.CompareTo(secondEndpoint);

      // ASSERT
      Assert.AreEqual(0, result);
    }

    [Test]
    public void NonGeneric_CompareTo_Should_Throws_When_Passed_An_Object_Of_Different_Type()
    {
      // ARRANGE
      var endpoint = Endpoint<DateTimeOffset>.Inclusive(DateTimeOffset.Now);
      const string differentType = "test";

      // ACT & ASSERT
      Assert.Throws<ArgumentException>(() => endpoint.CompareTo(differentType));
    }

    [Test]
    public void NonGeneric_CompareTo_Should_Correctly_Handle_Null_Values()
    {
      // ARRANGE
      var endpoint = Endpoint<DateTimeOffset>.Inclusive(DateTimeOffset.Now);

      // ACT & ASSERT
      Assert.Throws<ArgumentNullException>(() => endpoint.CompareTo((object) null));
    }

    [Test]
    public void NonGeneric_CompareTo_Should_Correctly_Handle_GreaterThan()
    {
      // ARRANGE
      var now = DateTime.Now;
      var biggerValue = new DateTimeOffset(now);
      var smallerValue = new DateTimeOffset(now).AddDays(-1);

      var biggerEndpoint = Endpoint<DateTimeOffset>.Exclusive(biggerValue);
      object smallerEndpoint = Endpoint<DateTimeOffset>.Inclusive(smallerValue);

      // ACT
      var result = biggerEndpoint.CompareTo(smallerEndpoint);

      // ASSERT
      Assert.Greater(result, 0);
    }

    [Test]
    public void NonGeneric_CompareTo_Should_Correctly_Handle_LowerThan()
    {
      // ARRANGE
      var now = DateTime.Now;
      var biggerValue = new DateTimeOffset(now);
      var smallerValue = new DateTimeOffset(now).AddDays(-1);

      object biggerEndpoint = Endpoint<DateTimeOffset>.Inclusive(biggerValue);
      var smallerEndpoint = Endpoint<DateTimeOffset>.Exclusive(smallerValue);

      // ACT
      var result = smallerEndpoint.CompareTo(biggerEndpoint);

      // ASSERT
      Assert.Less(result, 0);
    }

    [Test]
    public void NonGeneric_CompareTo_Should_Correctly_Handle_EqualThan()
    {
      // ARRANGE
      var now = DateTime.Now;
      var firstValue = new DateTimeOffset(now);
      var secondValue = new DateTimeOffset(now);

      var firstEndpoint = Endpoint<DateTimeOffset>.Inclusive(firstValue);
      object secondEndpoint = Endpoint<DateTimeOffset>.Exclusive(secondValue);

      // ACT
      var result = firstEndpoint.CompareTo(secondEndpoint);

      // ASSERT
      Assert.AreEqual(0, result);
    }

    [Test]
    public void GreaterThan_Operator_Should_Behave_Correctly()
    {
      // ARRANGE
      var now = DateTime.Now;
      var biggerValue = new DateTimeOffset(now);
      var smallerValue = new DateTimeOffset(now).AddDays(-1);

      var biggerEndpoint = Endpoint<DateTimeOffset>.Exclusive(biggerValue);
      var smallerEndpoint = Endpoint<DateTimeOffset>.Inclusive(smallerValue);

      // ACT & ASSERT
      Assert.IsTrue(biggerEndpoint > smallerEndpoint);
      Assert.IsFalse(biggerEndpoint <= smallerEndpoint);
    }

    [Test]
    public void GreaterThanOrEqual_Operator_Should_Behave_Correctly_When_Greater()
    {
      // ARRANGE
      var now = DateTime.Now;
      var biggerValue = new DateTimeOffset(now);
      var smallerValue = new DateTimeOffset(now).AddDays(-1);

      var biggerEndpoint = Endpoint<DateTimeOffset>.Exclusive(biggerValue);
      var smallerEndpoint = Endpoint<DateTimeOffset>.Inclusive(smallerValue);

      // ACT & ASSERT
      Assert.IsTrue(biggerEndpoint >= smallerEndpoint);
      Assert.IsFalse(biggerEndpoint < smallerEndpoint);
    }

    [Test]
    public void GreaterThanOrEqual_Operator_Should_Behave_Correctly_When_Equals()
    {
      // ARRANGE
      var now = DateTime.Now;
      var firstValue = new DateTimeOffset(now);
      var secondValue = new DateTimeOffset(now);

      var biggerEndpoint = Endpoint<DateTimeOffset>.Exclusive(firstValue);
      var smallerEndpoint = Endpoint<DateTimeOffset>.Inclusive(secondValue);

      // ACT & ASSERT
      Assert.IsTrue(biggerEndpoint >= smallerEndpoint);
      Assert.IsFalse(biggerEndpoint < smallerEndpoint);
    }

    [Test]
    public void LesserThan_Operator_Should_Behave_Correctly()
    {
      // ARRANGE
      var now = DateTime.Now;
      var biggerValue = new DateTimeOffset(now);
      var smallerValue = new DateTimeOffset(now).AddDays(-1);

      var biggerEndpoint = Endpoint<DateTimeOffset>.Exclusive(biggerValue);
      var smallerEndpoint = Endpoint<DateTimeOffset>.Inclusive(smallerValue);

      // ACT & ASSERT
      Assert.IsTrue(smallerEndpoint < biggerEndpoint);
      Assert.IsFalse(smallerEndpoint >= biggerEndpoint);
    }

    [Test]
    public void LesserThanOrEqual_Operator_Should_Behave_Correctly_When_Greater()
    {
      // ARRANGE
      var now = DateTime.Now;
      var biggerValue = new DateTimeOffset(now);
      var smallerValue = new DateTimeOffset(now).AddDays(-1);

      var biggerEndpoint = Endpoint<DateTimeOffset>.Exclusive(biggerValue);
      var smallerEndpoint = Endpoint<DateTimeOffset>.Inclusive(smallerValue);

      // ACT & ASSERT
      Assert.IsTrue(smallerEndpoint <= biggerEndpoint);
      Assert.IsFalse(smallerEndpoint > biggerEndpoint);
    }

    [Test]
    public void LesserThanOrEqual_Operator_Should_Behave_Correctly_When_Equals()
    {
      // ARRANGE
      var now = DateTime.Now;
      var firstValue = new DateTimeOffset(now);
      var secondValue = new DateTimeOffset(now);

      var firstEndpoint = Endpoint<DateTimeOffset>.Exclusive(firstValue);
      var secondEndpoint = Endpoint<DateTimeOffset>.Inclusive(secondValue);

      // ACT & ASSERT
      Assert.IsTrue(secondEndpoint <= firstEndpoint);
      Assert.IsFalse(secondEndpoint > firstEndpoint);
    }

    [Test]
    public void Equals_Operator_Should_Return_False_If_Not_Equals_On_Status()
    {
      // ARRANGE
      var now = DateTime.Now;
      var firstValue = new DateTimeOffset(now);
      var secondValue = new DateTimeOffset(now);

      var firstEndpoint = Endpoint<DateTimeOffset>.Exclusive(firstValue);
      var secondEndpoint = Endpoint<DateTimeOffset>.Inclusive(secondValue);

      // ACT & ASSERT
      Assert.IsFalse(firstEndpoint == secondEndpoint);
      Assert.IsTrue(firstEndpoint != secondEndpoint);
    }

    [Test]
    public void Equals_Operator_Should_Return_False_If_Not_Equals_On_Value()
    {
      // ARRANGE
      var now = DateTime.Now;
      var firstValue = new DateTimeOffset(now);
      var secondValue = new DateTimeOffset(now).AddMilliseconds(1);

      var firstEndpoint = Endpoint<DateTimeOffset>.Exclusive(firstValue);
      var secondEndpoint = Endpoint<DateTimeOffset>.Exclusive(secondValue);

      // ACT & ASSERT
      Assert.IsFalse(firstEndpoint == secondEndpoint);
      Assert.IsTrue(firstEndpoint != secondEndpoint);
    }

    [Test]
    public void Equals_Operator_Should_Return_True_If_All_Properties_Are_Equal()
    {
      // ARRANGE
      var now = DateTime.Now;
      var firstValue = new DateTimeOffset(now);
      var secondValue = new DateTimeOffset(now);

      var firstEndpoint = Endpoint<DateTimeOffset>.Exclusive(firstValue);
      var secondEndpoint = Endpoint<DateTimeOffset>.Exclusive(secondValue);

      // ACT & ASSERT
      Assert.IsTrue(firstEndpoint == secondEndpoint);
      Assert.IsFalse(firstEndpoint != secondEndpoint);
    }
  }
}
