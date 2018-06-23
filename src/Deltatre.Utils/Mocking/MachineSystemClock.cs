using System;

namespace Deltatre.Utils.Mocking
{
	/// <inheritdoc />
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
	}
}
