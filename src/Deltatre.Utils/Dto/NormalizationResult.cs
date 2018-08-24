using System;

namespace Deltatre.Utils.Dto
{
	/// <summary>
	/// Represents the result obtained when normalizing a value of type T
	/// </summary>
	/// <typeparam name="T">The type of value being normalized</typeparam>
	[Obsolete("Use OperationResult instead")]
	public class NormalizationResult<T>
	{
		/// <summary>
		/// Call this method to create an instance representing the result of a successful normalization.
		/// </summary>
		/// <param name="normalizedValue">The result produced from the normalization process.</param>
		/// <returns>An instance representing the result of a successful normalization.</returns>
		public static NormalizationResult<T> CreateValid(T normalizedValue) =>
			new NormalizationResult<T>(true, normalizedValue);

		/// <summary>
		/// Call this method to create an instance representing the result of a failed normalization.
		/// </summary>
		/// <returns>An instance representing the result of a failed normalization.</returns>
		/// <remarks>Property ValidatedValue will be set equal to the default value of type T.</remarks>
		public static NormalizationResult<T> CreateInvalid() =>
			new NormalizationResult<T>(false, default(T));

		/// <summary>
		/// Indicates whether the value that was normalized is valid. 
		/// </summary>
		public bool IsValid { get; }

		/// <summary>
		/// This is the result produced from the normalization process. In case of failed normalization it will be set to the default value of type T.
		/// </summary>
		public T NormalizedValue { get; }

		private NormalizationResult(bool isValid, T normalizedValue)
		{
			IsValid = isValid;
			NormalizedValue = normalizedValue;
		}
	}
}
