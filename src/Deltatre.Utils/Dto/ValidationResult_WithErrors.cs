using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Deltatre.Utils.Dto
{
	/// <summary>
	/// Represents the result obtained when validating a value of type TValue. Validation errors are of type TError
	/// </summary>
	/// <typeparam name="TValue">The type of value being validated</typeparam>
	/// <typeparam name="TError">The type of validation errors</typeparam>
	public class ValidationResult<TValue, TError>
	{
		/// <summary>
		/// Call this method to create an instance representing the result of a failed validation.
		/// </summary>
		/// <param name="errors">All the detected validation errors.</param>
		/// <exception cref="ArgumentNullException">Throws ArgumentNullException when parameter errors is null</exception>
		/// <returns>An instance representing the result of a failed validation.</returns>
		/// <remarks>Property ValidatedValue will be set equal to the default value of type TValue.</remarks>
		public static ValidationResult<TValue, TError> CreateInvalid(IEnumerable<TError> errors)
		{
			if (errors == null)
				throw new ArgumentNullException(nameof(errors));

			return new ValidationResult<TValue, TError>(false, default(TValue), errors);
		}

		/// <summary>
		/// Call this method to create an instance representing the result of a successful validation. The list of errors will be set to an empty list.
		/// </summary>
		/// <param name="validatedValue">The result produced from the validation process.</param>
		/// <returns>An instance representing the result of a successful validation.</returns>
		public static ValidationResult<TValue, TError> CreateValid(TValue validatedValue) => 
			new ValidationResult<TValue, TError>(true, validatedValue, Enumerable.Empty<TError>());

		/// <summary>
		/// Indicates whether the value that was validated is valid 
		/// </summary>
		public bool IsValid { get; }

		/// <summary>
		/// This is the result produced from the validation process. In case of failed validation it will be set to the default value of type TValue.
		/// </summary>
		public TValue ValidatedValue { get; }

		/// <summary>
		/// All the detected validation errors. In case of successfull validation this collection will be empty.
		/// </summary>
		public ReadOnlyCollection<TError> Errors { get; }

		private ValidationResult(bool isValid, TValue validatedValue, IEnumerable<TError> errors)
		{
			IsValid = isValid;
			ValidatedValue = validatedValue;
			Errors = new ReadOnlyCollection<TError>(errors.ToList());
		}
	}
}
