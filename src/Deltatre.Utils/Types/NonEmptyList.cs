using System;
using System.Collections;
using System.Collections.Generic;

namespace Deltatre.Utils.Types
{
  /// <inheritdoc />
  /// <summary>
  /// This type represents a non empty and read-only list of items.
  /// </summary>
  /// <typeparam name="T">The type of items contained into the sequence</typeparam>
  public sealed class NonEmptyList<T> : IReadOnlyList<T>
  {
    public T this[int index] => throw new NotImplementedException();

    public int Count => throw new NotImplementedException();

    public IEnumerator<T> GetEnumerator()
    {
      throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }
  }
}
