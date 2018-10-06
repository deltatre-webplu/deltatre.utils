using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deltatre.Utils.Randomization
{
  /// <summary>
  /// Helper methods for generating random objects
  /// </summary>
  public static class RandomHelpers
  {
    private static readonly string AlphanumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    /// <summary>
    /// Call this method to get a random alphanumeric string 
    /// </summary>
    /// <param name="length">The lenght of the random string to be generated</param>
    /// <returns>A string having the desired lenght composed by alphanumeric characters randomly chosen</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throws <see cref="ArgumentOutOfRangeException"/> when parameter <paramref name="length"/> is less than zero</exception>
    public static string GetRandomAlphanumericString(int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException(
          nameof(length), 
          $"Parameter {nameof(length)} cannot be less than zero.");

      if (length == 0)
        return string.Empty;

      var characters = new char[length];

      for (int i = 0; i < length; i++)
      {
        var randomIndex = RandomGenerator.Instance.Next(AlphanumericChars.Length);
        var randomChar = AlphanumericChars[randomIndex];
        characters[i] = randomChar;
      }

      return new string(characters);
    }
  }
}
