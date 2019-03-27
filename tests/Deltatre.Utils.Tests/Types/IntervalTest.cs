using System;
using Deltatre.Utils.Types;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Types
{
  [TestFixture]
  public class IntervalTest
  {
    [Test]
    public void Ctor_Should_Throws_When_From_GreaterThan_To()
    {
      // ARRANGE
      var from = Endpoint<int>.Inclusive(2);
      var to = Endpoint<int>.Inclusive(1);

      // ACT & ASSERT
      Assert.Throws<ArgumentException>(() => new Interval<int>(from, to));
    }

    [Test]
    public void Ctor_Should_Correctly_Construct_Range_Object_When_Parameters_Are_Valid()
    {
      // ARRANGE
      var from = Endpoint<int>.Inclusive(1);
      var to = Endpoint<int>.Exclusive(2);

      // ACT
      var result = new Interval<int>(from, to);

      // ASSERT
      Assert.IsTrue(result.From == from);
      Assert.IsTrue(result.To == to);
    }

    [Test]
    public void Equals_Should_Return_False_If_Endpoint_Value_Are_Not_Equals()
    {
      // ARRANGE
      var from = Endpoint<int>.Inclusive(1);
      var to_2 = Endpoint<int>.Inclusive(2);
      var to_3 = Endpoint<int>.Inclusive(3);

      var interval1 = new Interval<int>(from, to_2);
      var interval2 = new Interval<int>(from, to_3);

      // ACT & ASSERT
      Assert.IsFalse(interval1 == interval2);
      Assert.IsTrue(interval1 != interval2);
    }

    [Test]
    public void Equals_Should_Return_False_If_Endpoint_Statuses_Are_Not_Equals()
    {
      // ARRANGE
      var from = Endpoint<int>.Inclusive(1);
      var to_inclusive = Endpoint<int>.Inclusive(2);
      var to_exclusive = Endpoint<int>.Exclusive(2);

      var interval1 = new Interval<int>(from, to_inclusive);
      var interval2 = new Interval<int>(from, to_exclusive);

      // ACT & ASSERT
      Assert.IsFalse(interval1 == interval2);
      Assert.IsTrue(interval1 != interval2);
    }

    [Test]
    public void Equals_Should_Return_True_If_Both_Intervals_Are_Null()
    {
      // ARRANGE
      Interval<int> interval1 = null;
      Interval<int> interval2 = null;

      // ACT & ASSERT
      Assert.IsTrue(interval1 == interval2);
      Assert.IsFalse(interval1 != interval2);
    }

    [Test]
    public void Equals_Should_Return_False_If_One_Interval_is_Null()
    {
      // ARRANGE
      Interval<int> interval1 = null;
      var interval2 = new Interval<int>(Endpoint<int>.Inclusive(1), Endpoint<int>.Exclusive(2));

      // ACT & ASSERT
      Assert.IsFalse(interval1 == interval2);
      Assert.IsTrue(interval1 != interval2);
    }

    [Test]
    public void Equals_Should_Return_True_If_Intervals_Are_Equal()
    {
      // ARRANGE
      var interval1 = new Interval<int>(Endpoint<int>.Inclusive(1), Endpoint<int>.Exclusive(2));
      var interval2 = new Interval<int>(Endpoint<int>.Inclusive(1), Endpoint<int>.Exclusive(2));

      // ACT & ASSERT
      Assert.IsTrue(interval1 == interval2);
      Assert.IsFalse(interval1 != interval2);
    }

    [Test]
    public void Equals_Should_Return_True_If_Intervals_Are_Both_Empty()
    {
      // ARRANGE
      var interval1 = Interval<int>.Empty;
      var interval2 = Interval<int>.Empty;

      // ACT & ASSERT
      Assert.IsTrue(interval1 == interval2);
      Assert.IsFalse(interval1 != interval2);
    }

  }
}
