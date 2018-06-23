using System;
using System.Threading;

namespace Deltatre.Utils.Randomization
{
	/// <summary>
	/// Use this class to get a thread local instance of Random class. Instances of different threads will have different seeds.
	/// </summary>
	public static class RandomGenerator
	{
		private static int _seed = Environment.TickCount;

		private static readonly ThreadLocal<Random> ThreadLocalRandom = new ThreadLocal<Random>(() => 
				new Random(Interlocked.Increment(ref _seed))
		);

		/// <summary>
		/// The thread local instance of Random class.
		/// </summary>
		public static Random Instance => ThreadLocalRandom.Value;
	}
}
