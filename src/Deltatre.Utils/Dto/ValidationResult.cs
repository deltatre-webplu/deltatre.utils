namespace Deltatre.Utils.Dto
{
	/// <summary>
	/// Represents the result obtained when validating a value of type T
	/// </summary>
	/// <typeparam name="T">The type of value being validated</typeparam>
	public class ValidationResult<T>
	{
		/// <summary>
		/// Call this method to create an instance representing the result of a successful validation.
		/// </summary>
		/// <param name="validatedValue">The result produced from the validation process.</param>
		/// <returns>An instance representing the result of a successful validation.</returns>
		public static ValidationResult<T> CreateValid(T validatedValue) => 
			new ValidationResult<T>(true, validatedValue);

		/// <summary>
		/// Call this method to create an instance representing the result of a failed validation.
		/// </summary>
		/// <returns>An instance representing the result of a failed validation.</returns>
		/// <remarks>Property ValidatedValue will be set equal to the default value of type T.</remarks>
		public static ValidationResult<T> CreateInvalid() => 
			new ValidationResult<T>(false, default(T));

		/// <summary>
		/// Indicates whether the value that was validated is valid 
		/// </summary>
		public bool IsValid { get; }

		/// <summary>
		/// This is the result produced from the validation process. In case of failed validation it will be set to the default value of type T.
		/// </summary>
		public T ValidatedValue { get; }

		private ValidationResult(bool isValid, T validatedValue)
		{
			IsValid = isValid;
			ValidatedValue = validatedValue;
		}
	}
}
