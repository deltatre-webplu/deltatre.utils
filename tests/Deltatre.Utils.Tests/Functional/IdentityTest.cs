using NUnit.Framework;
using static Deltatre.Utils.Functional.Functions;

namespace Deltatre.Utils.Tests.Functional
{
	[TestFixture]
	public class IdentityTest
	{
		[Test]
		public void Identity_Returns_Item_Passed_In()
		{
			// ARRANGE
			const string item = "hello world";

			// ACT
			var result = Identity(item);

			// ASSERT
			Assert.AreEqual("hello world", result);
		}
	}
}
