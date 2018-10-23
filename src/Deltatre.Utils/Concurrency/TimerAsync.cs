using System;
using System.Threading;
using System.Threading.Tasks;

namespace Deltatre.Utils.Concurrency
{
  /// <summary>
  /// Provides a mechanism to schedule the recurrent execution of a background workload (supporting cancellation) on a thread pool thread. The schedulation can be stopped later, when the background workload isn't useful anymore. It is possible to start and stop the timer freely as many times as you want.
  /// This class is thread safe.
  /// This class cannot be inherited.
  /// </summary>
  public sealed class TimerAsync : IDisposable
  {
    private readonly Func<CancellationToken, Task> scheduledAction;
    private readonly TimeSpan dueTime;
    private readonly TimeSpan period;
    private readonly bool canStartNextActionBeforePreviousIsCompleted;
    private readonly SemaphoreSlim semaphore; // provides a way to syncronize access to isRunning private state
    private CancellationTokenSource cancellationTokenSource;
    private Task backgroundWorkloadTask;
    private bool isDisposed;
    private bool isRunning;

    /// <summary>
    /// This event is raised when an error occurs during the execution of the scheduled background workload. Subscribe this event if you want to perform logging or do something else when an error occurs.
    /// </summary>
    public event EventHandler<Exception> OnError;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimerAsync" /> class
    /// </summary>
    /// <param name="scheduledAction">The background workload to be scheduled</param>
    /// <param name="dueTime">The delay befoe <paramref name="scheduledAction" /> is invoked for the first time. Pass the value <see cref="Timeout.InfiniteTimeSpan" /> if you never want to execute <paramref name="scheduledAction" /></param>
    /// <param name="period">The delay between two consecutive invocations of <paramref name="scheduledAction" />. Pass the value <see cref="Timeout.InfiniteTimeSpan" /> if want to execute <paramref name="scheduledAction" /> once</param>
    /// <param name="canStartNextActionBeforePreviousIsCompleted"> Whether or not waiting for the completion of <paramref name="scheduledAction" /> before starting the delay represented by <paramref name="period" /></param>
    /// <exception cref="ArgumentNullException">Throws <see cref="ArgumentNullException"/> when parameter <paramref name="scheduledAction"/> is null</exception>
    /// <exception cref="ArgumentOutOfRangeException">Throws <see cref="ArgumentOutOfRangeException"/> when either parameter <paramref name="dueTime"/> or <paramref name="period"/> is less than <see cref="TimeSpan.Zero"/> and other than <see cref="Timeout.InfiniteTimeSpan" /></exception>
    public TimerAsync(
      Func<CancellationToken, Task> scheduledAction,
      TimeSpan dueTime,
      TimeSpan period,
      bool canStartNextActionBeforePreviousIsCompleted = false)
    {
      if (!isValidDelay(dueTime))
        throw new ArgumentOutOfRangeException(
          nameof(dueTime),
          $"Parameter {nameof(dueTime)} must be a non negative delay or a delay of -1 milliseconds");

      if (!isValidDelay(period))
        throw new ArgumentOutOfRangeException(
          nameof(period),
          $"Parameter {nameof(period)} must be a non negative delay or a delay of -1 milliseconds");

      this.scheduledAction = scheduledAction ?? throw new ArgumentNullException(nameof(scheduledAction));
      this.dueTime = dueTime;
      this.period = period;
      this.canStartNextActionBeforePreviousIsCompleted = canStartNextActionBeforePreviousIsCompleted;
      this.semaphore = new SemaphoreSlim(1);

      bool isValidDelay(TimeSpan delay)
      {
        return delay >= TimeSpan.Zero || delay == Timeout.InfiniteTimeSpan;
      }
    }

    /// <summary>
    /// Starts the timer. You can safely call this method multiple times even if the timer is already started.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Throws <see cref="ObjectDisposedException"/> if the instance on which you are calling <see cref="Start"/> has been disposed</exception>
    public void Start()
    {
      if (this.isDisposed)
        throw new ObjectDisposedException(typeof(TimerAsync).Name);

      this.semaphore.Wait();

      try
      {
        if (this.isRunning)
          return;

        this.cancellationTokenSource = new CancellationTokenSource();
        this.isRunning = true;
        this.backgroundWorkloadTask = this.RunBackgroundWorkload();
      }
      finally
      {
        this.semaphore.Release();
      }
    }

    /// <summary>
    /// Stops the timer. Calling this method implies cancelling the schedulation of the background workload. You can safely call this method multiple times even if the timer is already stopped.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the ongoing operation of stopping the timer</returns>
    /// <exception cref="ObjectDisposedException">Throws <see cref="ObjectDisposedException"/> if the instance on which you are calling <see cref="Stop"/> has been disposed</exception>
    public async Task Stop()
    {
      if (this.isDisposed)
        throw new ObjectDisposedException(typeof(TimerAsync).Name);

      await this.semaphore.WaitAsync().ConfigureAwait(false);

      try
      {
        if (!this.isRunning)
          return;

        this.cancellationTokenSource.Cancel();
        await this.backgroundWorkloadTask.ConfigureAwait(false);
      }
      catch (OperationCanceledException)
      {
        // nothing to do here: task cancellation throws OperationCanceledException by design
      }
      finally
      {
        this.isRunning = false;
        this.cancellationTokenSource?.Dispose();
        this.cancellationTokenSource = null;
        this.semaphore.Release();
      }
    }

    private Task RunBackgroundWorkload()
    {
      return Task.Run(async () =>
      {
        try
        {
          await Task.Delay(this.dueTime, this.cancellationTokenSource.Token).ConfigureAwait(false);

          while (true)
          {
            if (this.canStartNextActionBeforePreviousIsCompleted)
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
              this.scheduledAction(this.cancellationTokenSource.Token); // fire and forget call to the scheduled action whitout waiting for it to complete
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
            else
            {
              await this.scheduledAction(this.cancellationTokenSource.Token).ConfigureAwait(false);
            }

            await Task.Delay(this.period, this.cancellationTokenSource.Token).ConfigureAwait(false);
          }
        }
        catch (OperationCanceledException)
        {
          // nothing to do here: task cancellation throws OperationCanceledException by design
        }
        catch (Exception exception)
        {
          this.isRunning = false;

          try
          {
            this.OnError?.Invoke(this, exception);
          }
#pragma warning disable RCS1075 // Avoid empty catch clause that catches System.Exception.
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
          catch(Exception)
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
#pragma warning restore RCS1075 // Avoid empty catch clause that catches System.Exception.
          {
            // swallow any kind of error here (we know nothing about the event subscriber and it is possible that it throws an exception during execution)
          }
        }
      }, this.cancellationTokenSource.Token);
    }

    /// <summary>
    /// Release resources
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

		/// <summary>
		/// The class finalizer
		/// </summary>
    ~TimerAsync()
    {
      this.Dispose(false);
    }

    private void Dispose(bool disposing)
    {
      if (this.isDisposed)
        return;

      if (disposing)
      {
        this.Stop().Wait();
        this.cancellationTokenSource?.Dispose();
        this.semaphore?.Dispose();
      }

      this.isDisposed = true;
    }
  }
}

