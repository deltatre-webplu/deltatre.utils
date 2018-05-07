using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Deltatre.Utils.Types;

namespace Deltatre.Utils.Dto
{
	/// <summary>
	/// Represents the result obtained when normalizing a value of type TValue. Normalization errors are of type TError
	/// </summary>
	/// <typeparam name="TValue">The type of value being normalized</typeparam>
	/// <typeparam name="TError">The type of normalization errors</typeparam>
	public class NormalizationResult<TValue, TError>
	{
		/// <summary>
		/// Call this method to create an instance representing the result of a failed normalization.
		/// </summary>
		/// <param name="errors">All the detected normalization errors.</param>
		/// <exception cref="ArgumentNullException">Throws ArgumentNullException when parameter errors is null</exception>
		/// <returns>An instance representing the result of a failed normalization.</returns>
		/// <remarks>Property NormalizedValue will be set equal to the default value of type TValue.</remarks>
		public static NormalizationResult<TValue, TError> CreateInvalid(NonEmptySequence<TError> errors)
		{
			if (errors == null)
				throw new ArgumentNullException(nameof(errors));

			return new NormalizationResult<TValue, TError>(default(TValue), errors);
		}

		/// <summary>
		/// Call this method to create an instance representing the result of a successful normalization. The list of errors will be set to an empty list.
		/// </summary>
		/// <param name="normalizedValue">The result produced from the normalization process.</param>
		/// <returns>An instance representing the result of a successful normalization.</returns>
		public static NormalizationResult<TValue, TError> CreateValid(TValue normalizedValue) =>
			new NormalizationResult<TValue, TError>(normalizedValue, Enumerable.Empty<TError>());

		/// <summary>
		/// Indicates whether the value that was normalized is valid 
		/// </summary>
		public bool IsValid => !Errors.Any();

		/// <summary>
		/// This is the result produced from the normalization process. In case of failed normalization it will be set to the default value of type TValue.
		/// </summary>
		public TValue NormalizedValue { get; }

		/// <summary>
		/// All the detected normalization errors. In case of successfull normalization this collection will be empty.
		/// </summary>
		public ReadOnlyCollection<TError> Errors { get; }

		private NormalizationResult(TValue normalizedValue, IEnumerable<TError> errors)
		{
			NormalizedValue = normalizedValue;
			Errors = new ReadOnlyCollection<TError>(errors.ToList());
		}
	}
}
