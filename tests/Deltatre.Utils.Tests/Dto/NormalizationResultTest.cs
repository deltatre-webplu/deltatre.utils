using Deltatre.Utils.Dto;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Dto
{
	[TestFixture]
	public class NormalizationResultTest
	{
		[Test]
		public void CreateInvalid_Returns_An_Instance_Representing_Failed_Validation()
		{
			// ACT
			var result = NormalizationResult<string>.CreateInvalid();

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsFalse(result.IsValid);
			Assert.AreEqual(default(string), result.NormalizedValue);
		}

		[Test]
		public void CreateValid_Returns_An_Instance_Representing_Successful_Validation()
		{
			// ACT
			var result = NormalizationResult<string>.CreateValid("hello world");

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsValid);
			Assert.AreEqual("hello world", result.NormalizedValue);
		}
	}
}
