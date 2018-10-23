using Deltatre.Utils.Extensions.Enumerable;
using Deltatre.Utils.Concurrency;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Deltatre.Utils.Tests.Concurrency
{
  public partial class TimerAsyncTest
  {
    [Test]
    public async Task Stop_Terminates_Execution_Of_Scheduled_Background_Workload()
    {
      // ARRANGE
      var values = new List<int>();
      Func<CancellationToken, Task> action = ct =>
      {
        ct.ThrowIfCancellationRequested();
        values.Add(1);
        return Task.CompletedTask;
      };

      var target = new TimerAsync(
        action,
        TimeSpan.FromMilliseconds(500),
        TimeSpan.FromMilliseconds(500));

      // ACT
      target.Start();

      await Task.Delay(1200).ConfigureAwait(false); // in 1200 milliseconds we are sure that action is called at least twice

      await target.Stop().ConfigureAwait(false);
      var snapshot1 = values.ToArray();

      await Task.Delay(1200).ConfigureAwait(false); // in 1200 milliseconds we are sure that action is called at least twice (but now the timer is stopped)
      var snapshot2 = values.ToArray();

      // ASSERT
      Assert.GreaterOrEqual(snapshot1.Length, 2);
      Assert.IsTrue(snapshot1.All(i => i == 1));
      CollectionAssert.AreEqual(snapshot1, snapshot2);
    }

    [Test]
    public void Stop_Can_Be_Called_More_Than_Once_Without_Throwing_Exceptions()
    {
      // ARRANGE
      var target = new TimerAsync(
        _ => Task.CompletedTask,
        TimeSpan.FromMilliseconds(500),
        TimeSpan.FromMilliseconds(500));

      // ASSERT
      Assert.DoesNotThrowAsync(async () =>
      {
        target.Start();

        await Task.Delay(1200).ConfigureAwait(false);

        await target.Stop().ConfigureAwait(false);
        await target.Stop().ConfigureAwait(false);
      });
    }

    [Test]
    public async Task Stop_Can_Be_Called_More_Than_Once_Without_Changing_Expected_Behaviour()
    {
      // ARRANGE
      var values = new List<int>();
      Func<CancellationToken, Task> action = ct =>
      {
        ct.ThrowIfCancellationRequested();
        values.Add(1);
        return Task.CompletedTask;
      };

      var target = new TimerAsync(
        action,
        TimeSpan.FromMilliseconds(500),
        TimeSpan.FromMilliseconds(500));

      // ACT
      target.Start();

      await Task.Delay(1200).ConfigureAwait(false); // in 1200 milliseconds we are sure that action is called at least twice

      await target.Stop().ConfigureAwait(false);
      await target.Stop().ConfigureAwait(false);
      var snapshot1 = values.ToArray();

      await Task.Delay(1200).ConfigureAwait(false); // in 1200 milliseconds we are sure that action is called at least twice (but now the timer is stopped)
      await target.Stop().ConfigureAwait(false);
      var snapshot2 = values.ToArray();

      // ASSERT
      Assert.GreaterOrEqual(snapshot1.Length, 2);
      Assert.IsTrue(snapshot1.All(i => i == 1));
      CollectionAssert.AreEqual(snapshot1, snapshot2);
    }

    [Test]
    public async Task Stop_Is_Thread_Safe()
    {
      // ARRANGE
      var values = new List<int>();
      Func<CancellationToken, Task> action = ct =>
      {
        ct.ThrowIfCancellationRequested();
        values.Add(1);
        return Task.CompletedTask;
      };

      var target = new TimerAsync(
        action,
        TimeSpan.FromMilliseconds(500),
        TimeSpan.FromMilliseconds(500));

      bool run = true;

      var thread1 = new Thread(() =>
      {
        while (run)
        {
          target.Stop().Wait();
        }
      });

      var thread2 = new Thread(() =>
      {
        while (run)
        {
          target.Stop().Wait();
        }
      });

      var threads = new[] { thread1, thread2 };

      // ACT
      target.Start();

      await Task.Delay(1200).ConfigureAwait(false); // in 1200 milliseconds we are sure that action is called at least twice

      threads.ForEach(t => t.Start());
      await Task.Delay(2000).ConfigureAwait(false);
      run = false;
      threads.ForEach(t => t.Join());
      var snapshot1 = values.ToArray();

      await Task.Delay(1200).ConfigureAwait(false); // in 1200 milliseconds we are sure that action is called at least twice (but now the timer is stopped)
      var snapshot2 = values.ToArray();

      // ASSERT
      Assert.GreaterOrEqual(snapshot1.Length, 2);
      Assert.IsTrue(snapshot1.All(i => i == 1));
      CollectionAssert.AreEqual(snapshot1, snapshot2);
    }

    [Test]
    public void Stop_Throws_ObjectDisposedException_When_Called_On_A_Disposed_Instance()
    {
      // ARRANGE
      var timer = new TimerAsync(
        _ => Task.CompletedTask,
        TimeSpan.FromMilliseconds(500),
        TimeSpan.FromMilliseconds(500));

      // ACT & ASSERT
      timer.Dispose();
      Assert.ThrowsAsync<ObjectDisposedException>(timer.Stop);
    }

    [Test]
    public void Stop_Can_Be_Called_Before_Start_Without_Throwing_Exceptions()
    {
      // ARRANGE
      var target = new TimerAsync(
        _ => Task.CompletedTask,
        TimeSpan.FromMilliseconds(500),
        TimeSpan.FromMilliseconds(500));

      // ASSERT
      Assert.DoesNotThrowAsync(async () =>
      {
        await target.Stop().ConfigureAwait(false);
        target.Start();
        await Task.Delay(1200).ConfigureAwait(false);
      });
    }

    [Test]
    public async Task Stop_Can_Be_Called_Before_Start_Without_Changing_Expected_Behaviour()
    {
      // ARRANGE
      var values = new List<int>();
      Func<CancellationToken, Task> action = ct =>
      {
        ct.ThrowIfCancellationRequested();
        values.Add(1);
        return Task.CompletedTask;
      };

      var target = new TimerAsync(
        action,
        TimeSpan.FromMilliseconds(500),
        TimeSpan.FromMilliseconds(500));

      // ACT
      await target.Stop().ConfigureAwait(false);
      target.Start();

      await Task.Delay(1200).ConfigureAwait(false); // in 1200 milliseconds we are sure that action is called at least twice

      await target.Stop().ConfigureAwait(false);
      var snapshot1 = values.ToArray();

      await Task.Delay(1200).ConfigureAwait(false); // in 1200 milliseconds we are sure that action is called at least twice (but now the timer is stopped)
      var snapshot2 = values.ToArray();

      // ASSERT
      Assert.GreaterOrEqual(snapshot1.Length, 2);
      Assert.IsTrue(snapshot1.All(i => i == 1));
      CollectionAssert.AreEqual(snapshot1, snapshot2);
    }
  }
}
