using System;

namespace Deltatre.Utils.Types
{
  /// <summary>
  /// This type wraps a string which is guaranteed to be neither null nor white space
  /// </summary>
  public sealed class NonEmptyString
  {
    /// <summary>
    /// Implicit conversion from <see cref="NonEmptyString"/> to <see cref="string"/>
    /// </summary>
    /// <param name="nonEmptyString">The instance of <see cref="NonEmptyString"/> to be converted</param>
    public static implicit operator string(NonEmptyString nonEmptyString)
    {
      return nonEmptyString?.Value;
    }

    /// <summary>
    /// Explicit conversion from <see cref="string"/> to <see cref="NonEmptyString"/>
    /// </summary>
    /// <param name="value">The instance of <see cref="string"/> to be converted</param>
    /// <exception cref="InvalidCastException">Throws <see cref="InvalidCastException"/> when <paramref name="value"/> is null or white space</exception>
    public static explicit operator NonEmptyString(string value)
    {
      try
      {
        return new NonEmptyString(value);
      }
      catch (ArgumentException ex)
      {
        throw new InvalidCastException($"Unable to convert the provided string to {typeof(NonEmptyString).Name}", ex);
      }
    }

    /// <summary>
    /// Creates new instance of <see cref="NonEmptyString"/>
    /// </summary>
    /// <param name="value">The string to be wrapped</param>
    /// <exception cref="ArgumentException">Throws <see cref="ArgumentException"/> when parameter <paramref name="value"/> is null or white space</exception>
    public NonEmptyString(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException($"Parameter {nameof(value)} cannot be null or white space", nameof(value));

      this.Value = value;
    }

    /// <summary>
    /// Gets the wrapped string
    /// </summary>
    public string Value { get; }
  }
}
