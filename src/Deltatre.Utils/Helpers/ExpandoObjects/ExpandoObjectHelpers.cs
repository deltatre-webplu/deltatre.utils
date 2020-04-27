using Deltatre.Utils.Extensions.ExpandoObjects;
using System;
using System.Dynamic;

namespace Deltatre.Utils.Helpers.ExpandoObjects
{
  /// <summary>
  /// A collection of helper methods to work with instances of <see cref="ExpandoObject"/> class.
  /// </summary>
  public static class ExpandoObjectHelpers
  {
    /// <summary>
    /// Executes the shallow merge of <paramref name="source"/> over <paramref name="target"/>
    /// and returns a fresh new instance of <see cref="ExpandoObject"/> containing the result of
    /// the shallow merge operation.
    /// Parameter <paramref name="target"/> is used as the target of the shallow merge operation 
    /// and is not modified.
    /// Parameter <paramref name="source"/> is shallow merged over <paramref name="target"/>
    /// and is not modified.
    /// The returned object contains the result of the shallow merge operation.
    /// Only the top level properties of <paramref name="target"/> and <paramref name="source"/>
    /// are shallow merged.
    /// </summary>
    /// <param name="target">
    /// The object to be used as the target of the shallow merge operation.
    /// Cannot be <see langword="null"/>. This object is not modified.
    /// </param>
    /// <param name="source">
    /// The object to be merged over <paramref name="target"/>. Cannot be null.
    /// This object is not modified.
    /// </param>
    /// <returns>
    /// A fresh new instance of <see cref="ExpandoObject"/> containing the result of the shallow merge operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Throws <see cref="ArgumentNullException"/> when <paramref name="target"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Throws <see cref="ArgumentNullException"/> when <paramref name="source"/> is <see langword="null"/>.
    /// </exception>
    public static ExpandoObject ShallowMerge(ExpandoObject target, ExpandoObject source)
    {
      if (target == null)
        throw new ArgumentNullException(nameof(target));

      if (source == null)
        throw new ArgumentNullException(nameof(source));

      var result = target.ShallowClone();
      result.ShallowMergeWith(source);
      return result;
    }
  }
}
