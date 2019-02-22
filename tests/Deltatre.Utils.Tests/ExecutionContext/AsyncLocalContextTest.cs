using Deltatre.Utils.ExecutionContext;
using NUnit.Framework;
using System;
using System.Linq;
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
    public void Retrieving_a_non_set_property_should_work_appropriately()
    {
      // ACT
      var obj1 = _asyncLocalContext.GetProperty<SampleObject>("SampleObject");
      var obj2 = _asyncLocalContext.GetProperty<ISampleInterface>("ISampleInterface");
      var correlationId = _asyncLocalContext.GetCorrelationId();
      var someInt = _asyncLocalContext.GetProperty<int>("int");
      var someBool = _asyncLocalContext.GetProperty<bool>("bool");
      var someNullableInt = _asyncLocalContext.GetProperty<int?>("int?");
      var someString = _asyncLocalContext.GetProperty<string>("string");

      // ASSERT
      Assert.IsNull(obj1);
      Assert.IsNull(obj2);
      Assert.AreEqual(Guid.Empty, correlationId);
      Assert.AreEqual(0, someInt);
      Assert.IsFalse(someBool);
      Assert.IsFalse(someNullableInt.HasValue);
      Assert.IsNull(someString);
    }

    [Test]
    public void Can_store_and_retrieve_a_property_using_interface()
    {
      // ARRANGE
      var myObject = new SampleObject();

      // ACT
      _asyncLocalContext.SetProperty("myObject", myObject);
      var retrievedObjectGeneric = _asyncLocalContext.GetProperty<ISampleInterface>("myObject");
      var retrievedObject = _asyncLocalContext.GetProperty("myObject");

      // ASSERT
      Assert.AreEqual(myObject, retrievedObjectGeneric);
      Assert.AreEqual(myObject, retrievedObject);
    }

    [Test]
    public void Retrieving_an_object_of_the_wrong_type_throws_an_InvalidCastException()
    {
      // ARRANGE
      var myObject = new SampleObject();
      _asyncLocalContext.SetProperty("myObject", myObject);

      // ACT & ASSERT
      var exception = Assert.Throws<InvalidCastException>(() => _asyncLocalContext.GetProperty<SomeOtherObject>("myObject"));
      Assert.IsTrue(exception.Message.Contains(nameof(SampleObject)));
      Assert.IsTrue(exception.Message.Contains(nameof(SomeOtherObject)));
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

      var t1 = new Thread(() => {
        _asyncLocalContext.SetCorrelationId(firstTaskGuid);
      });

      t1.Start();
      t1.Join();

      var t2 = new Thread(() => {
        retrievedSecondGuid = _asyncLocalContext.GetCorrelationId();
      });

      t2.Start();
      t2.Join();

      //Assert.AreEqual(firstTaskGuid, retrievedFirstGuid);
      Assert.AreEqual(Guid.Empty, retrievedSecondGuid);
    }

    [Test]
    public void Multiple_threads_spawned_should_reference_different_instances()
    {
      var firstTaskGuid = Guid.NewGuid();
      var secondTaskGuid = Guid.NewGuid();

      var retrievedFirstGuid = Guid.Empty;
      var retrievedSecondGuid = Guid.Empty;

      _asyncLocalContext.PushProperty("test2", firstTaskGuid);

      var d1 = _asyncLocalContext.GetAllProperties();

      var tasks = Enumerable.Range(0, 2).Select(_ =>
      {
        return Task.Run(() =>
        {
          using (_asyncLocalContext.PushProperty("test", firstTaskGuid))
          {
            Assert.AreNotSame(d1, _asyncLocalContext.GetAllProperties());
          }
        });
      });
      Task.WhenAll(tasks).Wait();

      tasks = Enumerable.Range(0, 2).Select(_ =>
      {
        return Task.Run(() =>
        {
          _asyncLocalContext.SetProperty("test", firstTaskGuid);
          Assert.AreNotSame(d1, _asyncLocalContext.GetAllProperties());
        });
      });
      Task.WhenAll(tasks).Wait();
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

    public interface ISampleInterface
    {    }

    public class SampleObject : ISampleInterface
    {
      public string EntityType { get; set; }
      public string TranslationId { get; set; }

      public SampleObject()
      {
        EntityType = "MyEntityType";
        TranslationId = Guid.NewGuid().ToString();
      }
    }

    public class SomeOtherObject
    {
      public string Foo { get; set; }
    }
  }
}
