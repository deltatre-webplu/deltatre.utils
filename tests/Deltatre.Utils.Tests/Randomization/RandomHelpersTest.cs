using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
  }
}
