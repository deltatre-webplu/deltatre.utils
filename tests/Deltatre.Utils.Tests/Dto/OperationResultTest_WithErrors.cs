using System;
using Deltatre.Utils.Dto;
using NUnit.Framework;
using Deltatre.Utils.Extensions.Enumerable;
using Deltatre.Utils.Types;

namespace Deltatre.Utils.Tests.Dto
{
	[TestFixture]
	public class OperationResultWithErrorsTest
	{
		[Test]
		public void CreateFailure_Throws_When_Errors_Is_Null()
		{
			// ARRANGE
			NonEmptySequence<string> nullSequence = null;

			// ACT
			var exception = Assert.Throws<ArgumentNullException>(() => 
        OperationResult<string, string>.CreateFailure(nullSequence)
      );

      // ASSERT
      Assert.IsNotNull(exception);
      Assert.AreEqual("errors", exception.ParamName);
		}

		[Test]
		public void CreateFailure_Allows_To_Create_Failure_Result_Providing_A_NonEmptySequence_Of_Errors()
		{
			// ARRANGE
			var errors = new[] { "something bad occurred", "invalid prefix detected" }.ToNonEmptySequence();

			// ACT
			var result = OperationResult<string, string>.CreateFailure(errors);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsFalse(result.IsSuccess);
			Assert.IsNotNull(result.Errors);
			CollectionAssert.AreEqual(errors, result.Errors);
		}

		[Test]
		public void CreateFailure_Allows_To_Create_Failure_Result_Providing_A_Single_Error_Object()
		{
			// ARRANGE
			const string error = "something bad occurred";

			// ACT
			var result = OperationResult<string, string>.CreateFailure(error);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsFalse(result.IsSuccess);
			Assert.IsNotNull(result.Errors);
			CollectionAssert.AreEqual(new[] { error }, result.Errors);
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

    [Test]
    public void Implicit_Conversion_From_TOutput_To_OperationResultOfTOutputTError_Is_Available()
    {
      // ACT
      OperationResult<string, string> result = "Hello !";

      // ASSERT
      Assert.IsNotNull(result);
      Assert.IsTrue(result.IsSuccess);
      Assert.AreEqual("Hello !", result.Output);
      Assert.IsNotNull(result.Errors);
      Assert.IsEmpty(result.Errors);
    }
  }
}
