using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Deltatre.Utils.Reflection
{
  /// <summary>
  /// A collection of helper methods for common reflection tasks
  /// </summary>
  public static class ReflectionHelpers
  {
    private const string OnlyFilesWithDllExtensionPattern = "*.dll";

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

    /// <summary>
    /// Gets all assemblies in the binaries folder matching the specified search pattern. 
    /// </summary>
    /// <param name="searchPattern">
    /// The search pattern to be used to filter the returned assemblies. 
    /// This parameter can contain a combination of valid literal path and wildcard (* and ?) characters,
    /// but it doesn't support regular expressions. The only allowed file extensions are .dll and .exe (because these are the only file extensions of .NET assemblies).
    /// If you pass a string null or white space all the files in the binaries folder with a .dll extension will be returned.
    /// </param>
    /// <param name="searchOption">
    /// One of the enumeration values that specifies whether the search operation should include 
    /// all subdirectories or only the current directory.
    /// </param>
    /// <returns>All the assemblies in the binaries folder matching the specified search pattern.</returns>
    public static ReadOnlyCollection<Assembly> LoadAssembliesFromBinariesFolder(
      string searchPattern = null,
      SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
      var runningAssemblyFullPath = Assembly.GetExecutingAssembly().Location;
      var binariesFolderFullPath = Path.GetDirectoryName(runningAssemblyFullPath);

      var normalizedSearchPattern = string.IsNullOrWhiteSpace(searchPattern) ?
        OnlyFilesWithDllExtensionPattern :
        searchPattern;

      var assembliesFileNames = Directory.GetFiles(
        binariesFolderFullPath,
        normalizedSearchPattern,
        searchOption);

      return assembliesFileNames.Select(Assembly.LoadFile).ToList().AsReadOnly();
    }
  }
}
