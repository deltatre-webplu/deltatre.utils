using System.Globalization;

namespace Deltatre.Utils.Text
{
	/// <summary>
	/// Provides a set of utility methods to perform analysis on text
	/// </summary>
	public interface ITextAnalyzer
	{
		/// <summary>
		/// Gets the number of words contained in the provided text following the rules of the provided culture
		/// </summary>
		/// <param name="text">The text for which count the words</param>
		/// <param name="culture">The reference culture</param>
		/// <returns>The number of words</returns>
		int CountWords(string text, string culture);

		/// <summary>
		/// Gets the number of characters contained in the provided text following the rules of the provided culture
		/// </summary>
		/// <param name="text">The text for which count the characters</param>
		/// <param name="culture">The reference culture</param>
		/// <param name="skippedChars">A list of characters which will not be counted</param>
		/// <returns>The number of characters</returns>
		int CountCharacters(string text, string culture, string[] skippedChars = null);

		/// <summary>
		/// Gets the <see cref="TextInfo"/> object of a specific culture
		/// </summary>
		/// <param name="culture">The culture for which get the <see cref="TextInfo"/></param>
		/// <returns></returns>
		TextInfo GetTextInfo(string culture);
	}
}
