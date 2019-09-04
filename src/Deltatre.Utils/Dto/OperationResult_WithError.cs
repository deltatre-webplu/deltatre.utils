using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Deltatre.Utils.Types;

namespace Deltatre.Utils.Dto
{
	/// <summary>
	/// Represents the result obtained when performing an operation
	/// </summary>
	/// <typeparam name="TOutput">The type of the operation output</typeparam>
	/// <typeparam name="TError">The type of operation errors</typeparam>
	public sealed class OperationResult<TOutput, TError>
	{
		/// <summary>
		/// Call this method to create an instance representing the result of a failed operation.
		/// </summary>
		/// <param name="errors">All the detected errors</param>
		/// <exception cref="ArgumentNullException">Throws ArgumentNullException when parameter errors is null</exception>
		/// <returns>An instance representing the result of a failed operation.</returns>
		/// <remarks>Property Output will be set equal to the default value of type TOutput.</remarks>
		public static OperationResult<TOutput, TError> CreateFailure(NonEmptySequence<TError> errors)
		{
			if (errors == null)
				throw new ArgumentNullException(nameof(errors));

			return new OperationResult<TOutput, TError>(default(TOutput), errors);
		}

		/// <summary>
		/// Call this method to create an instance representing the result of a failed operation.
		/// </summary>
		/// <param name="error">The detected error</param>
		/// <exception cref="ArgumentNullException">Throws ArgumentNullException when parameter errors is null</exception>
		/// <returns>An instance representing the result of a failed operation.</returns>
		/// <remarks>Property Output will be set equal to the default value of type TOutput.</remarks>
		public static OperationResult<TOutput, TError> CreateFailure(TError error)
		{
			if (error == null)
				throw new ArgumentNullException(nameof(error));

			return CreateFailure(new NonEmptySequence<TError>(new[] { error }));
		}

		/// <summary>
		/// Call this method to create an instance representing the result of a successful operation. The list of errors will be set to an empty list.
		/// </summary>
		/// <param name="output">The operation output.</param>
		/// <returns>An instance representing the result of a successful operation.</returns>
		public static OperationResult<TOutput, TError> CreateSuccess(TOutput output) =>
			new OperationResult<TOutput, TError>(output, Enumerable.Empty<TError>());

    private OperationResult(TOutput output, IEnumerable<TError> errors)
    {
      Output = output;
      Errors = new ReadOnlyCollection<TError>(errors.ToList());
    }

    /// <summary>
    /// Indicates whether the operation completed successfully. 
    /// </summary>
    public bool IsSuccess => Errors.Count == 0;

		/// <summary>
		/// This is the result produced from the operation. In case of failed operation it will be set to the default value of type TOutput.
		/// </summary>
		public TOutput Output { get; }

		/// <summary>
		/// All the detected errors. In case of successfull operation this collection will be empty.
		/// </summary>
		public ReadOnlyCollection<TError> Errors { get; }

    /// <summary>
    /// Implicit type conversion from <typeparamref name="TOutput"/> to <see cref="OperationResult{TOutput, TError}" />
    /// </summary>
    /// <param name="value">An instance of type<typeparamref name="TOutput"/> to be converted to <see cref="OperationResult{TOutput, TError}"/></param>
    public static implicit operator OperationResult<TOutput, TError>(TOutput value)
    {
      return CreateSuccess(value);
    }
  }
}
