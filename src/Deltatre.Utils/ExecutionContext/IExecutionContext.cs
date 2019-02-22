using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Deltatre.Utils.ExecutionContext
{
  /// <summary>
  /// An interface to implement (per thread) execution context property storage and handling
  /// </summary>
  public interface IExecutionContext
  {
    /// <summary>
    /// Gets a correlation id (Guid), to be used for tracking and logging purposes
    /// </summary>
    Guid GetCorrelationId();

    /// <summary>
    /// Sets a correlation id (Guid), to be used for tracking and logging purposes
    /// </summary>
    /// <param name="correlationId"></param>
    void SetCorrelationId(Guid correlationId);

    /// <summary>
    /// Gets a previously set property.
    /// </summary>
    /// <param name="propertyName"></param>
    object GetProperty(string propertyName);

    /// <summary>
    /// Gets a previously set property.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertyName"></param>
    T GetProperty<T>(string propertyName);

    /// <summary>
    /// Sets a property to be retrieved later in the execution stack
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="propertyValue"></param>
    void SetProperty(string propertyName, object propertyValue);

    /// <summary>
    /// Sets a property to be retrieved later in the execution stack, and automatically returns an IDisposable object that
    /// will remove the property from the stack when disposed.
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="propertyValue"></param>
    IDisposable PushProperty(string propertyName, object propertyValue);

    /// <summary>
    /// Removes a property from the execution stack.
    /// </summary>
    /// <param name="propertyName"></param>
    void RemoveProperty(string propertyName);

    /// <summary>
    /// Return all properties currently set in the stack.
    /// </summary>
    ImmutableDictionary<string, object> GetAllProperties();
  }
}
