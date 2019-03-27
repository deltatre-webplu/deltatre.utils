using System;

namespace Deltatre.Utils.Types
{
  /// <summary>
  /// This class represents the endpoint of an interval. It stores a value and whether it is included or excluded from the interval.
  /// </summary>
  /// <typeparam name="T">The type of value</typeparam>
  public struct Endpoint<T> : IComparable, IComparable<Endpoint<T>>, IEquatable<Endpoint<T>>
    where T : struct, IComparable<T>, IEquatable<T>
  {
    /// <summary>
    /// Constructs an inclusive <see cref="Endpoint{T}"/> of value <paramref name="value"/>
    /// </summary>
    /// <param name="value">The value of the endpoint</param>
    /// <returns>Returns an inclusive <see cref="Endpoint{T}"/> of value <paramref name="value"/></returns>
    public static Endpoint<T> Inclusive(T value)
    {
      return new Endpoint<T>(value, EndPointStatus.Inclusive);
    }

    /// <summary>
    /// Constructs an exclusive <see cref="Endpoint{T}"/> of value <paramref name="value"/>
    /// </summary>
    /// <param name="value">The value of the endpoint</param>
    /// <returns>Returns an exclusive <see cref="Endpoint{T}"/> of value <paramref name="value"/></returns>
    public static Endpoint<T> Exclusive(T value)
    {
      return new Endpoint<T>(value, EndPointStatus.Exclusive);
    }

    private Endpoint(T value, EndPointStatus status)
    {
      Value = value;
      Status = status;
    }

    /// <summary>
    /// Value of the endpoint
    /// </summary>
    public T Value { get; }

    /// <summary>
    /// Status of the endpoint. It could be <see cref="EndPointStatus.Inclusive"/> or <see cref="EndPointStatus.Exclusive"/>
    /// </summary>
    public EndPointStatus Status { get; }

    #region Equality

    /// <summary>
    /// Determines whether two instances of <see cref="Endpoint{T}"/> are equal.
    /// Two <see cref="Endpoint{T}"/> are equal if both <see cref="Value"/> and <see cref="Status"/> are equal
    /// </summary>
    /// <param name="other">An object to compare the current instance with</param>
    /// <returns>Boolean indicating whether the two instances are considered equals</returns>
    public bool Equals(Endpoint<T> other)
    {
      return Value.Equals(other.Value) && Status == other.Status;
    }

    /// <summary>
    /// Determines whether two instances of <see cref="Endpoint{T}"/> are equal.
    /// Two <see cref="Endpoint{T}"/> are equal if both <see cref="Value"/> and <see cref="Status"/> are equal
    /// </summary>
    /// <param name="obj">An object to compare the current instance with</param>
    /// <returns>Boolean indicating whether the two instances are considered equals</returns>
    public override bool Equals(object obj)
    {
      return obj is Endpoint<T> endpoint && Equals(endpoint);
    }

    /// <summary>
    /// Compares two <see cref='Endpoint{T}'/> objects. The result specifies whether they are equal.
    /// </summary>
    /// <returns>Boolean indicating whether the two instances are considered equals</returns>
    public static bool operator ==(Endpoint<T> x, Endpoint<T> y)
    {
      return x.Equals(y);
    }

    /// <summary>
    /// Compares two <see cref='Endpoint{T}'/> objects. The result specifies whether they are not equal.
    /// </summary>
    /// <returns>Boolean indicating whether the two instances are considered not equals</returns>
    public static bool operator !=(Endpoint<T> x, Endpoint<T> y)
    {
      return !x.Equals(y);
    }

    /// <summary>
    /// Generate an hash code for an instance of <see cref="Endpoint{T}"/>.
    /// </summary>
    /// <returns>Returns the generated hash code</returns>
    public override int GetHashCode()
    {
      return Value.GetHashCode() ^ Status.GetHashCode();
    }

    #endregion

    #region Comparison

    /// <summary>
    /// Compares this instance of <see cref="Endpoint{T}"/> with <paramref name="obj"/>
    /// </summary>
    /// <param name="obj">The object to compare this instance with</param>
    /// <returns>
    /// 1 : if this instance is greater than <paramref name="obj"/>
    /// 0 : if this instance is equal to <paramref name="obj"/>
    /// -1 : if this instance is lesser than <paramref name="obj"/>
    /// </returns>
    public int CompareTo(object obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof(obj));

      if (!(obj is Endpoint<T>))
        throw new ArgumentException($"Given object is not {nameof(Endpoint<T>)}");

      return CompareTo((Endpoint<T>)obj);
    }

    /// <summary>
    /// Compares this instance of <see cref="Endpoint{T}"/> with <paramref name="other"/>
    /// </summary>
    /// <param name="other">The object to compare this instance with</param>
    /// <returns>
    /// 1 : if this instance is greater than <paramref name="other"/>
    /// 0 : if this instance is equal to <paramref name="other"/>
    /// -1 : if this instance is lesser than <paramref name="other"/>
    /// </returns>
    public int CompareTo(Endpoint<T> other)
    {
      return Value.CompareTo(other.Value);
    }

    /// <summary>
    /// Lesser than operator
    /// </summary>
    /// <param name="x">left side operand</param>
    /// <param name="y">right side operand</param>
    /// <returns>Returns a bool indicating whether or not <paramref name="x"/> is lesser than <paramref name="y"/></returns>
    public static bool operator <(Endpoint<T> x, Endpoint<T> y)
    {
      return x.CompareTo(y) < 0;
    }

    /// <summary>
    /// Lesser than or equal operator
    /// </summary>
    /// <param name="x">left side operand</param>
    /// <param name="y">right side operand</param>
    /// <returns>Returns a bool indicating whether or not <paramref name="x"/> is lesser than or equal <paramref name="y"/></returns>
    public static bool operator <=(Endpoint<T> x, Endpoint<T> y)
    {
      return x.CompareTo(y) <= 0;
    }

    /// <summary>
    /// Greater than operator
    /// </summary>
    /// <param name="x">left side operand</param>
    /// <param name="y">right side operand</param>
    /// <returns>Returns a bool indicating whether or not <paramref name="x"/> is greater than <paramref name="y"/></returns>
    public static bool operator >(Endpoint<T> x, Endpoint<T> y)
    {
      return x.CompareTo(y) > 0;
    }

    /// <summary>
    /// Greater than or equal operator
    /// </summary>
    /// <param name="x">left side operand</param>
    /// <param name="y">right side operand</param>
    /// <returns>Returns a bool indicating whether or not <paramref name="x"/> is greater than or equal <paramref name="y"/></returns>
    public static bool operator >=(Endpoint<T> x, Endpoint<T> y)
    {
      return x.CompareTo(y) >= 0;
    }

    #endregion

    /// <summary>
    /// Prints a representation of <see cref="Endpoint{T}"/>
    /// </summary>
    /// <returns>The string representation of the endpoint</returns>
    public override string ToString()
    {
      return $"Endpoint - value: {Value}, status: {Status}";
    }
  }

  /// <summary>
  /// This enum contains the possible statuses of an endpoint. 
  /// </summary>
  public enum EndPointStatus
  {
    /// <summary>
    /// The value of the endpoint is inclusive
    /// </summary>
    Inclusive = 0,

    /// <summary>
    /// The value of the endpoint is exclusive
    /// </summary>
    Exclusive = 1
  }
}
