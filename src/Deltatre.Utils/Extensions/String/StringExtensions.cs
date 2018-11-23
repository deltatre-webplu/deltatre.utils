using System;
using System.Text.RegularExpressions;

namespace Deltatre.Utils.Extensions.String
{
  /// <summary>
  /// Extension methods for strings
  /// </summary>
  public static class StringExtensions
  {
    private static readonly Regex HtmlTagRegex = new Regex(@"<\/?[A-Za-z0-9]+[^>]*>", RegexOptions.Compiled);

    /// <summary>
    /// This method is meant to be a simplistic tool to remove HTML tags (both opening and closing) from a string. Only the opening and closing tags will be removed the tag content won't.
    /// Opening tags not matching any closing tag will be removed.
    /// Closing tags not matching any opening tag will be removed.
    /// Encoded HTML tags won't be removed.
    /// Don't use this method as a general purpose string sanitization tool especially in critical contexts: it is regular expression based and this approach is known not to be optimal to parse HTML.
    /// </summary>
    /// <param name="source">The string to be sanitized from HTML tags</param>
    /// <returns>The source string cleaned by all the HTML tags</returns>
    public static string StripHtmlTags(this string source)
    {
      if (string.IsNullOrWhiteSpace(source))
        return source;

      return HtmlTagRegex.Replace(source, string.Empty);
    }

    /// <summary>
    /// Call this method if you want to process a string and be sure that its length doesn't exceed a maximum allowed length.
    /// If the lenght of the source string is less than or equal to the maximum allowed length then the source string is returned. 
    /// If the lenght of the source string is greater than the maximum allowedf length, then a substring of the source string is returned. 
    /// You can specify an optional suffix to be appended at the end of the returned string if the returned string is a substring of the source string. 
    /// </summary>
    /// <param name="source">The source string to be processed</param>
    /// <param name="maximumAllowedLength">The maximum allowed length of the source string. This value must be non negative.</param>
    /// <param name="ellipsis">The suffix to be appended at the end of the returned string if the source string is truncated because its length exceeds the maximum allowed length. If you pass a string null or white space then no suffix will be appended at the end of the returned string</param>
    /// <returns></returns>
    public static string Truncate(
      this string source,
      int maximumAllowedLength = 150,
      string ellipsis = "...")
    {
      if (maximumAllowedLength < 0)
        throw new ArgumentOutOfRangeException(
          nameof(maximumAllowedLength),
          $"Parameter '{nameof(maximumAllowedLength)}' must be non negative integer");

      if (string.IsNullOrWhiteSpace(source))
        return source;

      if (source.Length <= maximumAllowedLength)
        return source;

      var truncated = source.Substring(0, maximumAllowedLength);

      if (string.IsNullOrWhiteSpace(ellipsis))
        return truncated;

      return string.Concat(truncated, ellipsis);
    }
  }
}
