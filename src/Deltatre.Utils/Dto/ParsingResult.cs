namespace Deltatre.Utils.Dto
{
	/// <summary>
	/// Represents the result obtained when parsing a value of type T
	/// </summary>
	/// <typeparam name="T">The type of value being parsed</typeparam>
	public class ParsingResult<T>
	{
		/// <summary>
		/// Call this method to create an instance representing the result of a successful parsing.
		/// </summary>
		/// <param name="parsedValue">The result produced from the parsing process.</param>
		/// <returns>An instance representing the result of a successful parsing.</returns>
		public static ParsingResult<T> CreateValid(T parsedValue) =>
			new ParsingResult<T>(true, parsedValue);

		/// <summary>
		/// Call this method to create an instance representing the result of a failed parsing.
		/// </summary>
		/// <returns>An instance representing the result of a failed parsing.</returns>
		/// <remarks>Property ParsedValue will be set equal to the default value of type T.</remarks>
		public static ParsingResult<T> CreateInvalid() =>
			new ParsingResult<T>(false, default(T));

		/// <summary>
		/// Indicates whether the value that was parsed is valid. 
		/// </summary>
		public bool IsValid { get; }

		/// <summary>
		/// This is the result produced from the parsing process. In case of failed parsing it will be set to the default value of type T.
		/// </summary>
		public T ParsedValue { get; }

		private ParsingResult(bool isValid, T parsedValue)
		{
			IsValid = isValid;
			ParsedValue = parsedValue;
		}
	}
}
