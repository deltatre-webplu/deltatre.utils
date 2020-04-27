using System;
using System.Collections.Generic;
using System.Dynamic;
using Deltatre.Utils.Internals;

namespace Deltatre.Utils.Extensions.ExpandoObjects
{
  /// <summary>
  /// A collection of extension methods for the <see cref="ExpandoObject"/> class.
  /// </summary>
  public static class ExpandoObjectExtensions
  {
    /// <summary>
    /// Performs a top level shallow merge between the current instance and the provided <see cref="ExpandoObject"/>.
    /// The current instance is used as the target object and is modified.
    /// The provided <see cref="ExpandoObject"/> is shallow merged over the current instance. Only the top level properties are shallow merged.
    /// </summary>
    /// <param name="target">
    /// The object to be used as the target object for the shallow merge operation.
    /// This object is modified by the shallow merge operation.
    /// Cannot be <see langword="null"/>.
    /// </param>
    /// <param name="other">
    /// The object to be shallow merged over <paramref name="target"/>.
    /// Cannot be <see langword="null"/>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Throws <see cref="ArgumentNullException"/> when <paramref name="target"/> is <see langword="null"/>
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Throws <see cref="ArgumentNullException"/> when <paramref name="other"/> is <see langword="null"/>
    /// </exception>
    public static void ShallowMergeWith(
      this ExpandoObject target,
      ExpandoObject other)
    {
      if (other == null)
        throw new ArgumentNullException(nameof(other));

      if (target == null)
        throw new ArgumentNullException(nameof(target));

      var targetAsDict = (IDictionary<string, object>)target;
      var otherAsDict = (IDictionary<string, object>)other;

      foreach (var key in otherAsDict.Keys)
      {
        targetAsDict[key] = otherAsDict[key];
      }
    }

    /// <summary>
    /// Returns a shallow clone of the current instance.
    /// </summary>
    /// <param name="source">
    /// The object to be shallow cloned. Cannot be <see langword="null"/>.
    /// </param>
    /// <returns>
    /// A shallow clone of the current instance.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Throws <see cref="ArgumentNullException" /> when <paramref name="source"/> is <see langword="null"/>.
    /// </exception>
    public static ExpandoObject ShallowClone(this ExpandoObject source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(source));

      var result = new ExpandoObject();
      var resultAsDict = (IDictionary<string, object>)result;

      foreach (var (key, value) in source)
      {
        resultAsDict[key] = value;
      }

      return result;
    }
  }
}
