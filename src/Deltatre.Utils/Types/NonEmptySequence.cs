using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Deltatre.Utils.Types
{
	/// <inheritdoc />
	/// <summary>
	/// This type represents a non empty sequence of items
	/// </summary>
	/// <typeparam name="T">The type of items contained into the sequence</typeparam>
	public sealed class NonEmptySequence<T>: IEnumerable<T>
	{
		private readonly T[] _items;

		/// <exception cref="ArgumentNullException">Throws ArgumentNullException when parameter items is null</exception>
		public NonEmptySequence(IEnumerable<T> items)
		{
			if (items == null)
				throw new ArgumentNullException(nameof(items));

			var itemsArray = items.ToArray();

			if (!itemsArray.Any())
				throw new ArgumentException($"Parameter '{nameof(items)}' cannot be an empty sequence.");

			_items = itemsArray;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>) _items).GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
	}
}
