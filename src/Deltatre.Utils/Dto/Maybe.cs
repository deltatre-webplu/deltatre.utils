using System;

namespace Deltatre.Utils.Dto
{
  /// <summary>
  /// This type is a wrapper around a value.
  /// The wrapper can have a value inside it or it can be empty. 
  /// The wrapped value can be read if and only if it is
  /// available inside the wrapper.
  /// The wrapper can be used as a placeholder for the value in all the scenarios where the value
  /// itself can possibly be missing.
  /// </summary>
  /// <remarks>
  /// A typical use case for this type is a method which fetches an item from a database.
  /// The searched item can be available or not inside the database.
  /// You can use Maybe as the return type for the method.
  /// </remarks>
  /// <typeparam name="T">
  /// The type of the wrapped value.
  /// </typeparam>
  public sealed class Maybe<T>
  {
    /// <summary>
    /// Represents the empty <see cref="Maybe{T}"/> instance.
    /// This field is read-only.
    /// </summary>
    /// <remarks>
    /// Use this field each time you want to return an empty <see cref="Maybe{T}"/> instance.
    /// </remarks>
    public static readonly Maybe<T> None = new Maybe<T>(default, false);

    private readonly T _value;

    /// <summary>
    /// Initializes a new instance of <see cref="Maybe{T}"/> class.
    /// </summary>
    /// <param name="value">
    /// The value wrapped by the <see cref="Maybe{T}"/> instance.
    /// </param>
    /// <remarks>
    /// By using this constructor you always get a non empty <see cref="Maybe{T}"/> instance.
    /// </remarks>
    public Maybe(T value): this(value, true)
    {
    }

    private Maybe(T value, bool hasValue)
    {
      _value = value;
      HasValue = hasValue;
    }

    /// <summary>
    /// Gets a flag indicating whether the current instance is non empty.
    /// An instance of <see cref="Maybe{T}"/> is non empty if and only if
    /// it actually wraps a value.
    /// </summary>
    public bool HasValue { get; }


    /// <summary>
    /// Gets the wrapped value.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// When the current instance is an empty instance.
    /// </exception>
    /// <remarks>
    /// Reading the wrapped value is allowed if and only if the current instance is non empty.
    /// When trying to read the wrapped value from an empty instance an <see cref="InvalidOperationException"/>
    /// is thrown.
    /// </remarks>
    public T Value
    {
      get 
      {
        if (!this.HasValue) 
        {
          throw new InvalidOperationException("Accessing the value of an empty Maybe instance is not allowed");
        }

        return this._value;
      }
    }
  }
}
