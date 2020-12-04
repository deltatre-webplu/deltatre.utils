using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Deltatre.Utils.Azure
{
  /// <summary>
  /// The exception which is thrown when the hosting environment OS is not supported on Azure app services.
  /// This exception is thrown when the calling code performs an operation related to Azure app services, but the hosting environment
  /// is running an OS which is not supported by Azure app services.
  /// </summary>
  [Serializable]
  public class OsNotSupportedOnAzureAppServicesException : Exception
  {
    /// <summary>
    /// Gets or sets the description of the OS executed by the hosting environment.
    /// </summary>
    public string OperatingSystem { get; set; }

    /// <summary>
    /// Initializes a new instance of <see cref="OsNotSupportedOnAzureAppServicesException"/> class.
    /// </summary>
    public OsNotSupportedOnAzureAppServicesException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OsNotSupportedOnAzureAppServicesException"></see> class 
    /// with a specified error message.
    /// </summary>
    /// <param name="message">
    /// The message that describes the error.
    /// </param>
    public OsNotSupportedOnAzureAppServicesException(
      string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OsNotSupportedOnAzureAppServicesException"></see> class
    /// with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">
    /// The error message that explains the reason for the exception.
    /// </param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception,
    /// or a null reference (Nothing in Visual Basic) if no inner exception is specified.
    /// </param>
    public OsNotSupportedOnAzureAppServicesException(
      string message,
      Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OsNotSupportedOnAzureAppServicesException"></see> class with serialized data.
    /// </summary>
    /// <param name="info">
    /// The <see cref="SerializationInfo"></see> that holds the serialized object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    /// The <see cref="StreamingContext"></see> that contains contextual information about the source or destination.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="info">info</paramref> parameter is null.
    /// </exception>
    /// <exception cref="SerializationException">
    /// The class name is null or <see cref="Exception.HResult"></see> is zero (0).
    /// </exception>
    protected OsNotSupportedOnAzureAppServicesException(
      SerializationInfo info,
      StreamingContext context) : base(info, context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof(info));

      this.OperatingSystem = info.GetString(nameof(this.OperatingSystem));
    }

    /// <summary>
    /// When overridden in a derived class, sets the <see cref="SerializationInfo"></see> 
    /// with information about the exception.
    /// </summary>
    /// <param name="info">
    /// The <see cref="SerializationInfo"></see> that holds the serialized 
    /// object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    /// The <see cref="StreamingContext"></see> that contains contextual information about 
    /// the source or destination.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="info">info</paramref> parameter is a null reference 
    /// (Nothing in Visual Basic).
    /// </exception>
    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof(info));

      info.AddValue(nameof(this.OperatingSystem), this.OperatingSystem);
      
      base.GetObjectData(info, context);
    }
  }
}
