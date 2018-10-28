﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Deltatre.Utils.Extensions.Dictionary
{
  /// <summary>
  /// Extension methods for dictionaries
  /// </summary>
  public static class DictionaryExtensions
  {
    ///<summary>Returns a read-only version of the <paramref name="source"/> dictionary</summary>
    ///<param name="source">A dictionary object</param>
    ///<returns cref="System.Collections.ObjectModel.ReadOnlyDictionary{TKey, TValue}">
    ///A read-only version of <paramref name="source"/>
    ///</returns>
    /// <exception cref="ArgumentNullException"> Throws ArgumentNullException when parameter source is null </exception>
    public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(source));

      return new ReadOnlyDictionary<TKey, TValue>(source);
    }

    /// <summary>
    /// Returns the relative <c>string</c> value of <paramref name="key"/> or
    /// <c>null</c> if <paramref name="source"/> is <c>null</c> or <paramref name="key"/> does not exist inside <paramref name="source"/>.
    /// </summary>
    /// <param name="source">A dictionary object</param>
    /// <param name="key">A key of the dictionary</param>
    /// <returns>The relative <c>string</c> value of <paramref name="key"/> or
    /// <c>null</c> if <paramref name="key"/> does not exist inside <paramref name="source"/>.</returns>
    /// <exception cref="ArgumentNullException"> Throws ArgumentNullException when parameter source is null </exception>
    public static string GetStringOrDefault(this IDictionary<string, object> source, string key)
    {
      return GetValueOrDefault<string>(source, key);
    }

    /// <summary>
    /// Returns the relative <c>bool</c> value of <paramref name="key"/> or
    /// <c>false</c> if <paramref name="source"/> is <c>null</c> or <paramref name="key"/> does not exist inside <paramref name="source"/>.
    /// </summary>
    /// <param name="source">A dictionary object</param>
    /// <param name="key">A key of the dictionary</param>
    /// <returns>
    /// The relative <c>bool</c> value of <paramref name="key"/> or
    /// <c>false</c> if <paramref name="key"/> does not exist
    /// </returns>
    /// <exception cref="ArgumentNullException"> Throws ArgumentNullException when parameter source is null </exception>
    public static bool GetBoolOrDefault(this IDictionary<string, object> source, string key)
    {
      return GetValueOrDefault<bool>(source, key);
    }

    /// <summary>
    /// Returns the relative <c>int</c> value of <paramref name="key"/> or
    /// 0 if <paramref name="source"/> is <c>null</c> or <paramref name="key"/> does not exist inside <paramref name="source"/>.
    /// </summary>
    /// <param name="source">A dictionary object</param>
    /// <param name="key">A key of the dictionary</param>
    /// <returns>The relative <c>int</c> value of <paramref name="key"/> or
    /// 0 if <paramref name="key"/> does not exist inside <paramref name="source"/>.</returns>
    /// <exception cref="ArgumentNullException"> Throws ArgumentNullException when parameter source is null </exception>
    public static int GetIntOrDefault(this IDictionary<string, object> source, string key)
    {
      return GetValueOrDefault<int>(source, key);
    }

    /// <summary>
    /// <c>Returns</c> the relative value of <paramref name="key"/> or
    /// its default value for the type <typeparamref name="T"/> <c>if</c> <paramref name="source"/> is <c>null</c> or the key does not exist inside <paramref name="source"/>.
    /// </summary>
    /// <typeparam name="T">The desired type of the returning value</typeparam>
    /// <param name="source">A Dictionary object</param>
    /// <param name="key">A key of the dictionary</param>
    /// <returns>
    /// <para>The relative value of <paramref name="key"/> or</para>
    /// <para>its default value if the key does not exist inside <paramref name="source"/>.</para>
    /// </returns>
    /// <exception cref="ArgumentNullException"> Throws ArgumentNullException when parameter source is null </exception>
    public static T GetValueOrDefault<T>(this IDictionary<string, object> source, string key)
    {
      if (source == null)
      {
        throw new ArgumentNullException("The source dictionary is null");
      }
      try
      {
        if (source.ContainsKey(key))
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