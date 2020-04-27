using Deltatre.Utils.Extensions.ExpandoObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Deltatre.Utils.Tests.Extensions.ExpandoObjects
{
  [TestFixture]
  public sealed partial class ExpandoObjectExtensionsTest
  {
    [Test]
    public void ShallowClone_Throws_ArgumentNullException_When_Source_Is_Null() 
    {
      // ACT
      var exception = Assert.Throws<ArgumentNullException>(
        () => ExpandoObjectExtensions.ShallowClone(null)
      );

      // ASSERT
      Assert.IsNotNull(exception);
      Assert.AreEqual("source", exception.ParamName);
    }

    [Test]
    public void ShallowClone_Returns_Non_Null_Result()
    {
      // ARRANGE
      dynamic contact = new ExpandoObject();
      contact.Country = "Italy";
      contact.Region = "Lombardia";
      contact.Province = "MI";
      contact.City = "Milano";

      var target = new ExpandoObject();

      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict.Add("FirstName", "Mario");
      targetAsDict.Add("LastName", "Rossi");
      targetAsDict.Add("Age", 50);
      targetAsDict.Add("Contact", contact);

      // ACT
      var result = target.ShallowClone();

      // ASSERT
      Assert.IsNotNull(result);
    }

    [Test]
    public void ShallowClone_Returns_Fresh_New_Object()
    {
      // ARRANGE
      dynamic contact = new ExpandoObject();
      contact.Country = "Italy";
      contact.Region = "Lombardia";
      contact.Province = "MI";
      contact.City = "Milano";

      var target = new ExpandoObject();

      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict.Add("FirstName", "Mario");
      targetAsDict.Add("LastName", "Rossi");
      targetAsDict.Add("Age", 50);
      targetAsDict.Add("Contact", contact);

      // ACT
      var result = target.ShallowClone();

      // ASSERT
      Assert.AreNotSame(result, target);
    }

    [Test]
    public void ShallowClone_Returns_An_Object_Having_Same_Properties_As_Source()
    {
      // ARRANGE
      dynamic contact = new ExpandoObject();
      contact.Country = "Italy";
      contact.Region = "Lombardia";
      contact.Province = "MI";
      contact.City = "Milano";

      var target = new ExpandoObject();

      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict.Add("FirstName", "Mario");
      targetAsDict.Add("LastName", "Rossi");
      targetAsDict.Add("Age", 50);
      targetAsDict.Add("Contact", contact);

      // ACT
      var result = target.ShallowClone();

      // ASSERT
      var resultAsDict = (IDictionary<string, object>)result;
      Assert.AreEqual(resultAsDict.Count, 4);

      Assert.IsTrue(resultAsDict.ContainsKey("FirstName"));
      Assert.IsTrue(resultAsDict.ContainsKey("LastName"));
      Assert.IsTrue(resultAsDict.ContainsKey("Age"));
      Assert.IsTrue(resultAsDict.ContainsKey("Contact"));

      Assert.AreEqual(resultAsDict["FirstName"], "Mario");
      Assert.AreEqual(resultAsDict["LastName"], "Rossi");
      Assert.AreEqual(resultAsDict["Age"], 50);

      // check contact object
      var contactObject = resultAsDict["Contact"];
      Assert.IsNotNull(contactObject);
      Assert.IsInstanceOf<ExpandoObject>(contactObject);

      var contactObjectAsDict = (IDictionary<string, object>)contactObject;
      Assert.AreEqual(contactObjectAsDict.Count, 4);

      Assert.IsTrue(contactObjectAsDict.ContainsKey("Country"));
      Assert.IsTrue(contactObjectAsDict.ContainsKey("Region"));
      Assert.IsTrue(contactObjectAsDict.ContainsKey("Province"));
      Assert.IsTrue(contactObjectAsDict.ContainsKey("City"));

      Assert.AreEqual(contactObjectAsDict["Country"], "Italy");
      Assert.AreEqual(contactObjectAsDict["Region"], "Lombardia");
      Assert.AreEqual(contactObjectAsDict["Province"], "MI");
      Assert.AreEqual(contactObjectAsDict["City"], "Milano");
    }

    [Test]
    public void ShallowClone_Returns_Shallow_Clone_Of_Source()
    {
      // ARRANGE
      dynamic contact = new ExpandoObject();
      contact.Country = "Italy";
      contact.Region = "Lombardia";
      contact.Province = "MI";
      contact.City = "Milano";

      var target = new ExpandoObject();

      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict.Add("FirstName", "Mario");
      targetAsDict.Add("LastName", "Rossi");
      targetAsDict.Add("Age", 50);
      targetAsDict.Add("Contact", contact);

      // ACT
      var result = target.ShallowClone();

      // ASSERT
      var resultAsDict = (IDictionary<string, object>)result;
      Assert.AreEqual(resultAsDict.Count, 4);

      Assert.IsTrue(resultAsDict.ContainsKey("FirstName"));
      Assert.IsTrue(resultAsDict.ContainsKey("LastName"));
      Assert.IsTrue(resultAsDict.ContainsKey("Contact"));
      Assert.IsTrue(resultAsDict.ContainsKey("Age"));

      Assert.AreSame(resultAsDict["FirstName"], targetAsDict["FirstName"]);
      Assert.AreSame(resultAsDict["LastName"], targetAsDict["LastName"]);
      Assert.AreSame(resultAsDict["Contact"], targetAsDict["Contact"]);
      Assert.AreSame(resultAsDict["Age"], targetAsDict["Age"]);
    }

    [Test]
    public void ShallowClone_Is_Able_To_Clone_Source_Having_Deeply_Nested_Properties()
    {
      // ARRANGE
      dynamic secondLevelObject = new ExpandoObject();
      secondLevelObject.Key3 = "Value3";
      secondLevelObject.Key4 = "Value4";

      dynamic firstLevelObject = new ExpandoObject();
      firstLevelObject.Key1 = "Value1";
      firstLevelObject.Key2 = secondLevelObject;

      var target = new ExpandoObject();
      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict["key"] = firstLevelObject;

      // ACT
      var result = target.ShallowClone();

      // ASSERT
      var resultAsDict = (IDictionary<string, object>)result;
      Assert.AreEqual(resultAsDict.Count, 1);

      Assert.IsTrue(resultAsDict.ContainsKey("key"));
      var firstLevel = resultAsDict["key"];
      Assert.IsNotNull(firstLevel);
      Assert.IsInstanceOf<ExpandoObject>(firstLevel);

      var firstLevelAsDict = (IDictionary<string, object>)firstLevel;
      Assert.AreEqual(2, firstLevelAsDict.Count);
      Assert.IsTrue(firstLevelAsDict.ContainsKey("Key1"));
      Assert.IsTrue(firstLevelAsDict.ContainsKey("Key2"));
      Assert.AreEqual("Value1", firstLevelAsDict["Key1"]);

      var secondLevel = firstLevelAsDict["Key2"];
      Assert.IsNotNull(secondLevel);
      Assert.IsInstanceOf<ExpandoObject>(secondLevel);

      var secondLevelAsDict = (IDictionary<string, object>)secondLevel;
      Assert.AreEqual(2, secondLevelAsDict.Count);
      Assert.IsTrue(secondLevelAsDict.ContainsKey("Key3"));
      Assert.IsTrue(secondLevelAsDict.ContainsKey("Key4"));
      Assert.AreEqual("Value3", secondLevelAsDict["Key3"]);
      Assert.AreEqual("Value4", secondLevelAsDict["Key4"]);

      // check object references
      Assert.AreSame(secondLevelObject, secondLevel);
      Assert.AreSame(firstLevelObject, firstLevel);
    }
  }
}
