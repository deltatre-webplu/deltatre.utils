using System;

namespace Deltatre.Utils.Extensions.DateTimeExtension
{
  /// <summary>
  /// 
  /// </summary>  
  public static class DateTimeExtensions
  {
    private static DateTime _epoch;
    /// <summary>
    /// Unix Epoch Time (1970-01-01T00:00:00Z)
    /// </summary>
    public static DateTime Epoch
    {
      get
      {
        if(_epoch == default(DateTime))
          _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        return _epoch;
      }
    }

    /// <summary>
    /// Returns the number of seconds that have elapsed since Epoch (1970-01-01T00:00:00Z)
    /// </summary>
    /// <param name="date">DateTime to convert in Unix Time</param>
    /// <returns>The number of seconds that have elapsed since Epoch (1970-01-01T00:00:00Z)</returns>
    public static int ToUnixTimeSeconds(this DateTime date)
    {
      var utcDate = date.ToUniversalTime();
      return (int) utcDate.Subtract(Epoch).TotalSeconds;
    }
  }
}
