using Deltatre.Utils.Dto;
using NUnit.Framework;
using System;

namespace Deltatre.Utils.Tests.Dto
{
	[TestFixture]
	public class OperationResultTest
	{
		[Test]
		public void CreateSuccess_Returns_An_Instance_Representing_Successful_Operation()
		{
			// ACT
			var result = OperationResult<string>.CreateSuccess("hello world");

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual("hello world", result.Output);
		}

    [Test]
    public void Failure_Is_An_Instance_Representing_The_Result_Of_A_Failed_Operation()
    {
      // ACT
      var result = OperationResult<string>.Failure;

      // ASSERT
      Assert.IsNotNull(result);
      Assert.IsFalse(result.IsSuccess);
    }

    [Test]
    public void Implicit_Conversion_From_TOutput_To_OperationResultOfTOutput_Is_Available()
    {
      // ACT
      OperationResult<string> result = "Hello !";

      // ASSERT
      Assert.IsNotNull(result);
      Assert.IsTrue(result.IsSuccess);
      Assert.AreEqual("Hello !", result.Output);
    }

    [Test]
    public void Output_Throws_InvalidOperationException_When_Operation_Is_Failed()
    {
      // ARRANGE
      var target = OperationResult<string>.Failure;

      // ACT
      var exception = Assert.Throws<InvalidOperationException>(() => _ = target.Output);

      // ASSERT
      Assert.IsNotNull(exception);
      Assert.AreEqual("Reading the operation output is not allowed because the operation is failed.", exception.Message);
    }

    [Test]
    public void Output_Does_Not_Throw_When_Operation_Is_Successful()
    {
      // ARRANGE
      var target = OperationResult<string>.CreateSuccess("the message");

      // ACT
      Assert.DoesNotThrow(() => _ = target.Output);
    }

    [Test]
    public void Output_Returns_The_Output_Of_Successful_Operation()
    {
      // ARRANGE
      var target = OperationResult<string>.CreateSuccess("the message");

      // ACT
      var result = target.Output;

      // ASSERT
      Assert.AreEqual("the message", result);
    }
  }
}
