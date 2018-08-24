using System;
using Deltatre.Utils.Dto;
using NUnit.Framework;
using Deltatre.Utils.Extensions.Enumerable;

namespace Deltatre.Utils.Tests.Dto
{
	[TestFixture]
	public class OperationResultWithErrorsTest
	{
		[Test]
		public void CreateFailure_Throws_When_Errros_Is_Null()
		{
			// ACT
			Assert.Throws<ArgumentNullException>(() => OperationResult<string, string>.CreateFailure(null));
		}

		[Test]
		public void CreateFailure_Allows_To_Create_Failure_Result_With_Errors()
		{
			// ARRANGE
			var errors = new[] { "something bad occurred", "invalid prefix detected" }.ToNonEmptySequence();

			// ACT
			var result = OperationResult<string, string>.CreateFailure(errors);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsFalse(result.IsSuccess);
			Assert.IsNull(result.Output);
			Assert.IsNotNull(result.Errors);
			CollectionAssert.AreEqual(errors, result.Errors);
		}

		[Test]
		public void CreateSuccess_Creates_Success_Result()
		{
			// ACT
			var result = OperationResult<string, string>.CreateSuccess("my normalized value");

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual("my normalized value", result.Output);
			Assert.IsNotNull(result.Errors);
			Assert.IsEmpty(result.Errors);
		}
	}
}
