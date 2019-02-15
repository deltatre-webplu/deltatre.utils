using System;

namespace Deltatre.Utils.ExecutionContext
{
  /// <summary>
  /// A disposable bookmark for a property set in the IExecutionContext - meant to be used inside a using {} block
  /// </summary>
  public class AsyncLocalContextPropertyBookmark : IDisposable
  {
    private readonly IExecutionContext _executionContext;
    private readonly string _propertyName;

    /// <summary>
    /// Create a new AsyncLocalContextPropertyBookmark
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="contextStack"></param>
    public AsyncLocalContextPropertyBookmark(string propertyName, IExecutionContext contextStack)
    {
      _executionContext = contextStack;
      _propertyName = propertyName;
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    /// <summary>
    /// Disposable pattern implementation
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          _executionContext.RemoveProperty(_propertyName);
        }

        disposedValue = true;
      }
    }

    /// <summary>
    /// Disposable pattern implementation
    /// </summary>
    public void Dispose()
    {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
    }
    #endregion
  }
}
