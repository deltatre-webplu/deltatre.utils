﻿using System;

namespace Deltatre.Utils.Validators
{
  /// <summary>
  /// A collection of helper methods to validate URLs
  /// </summary>
  public static class UrlHelpers
  {
    /// <summary>
    /// Checks whether a string is a valid absolute url. Both HTTP and HTTPS scheme are taken into account.
    /// </summary>
    /// <param name="value">The string to be validated as an absolute URL</param>
    /// <returns>True if parameter <paramref name="value"/> is a valid absolute URL, false otherwise</returns>
    public static bool IsValidAbsoluteUrl(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        return false;

      var isAbsoluteUrl = Uri.TryCreate(value, UriKind.Absolute, out var url);
      if (!isAbsoluteUrl)
        return false;

      return url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps;
    }

    /// <summary>
    /// Checks whether a string is a valid absolute url having the HTTP schema.
    /// </summary>
    /// <param name="value">The string to be validated as an absolute URL having the HTTP schema.</param>
    /// <returns>True if parameter <paramref name="value"/> is a valid absolute URL having the HTTP schema, false otherwise.</returns>
    public static bool IsValidHttpAbsoluteUrl(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        return false;

      var isAbsoluteUrl = Uri.TryCreate(value, UriKind.Absolute, out var url);
      if (!isAbsoluteUrl)
        return false;

      return url.Scheme == Uri.UriSchemeHttp;
    }

    /// <summary>
    /// Checks whether a string is a valid absolute url having the HTTPS schema.
    /// </summary>
    /// <param name="value">The string to be validated as an absolute URL having the HTTPS schema.</param>
    /// <returns>
    /// True if parameter <paramref name="value"/> is a valid absolute URL having the HTTPS schema, false otherwise.
    /// </returns>
    public static bool IsValidHttpsAbsoluteUrl(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
        return false;

      var isAbsoluteUrl = Uri.TryCreate(value, UriKind.Absolute, out var url);
      if (!isAbsoluteUrl)
        return false;

      return url.Scheme == Uri.UriSchemeHttps;
    }
  }
}
