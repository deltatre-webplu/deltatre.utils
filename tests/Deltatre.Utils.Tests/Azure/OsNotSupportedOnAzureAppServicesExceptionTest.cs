using Deltatre.Utils.Azure;
using NUnit.Framework;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Deltatre.Utils.Tests.Azure
{
  [TestFixture]
  public sealed class OsNotSupportedOnAzureAppServicesExceptionTest
  {
    [Test]
    public void Exception_Object_Can_Be_Serialized_And_Then_Deserialized()
    {
      // ARRANGE
      var innerException = new InvalidOperationException("Something wrong happened");
      var target = new OsNotSupportedOnAzureAppServicesException(
        "The OSX operating system is not supported on Azure app services",
        innerException)
      {
        OperatingSystem = "Hello world"
      };

      // ACT
      var binaryFormatter = new BinaryFormatter();
      OsNotSupportedOnAzureAppServicesException result = null;

      using (var serializationStream = new MemoryStream()) 
      {
        binaryFormatter.Serialize(serializationStream, target);

        serializationStream.Seek(0, SeekOrigin.Begin);
        result = (OsNotSupportedOnAzureAppServicesException)binaryFormatter.Deserialize(serializationStream);
      }

      // ASSERT
      Assert.IsNotNull(result);

      Assert.IsNotNull(result.InnerException);
      Assert.IsInstanceOf<InvalidOperationException>(result.InnerException);

      Assert.AreEqual("The OSX operating system is not supported on Azure app services", result.Message);
      Assert.AreEqual("Something wrong happened", result.InnerException.Message);

      Assert.AreEqual("Hello world", result.OperatingSystem);
    }
  }
}
