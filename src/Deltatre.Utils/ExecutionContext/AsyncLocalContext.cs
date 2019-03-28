using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace Deltatre.Utils.ExecutionContext
{
  /// <summary>
  /// Concrete implementation of IExecutionContext that uses AsyncLocal to keep track of properties
  /// </summary>
  public class AsyncLocalContext : IExecutionContext
  {
    private const string CorrelationIdPropertyName = "CorrelationIdList";
    private readonly AsyncLocal<ImmutableDictionary<string, object>> _asyncLocal = new AsyncLocal<ImmutableDictionary<string, object>>();

    /// <summary>
    /// Return all properties currently set in the stack.
    /// </summary>
    public ImmutableDictionary<string, object> GetAllProperties()
    {
      return _asyncLocal.Value ?? ImmutableDictionary.Create<string, object>();
    }

    /// <summary>
    /// Gets a list correlation ids, to be used for tracking and logging purposes.
    /// </summary>
    public IEnumerable<string> GetCorrelationIdList() => GetProperty<IEnumerable<string>>(CorrelationIdPropertyName);

    /// <summary>
    /// Sets a list of correlation ids, replacing the current one, to be used for tracking and logging purposes.
    /// </summary>
    /// <param name="correlationIds"></param>
    /// <remarks>
    ///   You will probably rarely need to use this method. 
    ///   Generally, you should add a new correlationId using <see cref="AddCorrelationId(string)" />
    /// </remarks>
    public void SetCorrelationIdList(IEnumerable<string> correlationIds) => SetPropertyInternal(CorrelationIdPropertyName, correlationIds);

    /// <summary>
    /// Adds the given correlationId to the list of correlationIds.
    /// </summary>
    /// <param name="correlationId"></param>
    public void AddCorrelationId(string correlationId)
    {
      var currentList = GetCorrelationIdList() ?? new List<string>();
      var newList = new List<string>(currentList);
      if (!newList.Contains(correlationId))
        newList.Add(correlationId);
      SetCorrelationIdList(newList);
    }

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
    /// Sets multiple properties in the execution stack
    /// </summary>
    /// <param name="properties"></param>
    public void SetProperties(IEnumerable<KeyValuePair<string, object>> properties)
    {
      if (properties.Any(p => p.Key.Equals(CorrelationIdPropertyName)))
        throw new ArgumentException($"{CorrelationIdPropertyName} is a reserved property name.");
      SetPropertiesInternal(properties);
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
    /// Sets multiple properties in the execution stack, and automatically returns an IDisposable object that
    /// will remove the properties from the stack when disposed.
    /// </summary>
    /// <param name="properties"></param>
    public IDisposable PushProperties(IEnumerable<KeyValuePair<string, object>> properties)
    {
      SetProperties(properties);
      return new AsyncLocalContextPropertyBookmark(properties.Select(p => p.Key), this);
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

    /// <summary>
    /// Removes multiple properties from the execution stack.
    /// </summary>
    /// <param name="propertyNames"></param>
    public void RemoveProperties(IEnumerable<string> propertyNames)
    {
      var dictionary = GetAllProperties();
      _asyncLocal.Value = dictionary.RemoveRange(propertyNames);
    }

    private void SetPropertyInternal(string propertyName, object propertyValue)
    {
      var dictionary = GetAllProperties();
      _asyncLocal.Value = dictionary.SetItem(propertyName, propertyValue);
    }

    private void SetPropertiesInternal(IEnumerable<KeyValuePair<string, object>> properties)
    {
      var dictionary = GetAllProperties();
      _asyncLocal.Value = dictionary.SetItems(properties);
    }

  }
}
