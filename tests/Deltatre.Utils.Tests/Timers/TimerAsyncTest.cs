using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Deltatre.Utils.Timers;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Timers
{
  [TestFixture]
  public class TimerAsyncTest
  {
    [Test]
    public void Timer_Constructor_Throws_When_ScheduledAction_IsNull()
    {
      Assert.Throws<ArgumentNullException>(() => new TimerAsync(null, TimeSpan.Zero, TimeSpan.Zero));
    }

    [Test]
    public void Timer_Constructor_Throws_When_DueTime_Is_Less_Than_Zero()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new TimerAsync(_ => Task.FromResult(true), TimeSpan.FromMilliseconds(-1), TimeSpan.Zero));
    }

    [Test]
    public void Timer_Constructor_Throws_When_Period_Is_Less_Than_Zero()
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => new TimerAsync(_ => Task.FromResult(true), TimeSpan.Zero, TimeSpan.FromMilliseconds(-1)));
    }

    [Test]
    public async Task Start_Should_Run_The_Timer()
    {
      // ARRANGE
      var list = new List<int>();
      Func<CancellationToken, Task> action = _ =>
      {
        list.Add(1);
        return Task.FromResult(true);
      };
      var timer = new TimerAsync(action, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));

      // ACT
      timer.Start();

      // ASSERT
      await Task.Delay(1200);
      Assert.GreaterOrEqual(2, list.Count);
      Assert.IsTrue(list.All(i => i == 1));
    }

    [Test]
    public void Calling_Start_More_Than_Once_Should_Not_Throw_Exception()
    {
      // ARRANGE
      var timer = new TimerAsync(_ => Task.FromResult(true), TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));

      // ASSERT
      timer.Start();
      Assert.DoesNotThrow(() => timer.Start());
    }

    [Test]
    public async Task Calling_Start_More_Than_Once_Should_Not_Change_Running_Behaviour()
    {
      // ARRANGE
      var list = new List<int>();
      Func<CancellationToken, Task> action = _ =>
      {
        list.Add(1);
        return Task.FromResult(true);
      };
      var timer = new TimerAsync(action, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));

      // ACT
      timer.Start();
      timer.Start();

      // ASSERT
      await Task.Delay(1200);
      Assert.GreaterOrEqual(2, list.Count);
      Assert.IsTrue(list.All(i => i == 1));
    }

    [Test]
    public async Task Calling_Stop_Should_Cancel_Scheduled_Action()
    {
      // ARRANGE
      var list = new List<int>();
      Func<CancellationToken, Task> action = ct =>
      {
        ct.ThrowIfCancellationRequested();
        list.Add(1);
        return Task.FromResult(true);
      };
      var timer = new TimerAsync(action, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));
      timer.Start();
      await Task.Delay(1200);
      var count = list.Count;

      // ACT     
      await timer.Stop();

      // ASSERT
      Assert.LessOrEqual(list.Count, count + 1);
    }

    [Test]
    public void Calling_Stop_Before_Start_Should_Not_Throw_Exception()
    {
      // ARRANGE
      var timer = new TimerAsync(_ => Task.FromResult(true), TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));

      // ASSERT
      Assert.DoesNotThrowAsync(async () => await timer.Stop());
    }

    [Test]
    public async Task Calling_Stop_More_Than_Once_Should_Not_Throw_Exception()
    {
      // ARRANGE
      var timer = new TimerAsync(_ => Task.FromResult(true), TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));
      timer.Start();

      // ASSERT
      await timer.Stop();
      Assert.DoesNotThrowAsync(async () => await timer.Stop());
    }

    [Test]
    public async Task Calling_Stop_Before_Start_Should_Not_Change_Running_Behaviour()
    {
      // ARRANGE
      var list = new List<int>();
      Func<CancellationToken, Task> action = ct =>
      {
        ct.ThrowIfCancellationRequested();
        list.Add(1);
        return Task.FromResult(true);
      };
      var timer = new TimerAsync(action, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));
      await timer.Stop();

      // ACT
      timer.Start();

      // ASSERT
      await Task.Delay(1200);
      Assert.GreaterOrEqual(2, list.Count);
      Assert.IsTrue(list.All(i => i == 1));
    }

    [Test]
    public void Calling_Start_After_Dispose_Should_Throw()
    {
      // ARRANGE
      var timer = new TimerAsync(ct => Task.FromResult(true), TimeSpan.Zero, TimeSpan.Zero);

      // ACT & ASSERT
      timer.Dispose();
      Assert.Throws<ObjectDisposedException>(() => timer.Start());
    }

    [Test]
    public async Task Calling_Stop_After_Dispose_Should_Throw()
    {
      // ARRANGE
      var timer = new TimerAsync(ct => Task.FromResult(true), TimeSpan.Zero, TimeSpan.Zero);

      // ACT & ASSERT
      timer.Dispose();
      try
      {
        await timer.Stop();
      }
      catch (ObjectDisposedException)
      {
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}

