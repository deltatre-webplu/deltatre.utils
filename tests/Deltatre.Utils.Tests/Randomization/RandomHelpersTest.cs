using System;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Deltatre.Utils.Randomization;

namespace Deltatre.Utils.Tests.Randomization
{
	[TestFixture]
	public class RandomHelpersTest
	{
		[Test]
		public void Instance_Of_Random_Class_Is_Not_Null()
		{
			// ACT
			var instance = RandomHelpers.Instance;

			// ASSERT
			Assert.IsNotNull(instance);
		}

		[Test]
		public void Different_Threads_Get_Different_Instances_Of_Random_Class()
		{
			// ARRANGE
			Random instance1 = null;
			Random instance2 = null;

			var thread1 = new Thread(() => instance1 = RandomHelpers.Instance);
			var thread2 = new Thread(() => instance2 = RandomHelpers.Instance);

			// ACT
			thread1.Start();
			thread2.Start();

			thread1.Join();
			thread2.Join();

			// ASSERT
			Assert.IsFalse(ReferenceEquals(instance1, instance2));
		}

		[Test]
		public void Instances_Of_Different_Threads_Generate_Different_Sequences_Of_Random_Integers()
		{
			// ARRANGE
			const int sequenceLength = 3;
			var sequence1 = new int[sequenceLength];
			var sequence2 = new int[sequenceLength];

			var thread1 = new Thread(() =>
			{
				for (var i = 0; i < sequenceLength; i++)
				{
					sequence1[i] = RandomHelpers.Instance.Next((i + 1) * 10);
				}
			});

			var thread2 = new Thread(() =>
			{
				for (var i = 0; i < sequenceLength; i++)
				{
					sequence2[i] = RandomHelpers.Instance.Next((i + 1) * 10);
				}
			});

			// ACT
			thread1.Start();
			thread2.Start();

			thread1.Join();
			thread2.Join();

			// ASSERT
			(int sequence1Value, int sequence2Value)[] joinedSequence = sequence1.Zip(
				sequence2, 
				(val1, val2) => (val1, val2)
		  ).ToArray();

			Assert.IsTrue(joinedSequence.All(i => i.sequence1Value != i.sequence2Value));
		}
	}
}
