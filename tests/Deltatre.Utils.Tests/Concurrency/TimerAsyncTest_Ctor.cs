using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Deltatre.Utils.Concurrency;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Concurrency
{
  [TestFixture]
  public partial class TimerAsyncTest
  {
    [Test]
    public void Ctor_Throws_When_ScheduledAction_IsNull()
    {
      // ACT
      Assert.Throws<ArgumentNullException>(
        () => new TimerAsync(
          null,
          TimeSpan.FromSeconds(1),
          TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Ctor_Throws_When_DueTime_Is_Less_Than_Zero()
    {
      // ACT
      Assert.Throws<ArgumentOutOfRangeException>(
        () => new TimerAsync(
          _ => Task.CompletedTask,
          TimeSpan.FromMilliseconds(-6),
          TimeSpan.FromSeconds(10)));
    }

    [Test]
    public void Ctor_Throws_When_Period_Is_Less_Than_Zero()
    {
      // ACT
      Assert.Throws<ArgumentOutOfRangeException>(
        () => new TimerAsync(
          _ => Task.CompletedTask,
          TimeSpan.FromSeconds(10),
          TimeSpan.FromMilliseconds(-3)));
    }

    [Test]
    public void Ctor_Allows_To_Pass_DueTime_Zero()
    {
      // ACT
      Assert.DoesNotThrow(
        () => new TimerAsync(
          _ => Task.CompletedTask,
          TimeSpan.Zero,
          TimeSpan.FromSeconds(10)));
    }

    [Test]
    public void Ctor_Allows_To_Pass_Period_Zero()
    {
      // ACT
      Assert.DoesNotThrow(
        () => new TimerAsync(
          _ => Task.CompletedTask,
          TimeSpan.FromSeconds(5),
          TimeSpan.Zero));
    }

    [Test]
    public void Ctor_Allows_To_Pass_Infinite_DueTime()
    {
      Assert.DoesNotThrow(
        () => new TimerAsync(
          _ => Task.CompletedTask,
          Timeout.InfiniteTimeSpan,
          TimeSpan.FromSeconds(10)));
    }

    [Test]
    public void Ctor_Allows_To_Pass_Infinite_Period()
    {
      Assert.DoesNotThrow(
        () => new TimerAsync(
          _ => Task.CompletedTask,
          TimeSpan.FromSeconds(5),
          Timeout.InfiniteTimeSpan));
    }

    [Test]
    public async Task It_Is_Possible_To_Execute_Background_Workload_Before_Previous_Execution_Completes()
    {
      // ARRANGE
      var iterationInfos = new ConcurrentBag<(DateTime start, DateTime end, int iterationNumber)>();
      int counter = 0;
      Func<CancellationToken, Task> action = async _ => 
      {
        var iterationNumber = Interlocked.Increment(ref counter);

        var startTime = DateTime.Now;
        await Task.Delay(500).ConfigureAwait(false);
        var endTime = DateTime.Now;

        iterationInfos.Add((startTime, endTime, iterationNumber));
      };

      var target = new TimerAsync(
        action,
        TimeSpan.FromMilliseconds(40),
        TimeSpan.FromMilliseconds(40),
        canStartNextActionBeforePreviousIsCompleted: true);

      // ACT
      target.Start();
      await Task.Delay(600).ConfigureAwait(false);
      await target.Stop().ConfigureAwait(false);

      // ASSERT
      Assert.GreaterOrEqual(iterationInfos.Count, 2);

      // check the overlap
      var timeFrames = iterationInfos
        .OrderBy(tf => tf.iterationNumber)
        .Select(tf => (tf.start, tf.end))
        .ToArray<(DateTime start, DateTime end)>();

      var timeFrame1 = timeFrames[0];
      var timeFrame2 = timeFrames[1];
      Assert.IsTrue(timeFrame1.end > timeFrame2.start);
    }

    [Test]
    public async Task Amount_Of_Time_Before_First_Execution_Of_Background_Workload_Equals_DueTime()
    {
      // ARRANGE
      var timeframes = new List<(DateTime start, DateTime end)>();
      Func<CancellationToken, Task> action = async _ =>
      {
        var startTime = DateTime.Now;
        await Task.Delay(500).ConfigureAwait(false);
        var endTime = DateTime.Now;

        timeframes.Add((startTime, endTime));
      };

      var target = new TimerAsync(
        action,
        TimeSpan.FromMilliseconds(300),
        TimeSpan.FromMilliseconds(500));

      // ACT
      var startingTime = DateTime.Now;
      target.Start();

      await Task.Delay(350).ConfigureAwait(false);
      await target.Stop().ConfigureAwait(false);

      // ASSERT
      Assert.GreaterOrEqual(timeframes.Count, 1);
      var actualDueTime = timeframes[0].start - startingTime;
      Assert.GreaterOrEqual(actualDueTime.TotalMilliseconds, 300);
    }

    [Test]
    public async Task Amount_Of_Time_Between_Consecutives_Execution_Of_Background_Workload_Equals_Period()
    {
      // ARRANGE
      var timeframes = new List<(DateTime start, DateTime end)>();
      Func<CancellationToken, Task> action = async _ =>
      {
        var startTime = DateTime.Now;
        await Task.Delay(500).ConfigureAwait(false);
        var endTime = DateTime.Now;

        timeframes.Add((startTime, endTime));
      };

      var target = new TimerAsync(
        action,
        TimeSpan.FromMilliseconds(300),
        TimeSpan.FromMilliseconds(500));

      // ACT
      target.Start();
      await Task.Delay(2100).ConfigureAwait(false); // in this amount of time background workload is executed at least 2 times
      await target.Stop().ConfigureAwait(false);

      // ASSERT
      Assert.GreaterOrEqual(timeframes.Count, 2);
      var timeframe1 = timeframes[0];
      var timeframe2 = timeframes[1];
      var actualPeriod = timeframe2.start - timeframe1.end;
      Assert.GreaterOrEqual(actualPeriod.TotalMilliseconds, 500);
    }
  }
}

