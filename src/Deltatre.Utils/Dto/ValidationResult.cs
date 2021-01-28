using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Deltatre.Utils.Dto
{
  /// <summary>
  /// Represents the result of a validation process.
  /// </summary>
  /// <typeparam name="TError">
  /// The type of the validation error.
  /// </typeparam>
  public sealed class ValidationResult<TError>
  {
    /// <summary>
    /// Represents the validation result obtained when validating a valid item.
    /// This field is read-only.
    /// </summary>
    /// <remarks>
    /// A valid item is an item for which the validation process has detected no
    /// validation errors.
    /// </remarks>
    public static readonly ValidationResult<TError> Valid = new ValidationResult<TError>(ImmutableArray<TError>.Empty);

    /// <summary>
    /// Creates an instance of <see cref="ValidationResult{TError}"/> representing the
    /// validation result obtained when validating an invalid item.
    /// </summary>
    /// <param name="errors">
    /// The collection of detected validation errors.
    /// This parameter cannot be <see langword="null"/>.
    /// The provided collection of validation errors must contain at least one item.
    /// </param>
    /// <returns>
    /// An instance of <see cref="ValidationResult{TError}"/> representing the
    /// validation result obtained when validating an invalid item.
    /// </returns>
    /// <remarks>
    /// An invalid item is an item for which the validation process has detected at least
    /// one validation error.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// When <paramref name="errors"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// When <paramref name="errors"/> is an empty sequence.
    /// </exception>
    public static ValidationResult<TError> Invalid(
      IEnumerable<TError> errors)
    {
      if (errors == null)
        throw new ArgumentNullException(nameof(errors));

      var errorsArray = errors.ToImmutableArray();
      if (errorsArray.IsEmpty)
      {
        throw new ArgumentException(
          "You must provide at least one validation error.",
          nameof(errors)
        );
      }

      return new ValidationResult<TError>(errorsArray);
    }

    /// <summary>
    /// Gets a flag indicating whether the validated item is a valid one.
    /// </summary>
    public bool IsValid => this.Errors.IsEmpty;

    /// <summary>
    /// Gets the collection of the detected validation errors.
    /// </summary>
    /// <remarks>
    /// An empty collection of validation errors means that the validated item
    /// is a valid one.
    /// </remarks>
    public ImmutableArray<TError> Errors { get; }

    private ValidationResult(ImmutableArray<TError> errors)
    {
      this.Errors = errors;
    }
  }
}
