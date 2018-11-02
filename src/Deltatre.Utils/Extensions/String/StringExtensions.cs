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
    /// Truncat method, tests the length of the source string to see if it needs any processing. 
    /// If the source is longer than the max length, we call Substring to get the first N characters. 
    /// This copies the string.
    /// </summary>
    /// <param name="source">The string to be sanitized from HTML tags</param>
    /// <param name="length">The max length of the source</param>
    /// <param name="ellipsis">Value to add at the end of the returned string</param>
    /// <returns></returns>
    public static string Truncate(this string source, int length = 150, string ellipsis = "...")
    {
      if (string.IsNullOrWhiteSpace(source))
        return source;

      if (source.Length > length)
      {
        source = source.Substring(0, length);
      }
      if (string.IsNullOrWhiteSpace(ellipsis))
      {
        return source;
      }
      return string.Concat(source, ellipsis);
    }
  }
}
