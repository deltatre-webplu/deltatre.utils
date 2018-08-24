using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Deltatre.Utils.Types;

namespace Deltatre.Utils.Dto
{
	/// <summary>
	/// Represents the result obtained when parsing a value of type TValue. Parsing errors are of type TError
	/// </summary>
	/// <typeparam name="TValue">The type of value being parsed</typeparam>
	/// <typeparam name="TError">The type of parsing errors</typeparam>
	[Obsolete("Use OperationResult instead")]
	public class ParsingResult<TValue, TError>
	{
		/// <summary>
		/// Call this method to create an instance representing the result of a failed parsing.
		/// </summary>
		/// <param name="errors">All the detected parsing errors.</param>
		/// <exception cref="ArgumentNullException">Throws ArgumentNullException when parameter errors is null</exception>
		/// <returns>An instance representing the result of a failed parsing.</returns>
		/// <remarks>Property ParsedValue will be set equal to the default value of type TValue.</remarks>
		public static ParsingResult<TValue, TError> CreateInvalid(NonEmptySequence<TError> errors)
		{
			if (errors == null)
				throw new ArgumentNullException(nameof(errors));

			return new ParsingResult<TValue, TError>(default(TValue), errors);
		}

		/// <summary>
		/// Call this method to create an instance representing the result of a successful parsing. The list of errors will be set to an empty list.
		/// </summary>
		/// <param name="parsedValue">The result produced from the parsing process.</param>
		/// <returns>An instance representing the result of a successful parsing.</returns>
		public static ParsingResult<TValue, TError> CreateValid(TValue parsedValue) =>
			new ParsingResult<TValue, TError>(parsedValue, Enumerable.Empty<TError>());

		/// <summary>
		/// Indicates whether the value that was parsed is valid 
		/// </summary>
		public bool IsValid => !Errors.Any();

		/// <summary>
		/// This is the result produced from the parsing process. In case of failed parsing it will be set to the default value of type TValue.
		/// </summary>
		public TValue ParsedValue { get; }

		/// <summary>
		/// All the detected parsing errors. In case of successfull parsing this collection will be empty.
		/// </summary>
		public ReadOnlyCollection<TError> Errors { get; }

		private ParsingResult(TValue parsedValue, IEnumerable<TError> errors)
		{
			ParsedValue = parsedValue;
			Errors = new ReadOnlyCollection<TError>(errors.ToList());
		}
	}
}
