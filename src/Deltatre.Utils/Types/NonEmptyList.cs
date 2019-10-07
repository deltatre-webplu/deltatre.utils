using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Deltatre.Utils.Types
{
  /// <inheritdoc />
  /// <summary>
  /// This type represents a non empty and read-only list of items.
  /// </summary>
  /// <typeparam name="T">The type of items contained into the sequence</typeparam>
  public sealed class NonEmptyList<T> : IReadOnlyList<T>
  {
    private readonly List<T> _items;

    /// <summary>
    /// Initializes a new instance of the <see cref="NonEmptyList{T}"/> class.
    /// </summary>
    /// <param name="items">The items contained into the newly created instance of <see cref="NonEmptyList{T}"/>.</param>
    /// <exception cref="ArgumentNullException">Throws <see cref="ArgumentNullException"/> when <paramref name="items"/> is null</exception>
    /// <exception cref="ArgumentException">Throws <see cref="ArgumentException"/> when <paramref name="items"/> is an empty sequence</exception>
    public NonEmptyList(IEnumerable<T> items)
    {
      if (items == null)
        throw new ArgumentNullException(nameof(items));

      var itemsList = items.ToList();

      if (itemsList.Count == 0)
        throw new ArgumentException($"Parameter '{nameof(items)}' cannot be an empty sequence.");

      _items = itemsList;
    }

    /// <summary>Gets the element at the specified index in the read-only list.</summary>
    /// <param name="index">The zero-based index of the element to get.</param>
    /// <returns>The element at the specified index in the read-only list.</returns>
    public T this[int index] => _items[index];


    /// <summary>
    /// Gets the number of elements in the collection.
    /// </summary>
    public int Count => _items.Count;

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)_items).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_items).GetEnumerator();
  }
}
