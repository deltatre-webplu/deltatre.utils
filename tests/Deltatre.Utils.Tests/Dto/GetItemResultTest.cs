using Deltatre.Utils.Dto;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Dto
{
	[TestFixture]
	public class GetItemResultTest
	{
		[Test]
		public void CreateForItemFound_Creates_Instance_For_Successful_Get_Operation()
		{
			// ACT
			var result = GetItemResult<string>.CreateForItemFound("my string");

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Found);
			Assert.AreEqual("my string", result.Item);
		}

		[Test]
		public void CreateForItemNotFound_Creates_Instance_For_Failed_Get_Operation()
		{
			// ACT
			var result = GetItemResult<string>.CreateForItemNotFound();

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsFalse(result.Found);
			Assert.IsNull(result.Item);
		}
	}
}
