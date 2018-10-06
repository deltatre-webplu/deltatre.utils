using Deltatre.Utils.Dto;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Dto
{
	[TestFixture]
	public class OperationResultTest
	{
		[Test]
		public void CreateFailure_Returns_An_Instance_Representing_Failed_Validation()
		{
			// ACT
			var result = OperationResult<string>.CreateFailure();

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual(default(string), result.Output);
		}

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
      Assert.AreEqual(default(string), result.Output);
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
	}
}
