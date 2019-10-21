using System;

namespace Deltatre.Utils.Mocking
{
	/// <summary>
	/// This interface is an abstraction representing the system clock. It is useful to write testable classes relying on the system clock.
	/// </summary>
	public interface ISystemClock
	{
		/// <summary>
		/// Represents the current date and time, expressed as the local time.
		/// </summary>
		DateTime Now { get; }

		/// <summary>
		/// Represents the current date and time, expressed as the Coordinated Universal Time (UTC).
		/// </summary>
		DateTime UtcNow { get; }

    /// <summary>
    /// Gets a <see cref="DateTimeOffset"/> object that is set to the current date and time on the current computer, 
    /// with the offset set to the local time's offset from Coordinated Universal Time (UTC).
    /// </summary>
    DateTimeOffset OffsetNow { get; }

    /// <summary>
    /// Gets a <see cref="DateTimeOffset"/> object whose date and time are set to the current Coordinated Universal Time (UTC) date and time 
    /// and whose offset is set to <see cref="TimeSpan.Zero"/>
    /// </summary>
    DateTimeOffset OffsetUtcNow { get; }
	}
}
