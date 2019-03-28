using Deltatre.Utils.ExecutionContext;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
      const string correlationId = "my correlation id";

      // ACT
      _asyncLocalContext.AddCorrelationId(correlationId);
      var retrievedList = _asyncLocalContext.GetCorrelationIdList();

      // ASSERT
      Assert.AreEqual(1, retrievedList.Count());
      Assert.AreEqual(correlationId, retrievedList.First());
    }

    [Test]
    public void Pushing_an_already_inserted_correlationId_should_not_add_it_to_the_list()
    {
      // ARRANGE
      const string correlationId1 = "my correlation id";
      const string correlationId2 = "my correlation id"; // same, but another string instance

      // ACT
      _asyncLocalContext.AddCorrelationId(correlationId1);
      var retrievedList1 = _asyncLocalContext.GetCorrelationIdList();

      _asyncLocalContext.AddCorrelationId(correlationId2);
      var retrievedList2 = _asyncLocalContext.GetCorrelationIdList();

      // ASSERT
      Assert.AreEqual(1, retrievedList2.Count());
      Assert.AreEqual(retrievedList1.Count(), retrievedList2.Count());

      Assert.AreEqual(correlationId1, retrievedList1.First());
      Assert.AreEqual(correlationId2, retrievedList1.First());
      Assert.AreEqual(correlationId1, retrievedList2.First());
      Assert.AreEqual(correlationId2, retrievedList2.First());
    }

    [Test]
    public void CorrelationId_duplication_avoidance_is_case_sensitive()
    {
      // ARRANGE
      const string correlationId1 = "correlation";
      var correlationId2 = correlationId1.ToUpper();

      // ACT
      _asyncLocalContext.AddCorrelationId(correlationId1);
      var retrievedList1 = _asyncLocalContext.GetCorrelationIdList();

      _asyncLocalContext.AddCorrelationId(correlationId2);
      var retrievedList2 = _asyncLocalContext.GetCorrelationIdList();

      // ASSERT
      Assert.AreEqual(1, retrievedList1.Count());
      Assert.AreEqual(correlationId1, retrievedList1.First());

      Assert.AreEqual(2, retrievedList2.Count());
      Assert.AreEqual(correlationId1, retrievedList2.First());
      Assert.AreEqual(correlationId2, retrievedList2.Last());
    }

    [Test]
    public void Can_store_and_retrieve_correlationIds_as_a_list()
    {
      // ARRANGE
      var correlationIds = new List<string> { "one", "two", "three" };

      // ACT
      _asyncLocalContext.SetCorrelationIdList(correlationIds);
      var retrievedList = _asyncLocalContext.GetCorrelationIdList();

      // ASSERT
      Assert.AreSame(correlationIds, retrievedList);
    }

    [Test]
    public void Retrieving_the_list_of_correlationId_without_having_set_it_beforehand_should_return_null()
    {
      // ASSERT
      var retrievedList = _asyncLocalContext.GetCorrelationIdList();
      Assert.IsNull(retrievedList);
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
    public async Task Setting_a_property_in_another_thread_does_not_populate_the_context_of_the_main_thread()
    {
      // ARRANGE
      var myObject = new SampleObject();
      SampleObject retrievedObjectInsideTheTask = null;

      // ACT
      await Task.Run(() => {
        _asyncLocalContext.SetProperty("myObject", myObject);
        retrievedObjectInsideTheTask = _asyncLocalContext.GetProperty<SampleObject>("myObject");
      }).ConfigureAwait(false);

      var retrievedObject = _asyncLocalContext.GetProperty("myObject");

      // ASSERT
      Assert.IsNull(retrievedObject);
      Assert.AreEqual(myObject, retrievedObjectInsideTheTask);
    }

    [Test]
    public void Can_store_and_retrieve_multiple_properties()
    {
      // ARRANGE
      var myObject = new SampleObject();
      var myObject2 = new SampleObject();

      // ACT
      _asyncLocalContext.SetProperties(new[] {
        new KeyValuePair<string, object>("myObject", myObject),
        new KeyValuePair<string, object>("myObject2", myObject2)
      });

      var retrievedObjectGeneric = _asyncLocalContext.GetProperty<SampleObject>("myObject");
      var retrievedObject = _asyncLocalContext.GetProperty("myObject");
      var retrievedObjectGeneric2 = _asyncLocalContext.GetProperty<SampleObject>("myObject2");
      var retrievedObject2 = _asyncLocalContext.GetProperty("myObject2");

      // ASSERT
      Assert.AreEqual(myObject, retrievedObjectGeneric);
      Assert.AreEqual(myObject, retrievedObject);
      Assert.AreEqual(myObject2, retrievedObjectGeneric2);
      Assert.AreEqual(myObject2, retrievedObject2);
    }

    [Test]
    public void CorrelationIdList_is_a_reserved_property_name()
    {
      // ARRANGE
      var myObject = new SampleObject();

      // ACT & ASSERT
      Assert.Throws<ArgumentException>(() => _asyncLocalContext.SetProperty("CorrelationIdList", myObject));

      Assert.Throws<ArgumentException>(() => _asyncLocalContext.SetProperties(new[] {
        new KeyValuePair<string, object>("myObject", myObject),
        new KeyValuePair<string, object>("CorrelationIdList", myObject)
      }));
    }

    [Test]
    public void Retrieving_a_non_set_property_should_work_appropriately()
    {
      // ACT
      var obj1 = _asyncLocalContext.GetProperty<SampleObject>("SampleObject");
      var obj2 = _asyncLocalContext.GetProperty<ISampleInterface>("ISampleInterface");
      var someGuid = _asyncLocalContext.GetProperty<Guid>("guid");
      var someInt = _asyncLocalContext.GetProperty<int>("int");
      var someBool = _asyncLocalContext.GetProperty<bool>("bool");
      var someNullableInt = _asyncLocalContext.GetProperty<int?>("int?");
      var someString = _asyncLocalContext.GetProperty<string>("string");

      // ASSERT
      Assert.IsNull(obj1);
      Assert.IsNull(obj2);
      Assert.AreEqual(Guid.Empty, someGuid);
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
    public void Can_push_and_remove_multiple_properties()
    {
      // ARRANGE
      var myObject = new SampleObject();
      var myObject2 = new SampleObject();

      SampleObject retrievedObjectGeneric;
      object retrievedObject;
      SampleObject retrievedObjectGeneric2;
      object retrievedObject2;

      // ACT
      using (_asyncLocalContext.PushProperties(new[] {
        new KeyValuePair<string, object>("myObject", myObject),
        new KeyValuePair<string, object>("myObject2", myObject2)
      }))
      {
        // inside the using I can correctly retrieve the property
        retrievedObjectGeneric = _asyncLocalContext.GetProperty<SampleObject>("myObject");
        retrievedObject = _asyncLocalContext.GetProperty("myObject");
        retrievedObjectGeneric2 = _asyncLocalContext.GetProperty<SampleObject>("myObject2");
        retrievedObject2 = _asyncLocalContext.GetProperty("myObject2");
      }

      // after the dispose, I cannot read the property anymore
      var nullObject = _asyncLocalContext.GetProperty("myObject");
      var nullObject2 = _asyncLocalContext.GetProperty("myObject2");

      // ASSERT
      Assert.AreEqual(myObject, retrievedObjectGeneric);
      Assert.AreEqual(myObject, retrievedObject);
      Assert.IsNull(nullObject);

      Assert.AreEqual(myObject2, retrievedObjectGeneric2);
      Assert.AreEqual(myObject2, retrievedObject2);
      Assert.IsNull(nullObject2);
    }

    [Test]
    public void CorrelationId_is_per_thread()
    {
      var firstCorrelationId = "correlation 1";

      IEnumerable<string> onAnotherThreadList = new List<string>();

      var t1 = new Thread(() => {
        _asyncLocalContext.AddCorrelationId(firstCorrelationId);
      });

      t1.Start();
      t1.Join();

      var t2 = new Thread(() => {
        onAnotherThreadList = _asyncLocalContext.GetCorrelationIdList();
      });

      t2.Start();
      t2.Join();

      Assert.IsNull(onAnotherThreadList);
    }

    [Test]
    public void Child_thread_can_push_correlationId_without_altering_the_original_list()
    {
      var t1CorrelationId = "correlation 1";
      var t2CorrelationId = "correlation 2";

      IEnumerable<string> t1List = null;
      IEnumerable<string> t2List = null;

      var t1 = new Thread(() => {

        _asyncLocalContext.AddCorrelationId(t1CorrelationId);

        var t2 = new Thread(() => {
          _asyncLocalContext.AddCorrelationId(t2CorrelationId);
          t2List = _asyncLocalContext.GetCorrelationIdList();
        });

        t2.Start();
        t2.Join();

        t1List = _asyncLocalContext.GetCorrelationIdList();

      });

      t1.Start();
      t1.Join();

      Assert.AreNotSame(t1List, t2List);

      Assert.AreEqual(1, t1List.Count());
      Assert.AreEqual(t1CorrelationId, t1List.First());

      Assert.AreEqual(2, t2List.Count());
      Assert.AreEqual(t1CorrelationId, t2List.First());
      Assert.AreEqual(t2CorrelationId, t2List.Last());
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
