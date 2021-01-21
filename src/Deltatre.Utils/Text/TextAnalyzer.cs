using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Deltatre.Utils.Text
{
	/// <summary>
	/// Provides a set of utility methods to perform analysis on text
	/// </summary>
	public class TextAnalyzer : ITextAnalyzer
	{
		private readonly string[] _cultureWordExceptions = new[] { "th-TH" };

		/// <summary>
		/// Gets the number of words contained in the provided text following the rules of the provided culture
		/// </summary>
		/// <param name="text">The text for which count the words</param>
		/// <param name="culture">The reference culture</param>
		/// <returns>The number of words</returns>
		public int CountWords(string text, string culture)
		{
			if (string.IsNullOrEmpty(text))			
				return 0;

			var cultureInfo = new CultureInfo(culture);
			if (!cultureInfo.TextInfo.IsRightToLeft && !IsWordsText(text, cultureInfo))
				return CountCharacters(text, culture, new[] { " " });

			return CalculateWordCount(text);
		}

		/// <summary>
		/// Gets the number of characters contained in the provided text following the rules of the provided culture
		/// </summary>
		/// <param name="text">The text for which count the characters</param>
		/// <param name="culture">The reference culture</param>
		/// <param name="skippedChars">A list of characters which will not be counted</param>
		/// <returns>The number of characters</returns>
		public int CountCharacters(string text, string culture, string[] skippedChars = null)
		{
			if (string.IsNullOrEmpty(text))
				return 0;

			if (skippedChars != null)
				foreach (var skippedChar in skippedChars)
					text = text.Replace(skippedChar, "");

			return text.Length;
		}

		/// <summary>
		/// Gets the <see cref="TextInfo"/> object of a specific culture
		/// </summary>
		/// <param name="culture">The culture for which get the <see cref="TextInfo"/></param>
		/// <returns></returns>
		public TextInfo GetTextInfo(string culture)
		{
			var cultureInfo = new CultureInfo(culture);
			return cultureInfo.TextInfo;
		}

		private static int CalculateWordCount(string text)
		{
			var collection = Regex.Matches(text, @"[\S]+");
			return collection.Count;
		}

		private bool IsWordsText(string text, CultureInfo cultureInfo)
		{
			if (cultureInfo.TextInfo.ANSICodePage == 0 || _cultureWordExceptions.Contains(cultureInfo.Name))
				return true;

			var chekLength = text.Length < 10
				? text.Length
				: 10;

			var otherLetterChars = 0;
			var spaceChars = 0;
			for (int t = 0; t < chekLength; t++)
			{
				var cat = char.GetUnicodeCategory(text[t]);
				switch (cat)
				{
					case UnicodeCategory.OtherLetter:
						otherLetterChars++;
						break;
					case UnicodeCategory.SpaceSeparator:
						spaceChars++;
						break;
				}
				//if (cat == UnicodeCategory.OtherLetter)
				//{
				//	otherLetterChars++;
				//}
			}

			return otherLetterChars == 0;
		}
	}
}
