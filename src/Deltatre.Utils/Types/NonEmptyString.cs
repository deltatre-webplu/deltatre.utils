using System;

namespace Deltatre.Utils.Types
{
  /// <summary>
  /// This type wraps a string which is guaranteed to be neither null nor white space
  /// </summary>
  public struct NonEmptyString
  {
    /// <summary>
    /// Implicit conversion from <see cref="NonEmptyString"/> to <see cref="string"/>
    /// </summary>
    /// <param name="nonEmptyString">The instance of <see cref="NonEmptyString"/> to be converted</param>
    public static implicit operator string(NonEmptyString nonEmptyString)
    {
      return nonEmptyString.Value;
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

    /// <summary>Indicates whether this instance and a specified object are equal.</summary>
    /// <param name="obj">The object to compare with the current instance. </param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />. </returns>
    public override bool Equals(object obj)
    {
      if (!(obj is NonEmptyString))
      {
        return false;
      }

      var other = (NonEmptyString)obj;
      return this.Value == other.Value;
    }

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode()
    {
      unchecked
      {
        int hash = 17;
        hash = (hash * 23) + (this.Value == null ? 0 : this.Value.GetHashCode());
        return hash;
      }
    }

    /// <summary>
    /// Compares two instances of <see cref="NonEmptyString"/> for equality
    /// </summary>
    /// <param name="left">An instance of <see cref="NonEmptyString"/></param>
    /// <param name="right">An instance of <see cref="NonEmptyString"/></param>
    /// <returns></returns>
    public static bool operator ==(NonEmptyString left, NonEmptyString right)
    {
      return left.Equals(right);
    }

    /// <summary>
    /// Compares two instances of <see cref="NonEmptyString"/> for inequality
    /// </summary>
    /// <param name="left">An instance of <see cref="NonEmptyString"/></param>
    /// <param name="right">An instance of <see cref="NonEmptyString"/></param>
    /// <returns></returns>
    public static bool operator !=(NonEmptyString left, NonEmptyString right)
    {
      return !(left == right);
    }
  }
}
