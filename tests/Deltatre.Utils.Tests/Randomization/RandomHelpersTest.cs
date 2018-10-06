using System;
using System.Collections.Generic;
using System.Linq;
using Deltatre.Utils.Randomization;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Randomization
{
  [TestFixture]
  public class RandomHelpersTest
  {
    [Test]
    public void GetRandomAlphanumericString_Throws_Argument_Out_Of_Range_Exception_When_Lenght_Is_Less_Than_Zero()
    {
      // ACT
      Assert.Throws<ArgumentOutOfRangeException>(() => RandomHelpers.GetRandomAlphanumericString(-3));
    }

    [Test]
    public void GetRandomAlphanumericString_Returns_Empty_String_When_Length_Equals_Zero()
    {
      // ACT
      var result = RandomHelpers.GetRandomAlphanumericString(0);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreEqual(string.Empty, result);
    }

    [Test]
    public void GetRandomAlphanumericString_Returns_Alphanumeric_String()
    {
      // ACT
      var result = RandomHelpers.GetRandomAlphanumericString(8);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.IsTrue(result.All(char.IsLetterOrDigit));
    }

    [Test]
    public void GetRandomAlphanumericString_Returns_String_Having_Requested_Length()
    {
      // ACT
      var result = RandomHelpers.GetRandomAlphanumericString(8);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreEqual(8, result.Length);
    }

    [Test]
    public void GetRandomAlphanumericString_Returns_String_With_Random_Characters()
    {
      // ARRANGE
      var results = new HashSet<string>();

      // ACT
      for (int i = 0; i < 1000; i++)
      {
        var result = RandomHelpers.GetRandomAlphanumericString(8);
        var addedToSet = results.Add(result);
        Assert.IsTrue(addedToSet);
      }
    }
  }
}
