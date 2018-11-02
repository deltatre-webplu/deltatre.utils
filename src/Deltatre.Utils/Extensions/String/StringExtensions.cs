using System;
using System.Text.RegularExpressions;

namespace Deltatre.Utils.Extensions.String
{
    /// <summary>
    /// Extension methods for strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Call this method to simply remove html tags from the given string source.
        /// To include literal html in the returned string you should encode it (e.g.: &lt;h1&gt; Title here &lt;/h1&gt;).
        /// </summary>
        /// <param name="source">The string to be stripped</param>
        /// <returns>A string without html tags</returns>
        public static string StripTags(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return string.Empty;
            }
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
    }
}
