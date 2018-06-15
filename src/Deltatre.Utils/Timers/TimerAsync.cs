using System;
using System.Threading;
using System.Threading.Tasks;

namespace Deltatre.Utils.Timers
{
    public sealed class TimerAsync
    {
      private readonly Func<CancellationToken, Task> _scheduledAction;
      private readonly TimeSpan _dueTime;
      private TimeSpan _period;
      private CancellationTokenSource _cancellationSource;
      private Task _scheduledTask;
      public event EventHandler<Exception> OnError;
      
      public TimerAsync(Func<CancellationToken, Task> scheduledAction, TimeSpan dueTime, TimeSpan period)
      {
        _scheduledAction = scheduledAction ?? throw new ArgumentNullException(nameof(scheduledAction));
        
        if(dueTime < TimeSpan.Zero)
          throw new ArgumentException("due time must be equal or greater than zero", nameof(dueTime));
        _dueTime = dueTime;
        
        if(period < TimeSpan.Zero)
          throw new ArgumentException("period must be equal or greater than zero", nameof(period));
        _period = period;
      }

      public void Start()
      {
        _cancellationSource = new CancellationTokenSource();

        _scheduledTask = Task.Run(async () =>
        {
          try
          {
            await Task.Delay(_dueTime, _cancellationSource.Token);

            while (true)
            {
              await _scheduledAction(_cancellationSource.Token);
              await Task.Delay(_period, _cancellationSource.Token);
            }            
          }
          catch (OperationCanceledException)
          {
          }
          catch (Exception ex)
          {
            OnError?.Invoke(this, ex);
          }
          
        }, _cancellationSource.Token);
      }

      public async Task Stop()
      {
        _cancellationSource.Cancel();
        try
        {
          await _scheduledTask;
        }
        catch (OperationCanceledException)
        {

        }
      }
    }
}

