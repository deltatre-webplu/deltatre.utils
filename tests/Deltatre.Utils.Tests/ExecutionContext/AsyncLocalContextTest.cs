using Deltatre.Utils.ExecutionContext;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Deltatre.Utils.Tests.ExecutionContext
{
  [TestFixture]
  public class AsyncLocalContextTest
  {
    private AsyncLocalContext _asyncLocalContext;

    [SetUp]
    public void Init()
    {
      _asyncLocalContext = new AsyncLocalContext();
    }

    [Test]
    public void Can_store_and_retrieve_correlationId()
    {
      // ARRANGE
      var guid = Guid.NewGuid();

      // ACT
      _asyncLocalContext.SetCorrelationId(guid);
      var retrievedGuid = _asyncLocalContext.GetCorrelationId();

      // ASSERT
      Assert.AreEqual(guid, retrievedGuid);
    }

    [Test]
    public void Retrieving_a_correlationId_without_having_set_it_beforehand_should_return_a_GuidEmpty()
    {
      // ASSERT
      var retrievedGuid = _asyncLocalContext.GetCorrelationId();
      Assert.AreEqual(Guid.Empty, retrievedGuid);
    }

    [Test]
    public void Can_store_and_retrieve_a_property()
    {
      // ARRANGE
      var myObject = new SampleObject();

      // ACT
      _asyncLocalContext.SetProperty("myObject", myObject);
      var retrievedObjectGeneric = _asyncLocalContext.GetProperty<SampleObject>("myObject");
      var retrievedObject = _asyncLocalContext.GetProperty("myObject");

      // ASSERT
      Assert.AreEqual(myObject, retrievedObjectGeneric);
      Assert.AreEqual(myObject, retrievedObject);
    }

    [Test]
    public void CorrelationId_is_a_reserved_property_name()
    {
      // ARRANGE
      var myObject = new SampleObject();

      // ACT & ASSERT
      Assert.Throws<ArgumentException>(() => _asyncLocalContext.SetProperty("CorrelationId", myObject));
    }

    [Test]
    public void Can_push_and_remove_a_property()
    {
      // ARRANGE
      var myObject = new SampleObject();
      SampleObject retrievedObjectGeneric;
      object retrievedObject;

      // ACT
      using (_asyncLocalContext.PushProperty("myObject", myObject))
      {
        // inside the using I can correctly retrieve the property
        retrievedObjectGeneric = _asyncLocalContext.GetProperty<SampleObject>("myObject");
        retrievedObject = _asyncLocalContext.GetProperty("myObject");
      }

      // after the dispose, I cannot read the property anymore
      var nullObject = _asyncLocalContext.GetProperty("myObject");

      // ASSERT
      Assert.AreEqual(myObject, retrievedObjectGeneric);
      Assert.AreEqual(myObject, retrievedObject);
      Assert.IsNull(nullObject);
    }

    [Test]
    public void CorrelationId_is_per_thread()
    {
      var firstTaskGuid = Guid.NewGuid();
      var secondTaskGuid = Guid.NewGuid();

      var retrievedFirstGuid = Guid.Empty;
      var retrievedSecondGuid = Guid.Empty;

      var firstTask = Task.Factory.StartNew(() =>
      {
        Thread.Sleep(1000);
        _asyncLocalContext.SetCorrelationId(firstTaskGuid);
        Thread.Sleep(100);
        retrievedFirstGuid = _asyncLocalContext.GetCorrelationId();
      });

      var secondTask = Task.Factory.StartNew(() =>
      {
        _asyncLocalContext.SetCorrelationId(secondTaskGuid);
        Thread.Sleep(2000);
        retrievedSecondGuid = _asyncLocalContext.GetCorrelationId();
      });

      Task.WaitAll(firstTask, secondTask);

      Assert.AreEqual(firstTaskGuid, retrievedFirstGuid);
      Assert.AreEqual(secondTaskGuid, retrievedSecondGuid);
    }

    [Test]
    public void PushProperty_is_per_thread()
    {
      // ARRANGE
      const string propertyName = "myProperty";
      var myObject = new SampleObject();
      SampleObject retrieved1 = null;
      SampleObject retrieved2 = null;

      var firstTask = Task.Factory.StartNew(() =>
      {
        using (_asyncLocalContext.PushProperty(propertyName, myObject))
        {
          Thread.Sleep(2000);
          retrieved1 = _asyncLocalContext.GetProperty<SampleObject>(propertyName);
        }
      });

      var secondTask = Task.Factory.StartNew(() =>
      {
        Thread.Sleep(500);
        retrieved2 = _asyncLocalContext.GetProperty<SampleObject>(propertyName);
      });

      Task.WaitAll(firstTask, secondTask);

      Assert.AreEqual(myObject, retrieved1);
      Assert.IsNull(retrieved2);
    }

    public class SampleObject
    {
      public string EntityType { get; set; }
      public string TranslationId { get; set; }

      public SampleObject()
      {
        EntityType = "MyEntityType";
        TranslationId = Guid.NewGuid().ToString();
      }
    }
  }
}
