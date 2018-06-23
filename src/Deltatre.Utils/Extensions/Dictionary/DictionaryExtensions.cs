using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Deltatre.Utils.Extensions.Dictionary
{
	/// <summary>
	/// Extension methods for dictionaries
	/// </summary>
	public static class DictionaryExtensions
	{
		/// <exception cref="ArgumentNullException"> Throws ArgumentNullException when parameter source is null </exception>
		public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> source)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			return new ReadOnlyDictionary<TKey, TValue>(source);
		}
	}
}
