using System;
using System.Linq;
using Deltatre.Utils.Types;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Types
{
	[TestFixture]
	public class NonEmptySequenceTest
	{
		[Test]
		public void Ctor_Throws_When_Items_Is_Null()
		{
			// ACT
			Assert.Throws<ArgumentNullException>(() => new NonEmptySequence<string>(null));
		}

		[Test]
		public void Ctor_Throws_When_Items_Is_Empty_Sequence()
		{
			// ACT
			Assert.Throws<ArgumentException>(() => new NonEmptySequence<string>(Enumerable.Empty<string>()));
		}

		[Test]
		public void Ctor_Creates_An_Enumerable_Instance()
		{
			// ARRANGE
			var items = new[] {"foo", "bar"};

			// ACT
			var result = new NonEmptySequence<string>(items);

			// ASSERT
			Assert.IsNotNull(result);
			CollectionAssert.AreEqual(new[] { "foo", "bar" }, result);
		}
	}
}
