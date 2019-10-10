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
		public static OperationResult<TOutput, TError> CreateFailure(NonEmptySequence<TError> errors)
		{
			if (errors == null)
				throw new ArgumentNullException(nameof(errors));

			return new OperationResult<TOutput, TError>(default, errors);
		}

		/// <summary>
		/// Call this method to create an instance representing the result of a failed operation.
		/// </summary>
		/// <param name="error">The detected error</param>
		/// <returns>An instance representing the result of a failed operation.</returns>
		public static OperationResult<TOutput, TError> CreateFailure(TError error) =>
      CreateFailure(new NonEmptySequence<TError>(new[] { error }));

    /// <summary>
    /// Call this method to create an instance representing the result of a successful operation. The list of errors will be set to an empty list.
    /// </summary>
    /// <param name="output">The operation output.</param>
    /// <returns>An instance representing the result of a successful operation.</returns>
    public static OperationResult<TOutput, TError> CreateSuccess(TOutput output) =>
			new OperationResult<TOutput, TError>(output, Enumerable.Empty<TError>());

    private readonly TOutput _output;

    private OperationResult(TOutput output, IEnumerable<TError> errors)
    {
      _output = output;
      Errors = new ReadOnlyCollection<TError>(errors.ToList());
    }

    /// <summary>
    /// Gets a flag indicating whether the operation completed successfully. 
    /// </summary>
    public bool IsSuccess => Errors.Count == 0;

    /// <summary>
    /// Gets the result produced from the operation. 
    /// Accessing this property throws <see cref="System.InvalidOperationException"/> when property <see cref="IsSuccess"/> is <see langword="false"/>.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">
    /// Throws <see cref="System.InvalidOperationException"/> when property <see cref="IsSuccess"/> is <see langword="false"/>.
    /// </exception>
    public TOutput Output
    {
      get
      {
        if (!this.IsSuccess)
        {
          throw new InvalidOperationException(
            "Reading the operation output is not allowed because the operation is failed.");
        }

        return _output;
      }
    }

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
