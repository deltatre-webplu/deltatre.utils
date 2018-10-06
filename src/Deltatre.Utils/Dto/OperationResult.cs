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
    /// The result of a failed operation
    /// </summary>
    public static readonly OperationResult<TOutput> Failure = new OperationResult<TOutput>(false, default(TOutput));

    /// <summary>
    /// Call this method to create an instance representing the result of a successful operation.
    /// </summary>
    /// <param name="output">The operation output.</param>
    /// <returns>An instance representing the result of a successful operation.</returns>
    public static OperationResult<TOutput> CreateSuccess(TOutput output) =>
			new OperationResult<TOutput>(true, output);

    /// <summary>
    /// Call this method to create an instance representing the result of a failed operation.
    /// </summary>
    /// <returns>An instance representing the result of a failed operation.</returns>
    /// <remarks>Property Output will be set equal to the default value of type TOutput.</remarks>
    [Obsolete("Use the static field Failure instead")]
    public static OperationResult<TOutput> CreateFailure() =>
			new OperationResult<TOutput>(false, default(TOutput));

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
	}
}
