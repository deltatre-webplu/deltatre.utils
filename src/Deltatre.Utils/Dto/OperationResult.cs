using System;

namespace Deltatre.Utils.Dto
{
	/// <summary>
	/// Represents the result obtained when performing an operation
	/// </summary>
	/// <typeparam name="TOutput">The type of the operation output</typeparam>
	public sealed class OperationResult<TOutput>
	{
    /// <summary>
    /// The result of a failed operation. Property <see cref="Output"/> will be set equal to the default value of type TOutput.
    /// </summary>
    public static readonly OperationResult<TOutput> Failure = new OperationResult<TOutput>(false, default(TOutput));

    /// <summary>
    /// Call this method to create an instance representing the result of a successful operation.
    /// </summary>
    /// <param name="output">The operation output.</param>
    /// <returns>An instance representing the result of a successful operation.</returns>
    public static OperationResult<TOutput> CreateSuccess(TOutput output) =>
			new OperationResult<TOutput>(true, output);

    private OperationResult(bool isSuccess, TOutput output)
    {
      IsSuccess = isSuccess;
      Output = output;
    }

    /// <summary>
    /// Indicates whether the operation completed successfully. 
    /// </summary>
    public bool IsSuccess { get; }

		/// <summary>
		/// This is the result produced from the operation. In case of failed operation it will be set to the default value of type TOutput.
		/// </summary>
		public TOutput Output { get; }

    /// <summary>
    /// Implicit type conversion from <typeparamref name="TOutput"/> to <see cref="OperationResult{TOutput}" />
    /// </summary>
    /// <param name="value">An instance of type<typeparamref name="TOutput"/> to be converted to <see cref="OperationResult{TOutput}"/></param>
    public static implicit operator OperationResult<TOutput>(TOutput value)
    {
      return CreateSuccess(value);
    }
	}
}
