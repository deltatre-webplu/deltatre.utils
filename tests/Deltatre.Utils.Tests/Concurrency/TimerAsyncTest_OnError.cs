using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Deltatre.Utils.Concurrency;
using Moq;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Concurrency
{
  public partial class TimerAsyncTest
  {
    [Test]
    public async Task OnError_Is_Raised_When_An_Error_Occurs_Inside_Background_Workload()
    {
      // ARRANGE
      int counter = 0;
      Func<CancellationToken, Task> action = _ =>
      {
        counter++;

        if (counter >= 2)
        {
          throw new TestException("KABOOM !");
        }

        return Task.CompletedTask;
      };

      var target = new TimerAsync(
        action,
        TimeSpan.FromMilliseconds(500),
        TimeSpan.FromMilliseconds(500));

      var mock = new Mock<ILogger>();
      mock.Setup(m => m.Log(It.IsAny<string>())).Verifiable();
      target.OnError += (object sender, Exception e) => mock.Object.Log($"An error occurred {e.Message}");

      // ACT
      target.Start();
      await Task.Delay(2500).ConfigureAwait(false);

      // ASSERT
      mock.Verify(m => m.Log(It.IsAny<string>()), Times.Once());
      mock.Verify(m => m.Log("An error occurred KABOOM !"), Times.Once());
    }

    [Test]
    public async Task OnError_Is_Not_Called_When_Timer_Is_Stopped()
    {
      // ARRANGE
      var values = new List<int>();
      Func<CancellationToken, Task> action = _ =>
      {
        values.Add(1);
        return Task.CompletedTask;
      };

      var target = new TimerAsync(
        action,
        TimeSpan.FromMilliseconds(500),
        TimeSpan.FromMilliseconds(500));

      var mock = new Mock<ILogger>();
      mock.Setup(m => m.Log(It.IsAny<string>())).Verifiable();
      target.OnError += (object sender, Exception e) => mock.Object.Log($"An error occurred {e.Message}");

      // ACT
      target.Start();
      await Task.Delay(2500).ConfigureAwait(false);
      await target.Stop().ConfigureAwait(false);
      await Task.Delay(2500).ConfigureAwait(false);

      // ASSERT
      mock.Verify(m => m.Log(It.IsAny<string>()), Times.Never());
    }

    public interface ILogger
    {
      void Log(string message);
    }

    public class TestException : Exception
    {
      public TestException()
      {
      }

      public TestException(string message) : base(message)
      {
      }

      public TestException(string message, Exception innerException) : base(message, innerException)
      {
      }

      protected TestException(SerializationInfo info, StreamingContext context) : base(info, context)
      {
      }
    }
  }
}
