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

    /// <summary>
    /// <para><c>Returns</c> the relative value of the dictionary <paramref name="key"/> or
    /// its default value for the type <c>if</c> the dictionary is null or the key does not exist</para>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T">The desired type of the returning value</typeparam>
    /// <param name="source">A Dictionary object</param>
    /// <param name="key">A key of the dictionary</param>
    /// <returns>
    /// <para>The relative value of the dictionary key or</para>
    /// <para>its default value if <paramref name="source"/> is <c>null</c> or the key does not exist</para>
    /// </returns>
    public static T GetValueOrDefault<T>(this IDictionary<string, object> source, string key)
    {
      try
      {
        if (source != null && (source.ContainsKey(key)))
        {
          return (T)Convert.ChangeType(source[key], typeof(T));
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