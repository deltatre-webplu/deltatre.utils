using System;
using System.Linq;

namespace Deltatre.Utils.Reflection
{
  /// <summary>
  /// A collection of helper methods for common reflection tasks
  /// </summary>
  public static class ReflectionHelpers
  {
    /// <summary>
    /// Checks whether a given instance of <see cref="System.Type"/> represents a decorator for the abstraction <typeparamref name="TDecoratee"/>.
    /// A decorator for <typeparamref name="TDecoratee"/> is a non abstract type which implements the abstraction <typeparamref name="TDecoratee"/> and has a single public constructor which requires an instance of <typeparamref name="TDecoratee"/>.
    /// If there are multiple public constructors false is returned.
    /// </summary>
    /// <typeparam name="TDecoratee">The type being decorated</typeparam>
    /// <param name="type">The type being checked in order to determine whether it is a decorator for type <typeparamref name="TDecoratee"/></param>
    /// <returns>True if <paramref name="type"/> is a decorator for <typeparamref name="TDecoratee"/></returns>
    /// <exception cref="ArgumentNullException">Throws <see cref="ArgumentNullException"/> when parameter <paramref name="type"/> is null</exception>
    public static bool IsDecoratorFor<TDecoratee>(Type type) where TDecoratee : class
    {
      if (type == null)
        throw new ArgumentNullException(nameof(type));

      var isNonAbstractType = !type.IsAbstract;
      var implementsAbstraction = typeof(TDecoratee).IsAssignableFrom(type);
      if (!isNonAbstractType || !implementsAbstraction)
      {
        return false;
      }

      var publicConstructors = type.GetConstructors();

      var hasNoPublicConstructors = publicConstructors.Length == 0;
      var hasMultiplePublicConstructors = publicConstructors.Length > 1;

      if (hasNoPublicConstructors || hasMultiplePublicConstructors)
      {
        return false;
      }

      var dependsOnAbstraction = publicConstructors[0].GetParameters().Any(p => p.ParameterType == typeof(TDecoratee));
      return dependsOnAbstraction;
    }
  }
}
