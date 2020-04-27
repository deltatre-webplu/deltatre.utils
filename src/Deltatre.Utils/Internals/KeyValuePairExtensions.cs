using System.Collections.Generic;

namespace Deltatre.Utils.Internals
{
  internal static class KeyValuePairExtensions
  {
    internal static void Deconstruct<TKey, TValue>(
      this KeyValuePair<TKey, TValue> source,
      out TKey key,
      out TValue value)
    {
      key = source.Key;
      value = source.Value;
    }
  }
}
