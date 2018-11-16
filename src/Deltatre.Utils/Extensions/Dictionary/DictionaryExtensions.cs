using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

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
    /// <exception cref="ArgumentNullException"> Throws <see cref="ArgumentNullException"/> when parameter <paramref name="source"/> is null</exception>
    public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(source));

      return new ReadOnlyDictionary<TKey, TValue>(source);
    }

    /// <summary>Call this method if you want to convert a dictionary value to the type <see cref="string"/> in a safe way. The default value of type <see cref="string"/> will be returned if either the source dictionary does not contain the provided key or the value cannot be converted to type <see cref="string"/>. The current thread's culture will be used for the culture-specific aspects of the type conversion.</summary>
    /// <param name="source">The source dictionary</param>
    /// <param name="key">The dictionary key for which you want the value converted to type <see cref="string"/></param>
    /// <returns>The value of source dictionary corresponding to the provided key converted to type <see cref="string"/>. If the conversion cannot be performed or the source dictionary doesn't contain the provided key then the default value of type <see cref="string"/> is returned</returns>
    /// <exception cref="ArgumentNullException"> Throws <see cref="ArgumentNullException"/> when either parameter <paramref name="key"/> or <paramref name="source"/> is null</exception>
    public static string GetStringOrDefault(this IDictionary<string, object> source, string key) =>
      GetValueOrDefault<string>(
        source,
        key);

    /// <summary>Call this method if you want to convert a dictionary value to the type <see cref="string"/> in a safe way. The default value of type <see cref="string"/> will be returned if either the source dictionary does not contain the provided key or the value cannot be converted to type <see cref="string"/>.</summary>
    /// <param name="source">The source dictionary</param>
    /// <param name="key">The dictionary key for which you want the value converted to type <see cref="string"/></param>
    /// <param name="provider">An object that supplies culture-specific formatting information</param>
    /// <returns>The value of source dictionary corresponding to the provided key converted to type <see cref="string"/>. If the conversion cannot be performed or the source dictionary doesn't contain the provided key then the default value of type <see cref="string"/> is returned</returns>
    /// <exception cref="ArgumentNullException"> Throws <see cref="ArgumentNullException"/> when either parameter <paramref name="key"/> or <paramref name="provider"/> or <paramref name="source"/> is null</exception>
    public static string GetStringOrDefault(this IDictionary<string, object> source, string key, IFormatProvider provider) =>
      GetValueOrDefault<string>(
        source,
        key,
        provider);

    /// <summary>Call this method if you want to convert a dictionary value to the type <see cref="bool"/> in a safe way. The default value of type <see cref="bool"/> will be returned if either the source dictionary does not contain the provided key or the value cannot be converted to type <see cref="bool"/>. The current thread's culture will be used for the culture-specific aspects of the type conversion.</summary>
    /// <param name="source">The source dictionary</param>
    /// <param name="key">The dictionary key for which you want the value converted to type <see cref="bool"/></param>
    /// <returns>The value of source dictionary corresponding to the provided key converted to type <see cref="bool"/>. If the conversion cannot be performed or the source dictionary doesn't contain the provided key then the default value of type <see cref="bool"/> is returned</returns>
    /// <exception cref="ArgumentNullException"> Throws <see cref="ArgumentNullException"/> when either parameter <paramref name="key"/> or <paramref name="source"/> is null</exception>
    public static bool GetBoolOrDefault(this IDictionary<string, object> source, string key) =>
      GetValueOrDefault<bool>(
        source,
        key,
        Thread.CurrentThread.CurrentCulture);

    /// <summary>Call this method if you want to convert a dictionary value to the type <see cref="bool"/> in a safe way. The default value of type <see cref="bool"/> will be returned if either the source dictionary does not contain the provided key or the value cannot be converted to type <see cref="bool"/>.</summary>
    /// <param name="source">The source dictionary</param>
    /// <param name="key">The dictionary key for which you want the value converted to type <see cref="bool"/></param>
    /// <param name="provider">An object that supplies culture-specific formatting information</param>
    /// <returns>The value of source dictionary corresponding to the provided key converted to type <see cref="bool"/>. If the conversion cannot be performed or the source dictionary doesn't contain the provided key then the default value of type <see cref="bool"/> is returned</returns>
    /// <exception cref="ArgumentNullException"> Throws <see cref="ArgumentNullException"/> when either parameter <paramref name="key"/> or <paramref name="provider"/> or <paramref name="source"/> is null</exception>
    public static bool GetBoolOrDefault(this IDictionary<string, object> source, string key, IFormatProvider provider) =>
      GetValueOrDefault<bool>(
        source,
        key,
        provider);

    /// <summary>Call this method if you want to convert a dictionary value to the type <see cref="int"/> in a safe way. The default value of type <see cref="int"/> will be returned if either the source dictionary does not contain the provided key or the value cannot be converted to type <see cref="int"/>. The current thread's culture will be used for the culture-specific aspects of the type conversion.</summary>
    /// <param name="source">The source dictionary</param>
    /// <param name="key">The dictionary key for which you want the value converted to type <see cref="int"/></param>
    /// <returns>The value of source dictionary corresponding to the provided key converted to type <see cref="int"/>. If the conversion cannot be performed or the source dictionary doesn't contain the provided key then the default value of type <see cref="int"/> is returned</returns>
    /// <exception cref="ArgumentNullException"> Throws <see cref="ArgumentNullException"/> when either parameter <paramref name="key"/> or <paramref name="source"/> is null</exception>
    public static int GetIntOrDefault(this IDictionary<string, object> source, string key) =>
      GetValueOrDefault<int>(
        source,
        key,
        Thread.CurrentThread.CurrentCulture);

    /// <summary>Call this method if you want to convert a dictionary value to the type <see cref="int"/> in a safe way. The default value of type <see cref="int"/> will be returned if either the source dictionary does not contain the provided key or the value cannot be converted to type <see cref="int"/>.</summary>
    /// <param name="source">The source dictionary</param>
    /// <param name="key">The dictionary key for which you want the value converted to type <see cref="int"/></param>
    /// <param name="provider">An object that supplies culture-specific formatting information</param>
    /// <returns>The value of source dictionary corresponding to the provided key converted to type <see cref="int"/>. If the conversion cannot be performed or the source dictionary doesn't contain the provided key then the default value of type <see cref="int"/> is returned</returns>
    /// <exception cref="ArgumentNullException"> Throws <see cref="ArgumentNullException"/> when either parameter <paramref name="key"/> or <paramref name="provider"/> or <paramref name="source"/> is null</exception>
    public static int GetIntOrDefault(this IDictionary<string, object> source, string key, IFormatProvider provider) =>
      GetValueOrDefault<int>(
        source,
        key,
        provider);

    /// <summary>Call this method if you want to convert a dictionary value to the type <typeparamref name="T"/> in a safe way. The default value of type <typeparamref name="T"/> will be returned if either the source dictionary does not contain the provided key or the value cannot be converted to type <typeparamref name="T"/>. The current thread's culture will be used for the culture-specific aspects of the type conversion.</summary>
    /// <typeparam name="T">The desired type of the returning value</typeparam>
    /// <param name="source">The source dictionary</param>
    /// <param name="key">The dictionary key for which you want the value converted to type <typeparamref name="T"/></param>
    /// <returns>The value of source dictionary corresponding to the provided key converted to type <typeparamref name="T"/>. If the conversion cannot be performed or the source dictionary doesn't contain the provided key then the default value of type <typeparamref name="T"/> is returned</returns>
    /// <exception cref="ArgumentNullException"> Throws <see cref="ArgumentNullException"/> when either parameter <paramref name="key"/> or <paramref name="source"/> is null</exception>
    public static T GetValueOrDefault<T>(this IDictionary<string, object> source, string key) =>
      GetValueOrDefault<T>(
        source,
        key,
        Thread.CurrentThread.CurrentCulture);

    /// <summary>Call this method if you want to convert a dictionary value to the type <typeparamref name="T"/> in a safe way. The default value of type <typeparamref name="T"/> will be returned if either the source dictionary does not contain the provided key or the value cannot be converted to type <typeparamref name="T"/></summary>
    /// <typeparam name="T">The desired type of the returning value</typeparam>
    /// <param name="source">The source dictionary</param>
    /// <param name="key">The dictionary key for which you want the value converted to type <typeparamref name="T"/></param>
    /// <param name="provider">An object that supplies culture-specific formatting information</param>
    /// <returns>The value of source dictionary corresponding to the provided key converted to type <typeparamref name="T"/>. If the conversion cannot be performed or the source dictionary doesn't contain the provided key then the default value of type <typeparamref name="T"/> is returned</returns>
    /// <exception cref="ArgumentNullException"> Throws <see cref="ArgumentNullException"/> when either parameter <paramref name="key"/> or <paramref name="provider"/> or <paramref name="source"/> is null</exception>
    public static T GetValueOrDefault<T>(
      this IDictionary<string, object> source,
      string key,
      IFormatProvider provider)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(source));

      if (key == null)
        throw new ArgumentNullException(nameof(key));

      if (provider == null)
        throw new ArgumentNullException(nameof(provider));

      var sourceContainsKey = source.TryGetValue(key, out var value);
      if(!sourceContainsKey)
        return default(T);

      try
      {
        return (T)Convert.ChangeType(value, typeof(T), provider);
      }
      catch
      {
        return default(T);
      }
    }
  }
}