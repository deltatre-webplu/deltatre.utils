using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;

namespace Deltatre.Utils.ExecutionContext
{
  /// <summary>
  /// Concrete implementation of IExecutionContext that uses AsyncLocal to keep track of properties
  /// </summary>
  public class AsyncLocalContext : IExecutionContext
  {
    private const string CorrelationIdPropertyName = "CorrelationId";
    private readonly AsyncLocal<ImmutableDictionary<string, object>> _asyncLocal = new AsyncLocal<ImmutableDictionary<string, object>>();

    /// <summary>
    /// Return all properties currently set in the stack.
    /// </summary>
    public ImmutableDictionary<string, object> GetAllProperties()
    {
      return _asyncLocal.Value ?? ImmutableDictionary.Create<string, object>();
    }

    /// <summary>
    /// Gets a correlation id (Guid), to be used for tracking and logging purposes
    /// </summary>
    public Guid GetCorrelationId() => GetProperty<Guid>(CorrelationIdPropertyName);

    /// <summary>
    /// Sets a correlation id (Guid), to be used for tracking and logging purposes
    /// </summary>
    /// <param name="correlationId"></param>
    public void SetCorrelationId(Guid correlationId) => SetPropertyInternal(CorrelationIdPropertyName, correlationId);

    /// <summary>
    /// Gets a previously set property.
    /// </summary>
    /// <param name="propertyName"></param>
    public object GetProperty(string propertyName)
    {
      var dictionary = GetAllProperties();
      if (dictionary.TryGetValue(propertyName, out object value))
      {
        return value;
      }
      return null;
    }

    /// <summary>
    /// Gets a previously set property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertyName"></param>
    public T GetProperty<T>(string propertyName)
    {
      var value = GetProperty(propertyName);
      if (value == null)
      {
        return default(T);
      }
      return (T)value;
    }

    /// <summary>
    /// Sets a property to be retrieved later in the execution stack
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="propertyValue"></param>
    public void SetProperty(string propertyName, object propertyValue)
    {
      if (string.Equals(propertyName, CorrelationIdPropertyName))
        throw new ArgumentException($"{CorrelationIdPropertyName} is a reserved property name.");
      SetPropertyInternal(propertyName, propertyValue);
    }

    /// <summary>
    /// Sets a property to be retrieved later in the execution stack, 
    /// and automatically returns an AsyncLocalContextPropertyBookmark object that
    /// will remove the property from the stack when disposed.
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="propertyValue"></param>
    public IDisposable PushProperty(string propertyName, object propertyValue)
    {
      SetProperty(propertyName, propertyValue);
      return new AsyncLocalContextPropertyBookmark(propertyName, this);
    }

    /// <summary>
    /// Removes a property from the execution stack.
    /// </summary>
    /// <param name="propertyName"></param>
    public void RemoveProperty(string propertyName)
    {
      var dictionary = GetAllProperties();
      _asyncLocal.Value = dictionary.Remove(propertyName);
    }

    private void SetPropertyInternal(string propertyName, object propertyValue)
    {
      var dictionary = GetAllProperties();
      _asyncLocal.Value = dictionary.SetItem(propertyName, propertyValue);
    }
  }
}
