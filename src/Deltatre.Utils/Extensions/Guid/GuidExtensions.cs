using System;
using System.Text.RegularExpressions;

namespace Deltatre.Utils.Extensions.Guid
{
  /// <summary>
  /// Extension methods for <see cref="System.Guid"/>.
  /// </summary>
  public static class GuidExtensions
  {
    /// <summary>
    /// Converts a GUID to a Base64 string, removing "url-unfriendly" characters (/+=).
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToShortString(this System.Guid value)
    {
      return Regex.Replace(Convert.ToBase64String(value.ToByteArray()), "[/+=]", string.Empty);
    }
  }
}