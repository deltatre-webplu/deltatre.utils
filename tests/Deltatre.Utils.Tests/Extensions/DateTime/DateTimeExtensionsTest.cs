using System;
using Deltatre.Utils.Extensions.DateTimeExtension;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Extensions.DateTimeExtension
{
  [TestFixture]
  public class DateTimeExtensionsTest
  {
    [Test]
    public void Epoch_Should_Return_Unix_Epoch_Time()
    {
      // ARRANGE
      var expectedEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

      // ACT
      var actualEpoch = DateTimeExtensions.Epoch;

      // ASSERT
      Assert.AreEqual(expectedEpoch, actualEpoch);
    }

    [Test]
    public void ToUnixTimeSeconds_Should_Return_Zero_When_Called_With_Epoch()
    {
      // ARRANGE
      var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

      //ACT
      var unixTimeSeconds = epoch.ToUnixTimeSeconds();

      // ASSERT
      Assert.AreEqual(0, unixTimeSeconds);
    }

    [Test]
    public void ToUnixTimeSeconds_Should_Return_86400_When_Called_With_Epoch_Plus_One_Day()
    {
      // ARRANGE
      var date = new DateTime(1970, 1, 2, 0, 0, 0, DateTimeKind.Utc);

      //ACT
      var unixTimeSeconds = date.ToUnixTimeSeconds();

      // ASSERT
      Assert.AreEqual(86400, unixTimeSeconds);
    }

    [Test]
    public void ToUnixTimeSeconds_Should_Return_Minus_86400_When_Called_With_Epoch_Minus_One_Day()
    {
      // ARRANGE
      var date = new DateTime(1969, 12, 31, 0, 0, 0, DateTimeKind.Utc);

      //ACT
      var unixTimeSeconds = date.ToUnixTimeSeconds();

      // ASSERT
      Assert.AreEqual(-86400, unixTimeSeconds);
    }
  }
}
