using System;

namespace Deltatre.Utils.Mocking
{
	/// <summary>
	/// Concrete implementation of ISystemClock based on your machine's system clock.
	/// </summary>
	public sealed class MachineSystemClock: ISystemClock
	{
		/// <summary>
		/// The current date and time on this computer expressed as the local time
		/// </summary>
		public DateTime Now => DateTime.Now;

		/// <summary>
		/// The current date and time on this computer expressed as UTC time
		/// </summary>
		public DateTime UtcNow => DateTime.UtcNow;

    /// <summary>
    /// Gets a <see cref="DateTimeOffset" /> object that is set to the current date and time on the current computer, 
    /// with the offset set to the local time's offset from Coordinated Universal Time (UTC).
    /// </summary>
    public DateTimeOffset OffsetNow => DateTimeOffset.Now;

    /// <summary>
    /// Gets a <see cref="DateTimeOffset" /> object whose date and time are set to the current Coordinated Universal Time (UTC) date and time 
    /// and whose offset is set to <see cref="TimeSpan.Zero" />
    /// </summary>
    public DateTimeOffset OffsetUtcNow => DateTimeOffset.UtcNow;
  }
}
