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
	}
}
