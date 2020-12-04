using System;
using System.Runtime.InteropServices;

namespace Deltatre.Utils.Azure
{
  /// <summary>
  /// A collection of helper methods for applications running on the Azure cloud.
  /// </summary>
  public static class AzureHelpers
  {
    private const string AzureEnvironmentVariableForSiteName = "WEBSITE_SITE_NAME";

    /// <summary>
    /// Detects whether the running application is being hosted in the Azure cloud.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the running application is being hosted in Azure, <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsRunningOnAzureAppService() =>
      !string.IsNullOrWhiteSpace(
        Environment.GetEnvironmentVariable(AzureEnvironmentVariableForSiteName)
      );

    /// <summary>
    /// Returns the absolute path to a file which can be used to write logs so that they will be visible inside the Azure
    /// log stream. Both Windows and Linux app services are supported.
    /// </summary>
    /// <returns>
    /// The absolute path to a file that can be used to write logs so that they will be visible inside the Azure
    /// log stream.
    /// </returns>
    /// <exception cref="OsNotSupportedOnAzureAppServicesException">
    /// When the hosting environment is running an OS which is not compatible with Azure App Services (e.g.: OSX)
    /// </exception>
    public static string GetAzureLogStreamFilePath()
    {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        return @"D:\home\LogFiles\Application\logs.txt";
      }
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
      {
        return "/home/LogFiles/Application/logs.txt";
      }
      else
      {
        throw new OsNotSupportedOnAzureAppServicesException(
          $"The currently running operating system ({RuntimeInformation.OSDescription}) is not compatible with Azure app services."
        )
        {
          OperatingSystem = RuntimeInformation.OSDescription
        };
      }
    }
  }
}
