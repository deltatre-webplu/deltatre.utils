using System;

namespace Deltatre.Utils.Mocking
{
	/// <inheritdoc />
	/// <summary>
	/// Concrete implementation of ISystemClock based on your machine's system clock.
	/// </summary>
	public sealed class MachineSystemClock: ISystemClock
	{
		public DateTime Now => DateTime.Now;
		public DateTime UtcNow => DateTime.UtcNow;
	}
}
