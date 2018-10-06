using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deltatre.Utils.Validators
{
  /// <summary>
  /// A collection of helper methods to validate URLs
  /// </summary>
  public static class UrlHelpers
  {
    /// <summary>
    /// Checks whether a string is a valid absolute url. Both HTTP and HTTPS scheme are taken into account.
    /// </summary>
    /// <param name="value">The string to be validated as an absolute URL</param>
    /// <returns>True if parameter <paramref name="value"/> is a valid absolute URL, false otherwise</returns>
    public static bool IsValidAbsoluteUrl(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        return false;

      var isAbsoluteUrl = Uri.TryCreate(value, UriKind.Absolute, out var url);
      if (!isAbsoluteUrl)
        return false;

      return url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps;
    }
  }
}
