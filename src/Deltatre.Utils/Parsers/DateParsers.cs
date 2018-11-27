using System;
using System.Globalization;
using Deltatre.Utils.Dto;

namespace Deltatre.Utils.Parsers
{
	/// <summary>
	/// A collection of methods to parse dates expressed in different formats
	/// </summary>
	public static class DateParsers
	{
		private static readonly string[]  Iso8601Formats = {
			//Microsoft standard date and time format strings
			"s",
			"o",

			//Extended formats
			"yyyy-MM-ddTHH:mm:ssZ",
			"yyyy-MM-ddTHH:mm:ssz",
			"yyyy-MM-ddTHH:mm:sszz",
			"yyyy-MM-ddTHH:mm:sszzz",

			// Basic formats
			"yyyyMMddTHHmmsszzz",
			"yyyyMMddTHHmmsszz",
			"yyyyMMddTHHmmssz",
			"yyyyMMddTHHmmssZ",

			//Extended formats with accuracy reduced to minutes
			"yyyy-MM-ddTHH:mmZ",
			"yyyy-MM-ddTHH:mmz",
			"yyyy-MM-ddTHH:mmzz",
			"yyyy-MM-ddTHH:mmzzz",

			// Basic formats with accuracy reduced to minutes
			"yyyyMMddTHHmmzzz",
			"yyyyMMddTHHmmzz",
			"yyyyMMddTHHmmz",
			"yyyyMMddTHHmmZ",

			//Extended formats with accuracy reduced to hours
			"yyyy-MM-ddTHHZ",
			"yyyy-MM-ddTHHz",
			"yyyy-MM-ddTHHzz",
			"yyyy-MM-ddTHHzzz",

			// Basic formats with accuracy reduced to hours
			"yyyyMMddTHHzzz",
			"yyyyMMddTHHzz",
			"yyyyMMddTHHz",
			"yyyyMMddTHHZ",

			// Formats with hour only
			"yyyy-MM-dd",
			"yyyyMMdd",

      // Javascript ISO8601 format
      "yyyy-MM-ddTHH:mm:ss.fffZ",
      "yyyy-MM-ddTHH:mm:ss.fffzzz"
    };

		/// <summary>
		/// Parses a date and time of day represented as a string in the format ISO 8601.
		/// </summary>
		/// <param name="toBeParsed">The ISO 8601 string to be parsed</param>
		/// <returns>An object of type ParsingResult which represents the result of the performed parsing operation</returns>
		/// <exception cref="ArgumentException">Throws ArgumentException if a null or white space string is passed in</exception>
		public static OperationResult<DateTimeOffset> ParseIso8601Date(string toBeParsed)
		{
			if(string.IsNullOrWhiteSpace(toBeParsed))
				throw new ArgumentException($"Parameter '{toBeParsed}' cannot be null or white space.");

			var isSuccess = DateTimeOffset.TryParseExact(
				toBeParsed, 
				Iso8601Formats, 
				CultureInfo.InvariantCulture, 
				DateTimeStyles.RoundtripKind,
				out var result);

			return isSuccess ? 
				OperationResult<DateTimeOffset>.CreateSuccess(result):
				OperationResult<DateTimeOffset>.Failure;
		}
	}
}
