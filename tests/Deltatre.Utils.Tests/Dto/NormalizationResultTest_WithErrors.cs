using System;
using Deltatre.Utils.Dto;
using Deltatre.Utils.Extensions.Enumerable;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Dto
{
	[TestFixture]
	public class NormalizationResultWithErrorsTest
	{
		[Test]
		public void CreateInvalid_Throws_When_Errros_Is_Null()
		{
			// ACT
			Assert.Throws<ArgumentNullException>(() => NormalizationResult<string, string>.CreateInvalid(null));
		}

		[Test]
		public void CreateInvalid_Allows_To_Create_An_Invalid_Result_With_Errors()
		{
			// ARRANGE
			var errors = new[] { "something bad occurred", "invalid prefix detected" }.ToNonEmptySequence();

			// ACT
			var result = NormalizationResult<string, string>.CreateInvalid(errors);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsFalse(result.IsValid);
			Assert.IsNull(result.NormalizedValue);
			Assert.IsNotNull(result.Errors);
			CollectionAssert.AreEqual(errors, result.Errors);
		}

		[Test]
		public void CreateValid_Creates_Valid_Result()
		{
			// ACT
			var result = NormalizationResult<string, string>.CreateValid("my normalized value");

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsValid);
			Assert.AreEqual("my normalized value", result.NormalizedValue);
			Assert.IsNotNull(result.Errors);
			Assert.IsEmpty(result.Errors);
		}
	}
}
