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

    public static string GetStringOrDefault(this IDictionary<string, object> fields, string propertyName)
    {
      return GetValueOrDefault<string>(fields, propertyName);
    }

    public static bool GetBoolOrDefault(this IDictionary<string, object> fields, string propertyName)
    {
      return GetValueOrDefault<bool>(fields, propertyName);
    }

    public static int GetIntOrDefault(this IDictionary<string, object> fields, string propertyName)
    {
      return GetValueOrDefault<int>(fields, propertyName);
    }

    public static T GetValueOrDefault<T>(this IDictionary<string, object> fields, string propertyName)
    {
      try
      {
        if (fields != null && (fields.ContainsKey(propertyName)))
        {
          return (T)Convert.ChangeType(fields[propertyName], typeof(T));
        }
        return default(T);
      }
      catch
      {
        return default(T);
      }
    }
  }
}
