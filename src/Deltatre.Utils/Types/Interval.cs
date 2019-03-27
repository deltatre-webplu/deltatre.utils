using System;

namespace Deltatre.Utils.Types
{
  /// <summary>
  /// This class represents an interval of values. 
  /// </summary>
  /// <typeparam name="T">Type of values in the range. They must be struct and implement <see cref="IComparable{T}"/> and <see cref="IEquatable{T}"/></typeparam>
  public sealed class Interval<T> : IEquatable<Interval<T>>
    where T : struct, IComparable<T>, IEquatable<T>
  {
    /// <summary>
    /// Represents an empty interval
    /// </summary>
    public static readonly Interval<T> Empty = new Interval<T>(Endpoint<T>.Exclusive(default(T)), Endpoint<T>.Exclusive(default(T)));

    /// <summary>
    /// Left endpoint of the interval
    /// </summary>
    public Endpoint<T> From { get; }

    /// <summary>
    /// Right endpoint of the interval
    /// </summary>
    public Endpoint<T> To { get; }

    /// <summary>
    /// Constructs an object of type <see cref="Interval{T}"/>
    /// <paramref name="from"/> must be lesser than or equal <paramref name="to"/>
    /// </summary>
    /// <param name="from">Left endpoint of the interval</param>
    /// <param name="to">Right endpoint of the interval</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="from"/> is greater than <paramref name="to"/></exception>
    public Interval(Endpoint<T> from, Endpoint<T> to)
    {
      if (from > to)
      {
        throw new ArgumentException($"{nameof(from)} cannot be greater than {nameof(to)}");
      }

      From = from;
      To = to;
    }

    /// <summary>
    /// Prints a representation of <see cref="Interval{T}"/>
    /// </summary>
    /// <returns>The string representation of the interval</returns>
    public override string ToString()
    {
      var fromEndpointSymbol = From.Status == EndPointStatus.Inclusive ? '[' : '(';
      var toEndpointSymbol = To.Status == EndPointStatus.Inclusive ? ']' : ')';

      return $"{fromEndpointSymbol}{From.Value}, {To.Value}{toEndpointSymbol}";
    }

    #region Equality

    /// <summary>
    /// Determines whether two instances of <see cref="Interval{T}"/> are equal.
    /// Two <see cref="Interval{T}"/> are equal if both <see cref="From"/> and <see cref="To"/> endpoints are equal
    /// </summary>
    /// <param name="other">An object to compare the current instance with</param>
    /// <returns>Boolean indicating whether the two instances are considered equals</returns>
    public bool Equals(Interval<T> other)
    {
      if (other == null)
      {
        return false;
      }

      return From == other.From && To == other.To;
    }

    /// <summary>
    /// Determines whether two instances of <see cref="Interval{T}"/> are equal.
    /// Two <see cref="Interval{T}"/> are equal if both <see cref="From"/> and <see cref="To"/> endpoints are equal
    /// </summary>
    /// <param name="obj">An object to compare the current instance with</param>
    /// <returns>Boolean indicating whether the two instances are considered equals</returns>
    public override bool Equals(object obj)
    {
      return obj is Interval<T> interval && Equals(interval);
    }

    /// <summary>
    /// Compares two <see cref='Interval{T}'/> objects. The result specifies whether they are equal.
    /// </summary>
    /// <returns>Boolean indicating whether the two instances are considered equals</returns>
    public static bool operator ==(Interval<T> x, Interval<T> y)
    {
      if (ReferenceEquals(x, y))
        return true;

      if (ReferenceEquals(x, null))
        return false;

      if (ReferenceEquals(y, null))
        return false;

      return x.Equals(y);
    }

    /// <summary>
    /// Compares two <see cref='Interval{T}'/> objects. The result specifies whether they are not equal.
    /// </summary>
    /// <returns>Boolean indicating whether the two instances are considered not equals</returns>
    public static bool operator !=(Interval<T> x, Interval<T> y)
    {
      return !(x == y);
    }

    /// <summary>
    /// Generate an hash code for an instance of <see cref="Interval{T}"/>.
    /// </summary>
    /// <returns>Returns the generated hash code</returns>
    public override int GetHashCode()
    {
      return From.GetHashCode() ^ To.GetHashCode();
    }

    #endregion
  }
}
