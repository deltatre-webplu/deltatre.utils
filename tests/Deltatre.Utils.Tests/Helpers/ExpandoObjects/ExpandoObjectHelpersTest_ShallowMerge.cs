using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using static Deltatre.Utils.Helpers.ExpandoObjects.ExpandoObjectHelpers;

namespace Deltatre.Utils.Tests.Helpers.ExpandoObjects
{
  [TestFixture]
  public sealed class ExpandoObjectHelpersTest
  {
    [Test]
    public void ShallowMerge_Throws_ArgumentNullException_When_Target_Is_Null()
    {
      // ARRANGE
      var source = new ExpandoObject();
      var sourceAsDict = (IDictionary<string, object>)source;
      sourceAsDict.Add("FirstName", "Mario");
      sourceAsDict.Add("LastName", "Rossi");

      // ACT
      var exception = Assert.Throws<ArgumentNullException>(
        () => ShallowMerge(null, source)
      );

      // ASSERT
      Assert.IsNotNull(exception);
      Assert.AreEqual("target", exception.ParamName);
    }

    [Test]
    public void ShallowMerge_Throws_ArgumentNullException_When_Source_Is_Null()
    {
      // ARRANGE
      var target = new ExpandoObject();
      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict.Add("FirstName", "Mario");
      targetAsDict.Add("LastName", "Rossi");

      // ACT
      var exception = Assert.Throws<ArgumentNullException>(
        () => ShallowMerge(target, null)
      );

      // ASSERT
      Assert.IsNotNull(exception);
      Assert.AreEqual("source", exception.ParamName);
    }

    [Test]
    public void ShallowMerge_Returns_Clone_Of_Target_When_Source_Has_No_Properties()
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
      var source = new ExpandoObject();
      var result = ShallowMerge(target, source);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreNotSame(result, target);
      Assert.AreNotSame(result, source);

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
    public void ShallowMergeWith_Copies_Properties_Of_Source_To_Target_When_Target_Has_No_Properties()
    {
      // ARRANGE
      dynamic contact = new ExpandoObject();
      contact.Country = "Italy";
      contact.Region = "Lombardia";
      contact.Province = "MI";
      contact.City = "Milano";

      var source = new ExpandoObject();
      var sourceAsDict = (IDictionary<string, object>)source;
      sourceAsDict.Add("FirstName", "Mario");
      sourceAsDict.Add("LastName", "Rossi");
      sourceAsDict.Add("Age", 50);
      sourceAsDict.Add("Contact", contact);

      // ACT
      var target = new ExpandoObject();
      var result = ShallowMerge(target, source);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreNotSame(result, target);
      Assert.AreNotSame(result, source);

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
    public void ShallowMerge_Keeps_Properties_Of_Target_Missing_In_Source_Unchanged()
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

      var source = new ExpandoObject();
      var sourceAsDict = (IDictionary<string, object>)source;
      sourceAsDict.Add("Age", 45);
      sourceAsDict.Add("Contact", contact);
      sourceAsDict.Add("Hobbies", new[] { "gaming", "football" });

      // ACT
      var result = ShallowMerge(target, source);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreNotSame(result, target);
      Assert.AreNotSame(result, source);

      var resultAsDict = (IDictionary<string, object>)result;

      Assert.GreaterOrEqual(resultAsDict.Count, 2);
      
      Assert.IsTrue(resultAsDict.ContainsKey("FirstName"));
      Assert.IsTrue(resultAsDict.ContainsKey("LastName"));
      
      Assert.AreEqual(resultAsDict["FirstName"], "Mario");
      Assert.AreEqual(resultAsDict["LastName"], "Rossi");
    }

    [Test]
    public void ShallowMerge_Adds_Properties_Of_Source_Missing_In_Target_To_Result()
    {
      // ARRANGE
      dynamic contact = new ExpandoObject();
      contact.Country = "Italy";
      contact.Region = "Lombardia";
      contact.Province = "MI";
      contact.City = "Milano";

      var target = new ExpandoObject();

      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict.Add("Age", 50);
      targetAsDict.Add("Contact", contact);

      var source = new ExpandoObject();
      var sourceAsDict = (IDictionary<string, object>)source;
      sourceAsDict.Add("FirstName", "Mario");
      sourceAsDict.Add("LastName", "Rossi");

      // ACT
      var result = ShallowMerge(target, source);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreNotSame(result, target);
      Assert.AreNotSame(result, source);

      var resultAsDict = (IDictionary<string, object>)result;
      Assert.GreaterOrEqual(resultAsDict.Count, 2);

      Assert.IsTrue(resultAsDict.ContainsKey("FirstName"));
      Assert.IsTrue(resultAsDict.ContainsKey("LastName"));

      Assert.AreEqual(resultAsDict["FirstName"], "Mario");
      Assert.AreEqual(resultAsDict["LastName"], "Rossi");
    }

    [Test]
    public void ShallowMerge_Updates_Common_Properties_With_The_Value_Of_Source()
    {
      // ARRANGE
      dynamic targetContact = new ExpandoObject();
      targetContact.Country = "Italy";
      targetContact.Region = "Lombardia";
      targetContact.Province = "MI";
      targetContact.City = "Milano";

      var target = new ExpandoObject();

      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict.Add("FirstName", "Mario");
      targetAsDict.Add("LastName", "Rossi");
      targetAsDict.Add("Age", 50);
      targetAsDict.Add("Contact", targetContact);

      dynamic sourceContact = new ExpandoObject();
      sourceContact.Country = "Spain";
      sourceContact.City = "Barcellona";

      var source = new ExpandoObject();
      var sourceAsDict = (IDictionary<string, object>)source;
      sourceAsDict.Add("FirstName", "Giuseppe");
      sourceAsDict.Add("LastName", "Verdi");
      sourceAsDict.Add("Age", 25);
      sourceAsDict.Add("Contact", sourceContact);

      // ACT
      var result = ShallowMerge(target, source);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreNotSame(result, target);
      Assert.AreNotSame(result, source);

      var resultAsDict = (IDictionary<string, object>)result;
      Assert.AreEqual(resultAsDict.Count, 4);

      Assert.IsTrue(resultAsDict.ContainsKey("FirstName"));
      Assert.IsTrue(resultAsDict.ContainsKey("LastName"));
      Assert.IsTrue(resultAsDict.ContainsKey("Age"));
      Assert.IsTrue(resultAsDict.ContainsKey("Contact"));

      Assert.AreEqual(resultAsDict["FirstName"], "Giuseppe");
      Assert.AreEqual(resultAsDict["LastName"], "Verdi");
      Assert.AreEqual(resultAsDict["Age"], 25);

      // check contact object
      var contactObject = resultAsDict["Contact"];
      Assert.IsNotNull(contactObject);
      Assert.IsInstanceOf<ExpandoObject>(contactObject);

      var contactObjectAsDict = (IDictionary<string, object>)contactObject;
      Assert.AreEqual(contactObjectAsDict.Count, 2);

      Assert.IsTrue(contactObjectAsDict.ContainsKey("Country"));
      Assert.IsTrue(contactObjectAsDict.ContainsKey("City"));

      Assert.AreEqual(contactObjectAsDict["Country"], "Spain");
      Assert.AreEqual(contactObjectAsDict["City"], "Barcellona");
    }

    [Test]
    public void ShallowMerge_Does_Not_Change_Common_Properties_Having_Same_Value_In_Target_And_Source()
    {
      // ARRANGE
      dynamic targetContact = new ExpandoObject();
      targetContact.Country = "Italy";
      targetContact.Region = "Lombardia";
      targetContact.Province = "MI";
      targetContact.City = "Milano";

      var target = new ExpandoObject();
      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict.Add("FirstName", "Mario");
      targetAsDict.Add("LastName", "Rossi");
      targetAsDict.Add("Age", 50);
      targetAsDict.Add("Contact", targetContact);

      dynamic sourceContact = new ExpandoObject();
      sourceContact.Country = "Spain";
      sourceContact.City = "Barcellona";

      var source = new ExpandoObject();
      var sourceAsDict = (IDictionary<string, object>)source;
      sourceAsDict.Add("FirstName", "Mario");
      sourceAsDict.Add("LastName", "Rossi");
      sourceAsDict.Add("Age", 25);
      sourceAsDict.Add("Contact", sourceContact);

      // ACT
      var result = ShallowMerge(target, source);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreNotSame(result, target);
      Assert.AreNotSame(result, source);

      var resultAsDict = (IDictionary<string, object>)result;
      Assert.GreaterOrEqual(resultAsDict.Count, 2);

      Assert.IsTrue(resultAsDict.ContainsKey("FirstName"));
      Assert.IsTrue(resultAsDict.ContainsKey("LastName"));

      Assert.AreEqual(resultAsDict["FirstName"], "Mario");
      Assert.AreEqual(resultAsDict["LastName"], "Rossi");
    }

    [Test]
    public void ShallowMerge_Executes_First_Level_Merge_Only()
    {
      // ARRANGE
      var nestedObject1 = new ExpandoObject();
      var nestedObject1AsDict = (IDictionary<string, object>)nestedObject1;
      nestedObject1AsDict.Add("Key1", "Value1");
      nestedObject1AsDict.Add("Key2", "Value2");
      nestedObject1AsDict.Add("Key3", "Value3");
      nestedObject1AsDict.Add("Key5", "Value5");

      var nestedObject2 = new ExpandoObject();
      var nestedObject2AsDict = (IDictionary<string, object>)nestedObject2;
      nestedObject2AsDict.Add("Key2", "Value2");
      nestedObject2AsDict.Add("Key3", "Changed");
      nestedObject2AsDict.Add("Key4", "Value4");

      var target = new ExpandoObject();
      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict.Add("FooProperty", nestedObject1);

      var source = new ExpandoObject();
      var sourceAsDict = (IDictionary<string, object>)source;
      sourceAsDict.Add("FooProperty", nestedObject2);

      // ACT
      var result = ShallowMerge(target, source);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreNotSame(result, target);
      Assert.AreNotSame(result, source);

      var resultAsDict = (IDictionary<string, object>)result;
      Assert.AreEqual(resultAsDict.Count, 1);
      Assert.IsTrue(resultAsDict.ContainsKey("FooProperty"));

      // check nested object
      var nestedObject = resultAsDict["FooProperty"];
      Assert.IsNotNull(nestedObject);
      Assert.IsInstanceOf<ExpandoObject>(nestedObject);

      var nestedObjectAsDict = (IDictionary<string, object>)nestedObject;
      Assert.AreEqual(3, nestedObjectAsDict.Count);
      Assert.IsTrue(nestedObjectAsDict.ContainsKey("Key2"));
      Assert.IsTrue(nestedObjectAsDict.ContainsKey("Key3"));
      Assert.IsTrue(nestedObjectAsDict.ContainsKey("Key4"));

      Assert.AreEqual(nestedObjectAsDict["Key2"], "Value2");
      Assert.AreEqual(nestedObjectAsDict["Key3"], "Changed");
      Assert.AreEqual(nestedObjectAsDict["Key4"], "Value4");
    }

    [Test]
    public void ShallowMerge_Does_Not_Modify_Target_Parameter()
    {
      // ARRANGE
      dynamic targetContact = new ExpandoObject();
      targetContact.Country = "Italy";
      targetContact.Region = "Lombardia";
      targetContact.Province = "MI";
      targetContact.City = "Milano";

      var target = new ExpandoObject();
      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict.Add("FirstName", "Mario");
      targetAsDict.Add("LastName", "Rossi");
      targetAsDict.Add("Age", 50);
      targetAsDict.Add("Contact", targetContact);

      dynamic otherContact = new ExpandoObject();
      otherContact.Country = "Spain";
      otherContact.City = "Barcellona";

      var source = new ExpandoObject();
      var sourceAsDict = (IDictionary<string, object>)source;
      sourceAsDict.Add("FirstName", "Giuseppe");
      sourceAsDict.Add("LastName", "Verdi");
      sourceAsDict.Add("Age", 25);
      sourceAsDict.Add("Contact", otherContact);

      // ACT
      _ = ShallowMerge(target, source);

      // ASSERT
      Assert.AreEqual(targetAsDict.Count, 4);

      Assert.IsTrue(targetAsDict.ContainsKey("FirstName"));
      Assert.IsTrue(targetAsDict.ContainsKey("LastName"));
      Assert.IsTrue(targetAsDict.ContainsKey("Age"));
      Assert.IsTrue(targetAsDict.ContainsKey("Contact"));

      Assert.AreEqual(targetAsDict["FirstName"], "Mario");
      Assert.AreEqual(targetAsDict["LastName"], "Rossi");
      Assert.AreEqual(targetAsDict["Age"], 50);

      // check contact object
      var contactObject = targetAsDict["Contact"];
      Assert.IsNotNull(contactObject);
      Assert.IsInstanceOf<ExpandoObject>(contactObject);

      var contactObjectAsDict = (IDictionary<string, object>)contactObject;
      Assert.AreEqual(contactObjectAsDict.Count, 4);

      Assert.IsTrue(contactObjectAsDict.ContainsKey("Country"));
      Assert.IsTrue(contactObjectAsDict.ContainsKey("City"));
      Assert.IsTrue(contactObjectAsDict.ContainsKey("Region"));
      Assert.IsTrue(contactObjectAsDict.ContainsKey("Province"));

      Assert.AreEqual(contactObjectAsDict["Country"], "Italy");
      Assert.AreEqual(contactObjectAsDict["City"], "Milano");
      Assert.AreEqual(contactObjectAsDict["Region"], "Lombardia");
      Assert.AreEqual(contactObjectAsDict["Province"], "MI");
    }

    [Test]
    public void ShallowMerge_Does_Not_Modify_Source_Parameter()
    {
      // ARRANGE
      dynamic targetContact = new ExpandoObject();
      targetContact.Country = "Italy";
      targetContact.Region = "Lombardia";
      targetContact.Province = "MI";
      targetContact.City = "Milano";

      var target = new ExpandoObject();
      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict.Add("FirstName", "Mario");
      targetAsDict.Add("LastName", "Rossi");
      targetAsDict.Add("Age", 50);
      targetAsDict.Add("Contact", targetContact);

      dynamic otherContact = new ExpandoObject();
      otherContact.Country = "Spain";
      otherContact.City = "Barcellona";

      var source = new ExpandoObject();
      var sourceAsDict = (IDictionary<string, object>)source;
      sourceAsDict.Add("FirstName", "Giuseppe");
      sourceAsDict.Add("LastName", "Verdi");
      sourceAsDict.Add("Age", 25);
      sourceAsDict.Add("Contact", otherContact);

      // ACT
      _ = ShallowMerge(target, source);

      // ASSERT
      Assert.AreEqual(sourceAsDict.Count, 4);

      Assert.IsTrue(sourceAsDict.ContainsKey("FirstName"));
      Assert.IsTrue(sourceAsDict.ContainsKey("LastName"));
      Assert.IsTrue(sourceAsDict.ContainsKey("Age"));
      Assert.IsTrue(sourceAsDict.ContainsKey("Contact"));

      Assert.AreEqual(sourceAsDict["FirstName"], "Giuseppe");
      Assert.AreEqual(sourceAsDict["LastName"], "Verdi");
      Assert.AreEqual(sourceAsDict["Age"], 25);

      // check contact object
      var contactObject = sourceAsDict["Contact"];
      Assert.IsNotNull(contactObject);
      Assert.IsInstanceOf<ExpandoObject>(contactObject);

      var contactObjectAsDict = (IDictionary<string, object>)contactObject;
      Assert.AreEqual(contactObjectAsDict.Count, 2);

      Assert.IsTrue(contactObjectAsDict.ContainsKey("Country"));
      Assert.IsTrue(contactObjectAsDict.ContainsKey("City"));

      Assert.AreEqual(contactObjectAsDict["Country"], "Spain");
      Assert.AreEqual(contactObjectAsDict["City"], "Barcellona");
    }

    [Test]
    public void ShallowMerge_Is_Able_To_Shallow_Merge_Deeply_Nested_Objects()
    {
      // ARRANGE
      dynamic targetSecondLevelObject = new ExpandoObject();
      targetSecondLevelObject.TargetKey3 = "target value 3";
      targetSecondLevelObject.TargetKey4 = "target value 4";

      dynamic targetFirstLevelObject = new ExpandoObject();
      targetFirstLevelObject.TargetKey1 = "target value 1";
      targetFirstLevelObject.TargetKey2 = targetSecondLevelObject;

      var target = new ExpandoObject();
      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict["Key"] = targetFirstLevelObject;

      dynamic sourceSecondLevelObject = new ExpandoObject();
      sourceSecondLevelObject.SourceKey3 = "source value 3";
      sourceSecondLevelObject.SourceKey4 = "source value 4";

      dynamic sourceFirstLevelObject = new ExpandoObject();
      sourceFirstLevelObject.SourceKey1 = "source value 1";
      sourceFirstLevelObject.SourceKey2 = sourceSecondLevelObject;

      var source = new ExpandoObject();
      var sourceAsDict = (IDictionary<string, object>)source;
      sourceAsDict["Key"] = sourceFirstLevelObject;

      // ACT
      var result = ShallowMerge(target, source);

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreNotSame(result, target);
      Assert.AreNotSame(result, source);

      var resultAsDict = (IDictionary<string, object>)result;
      Assert.AreEqual(1, resultAsDict.Count);
      Assert.IsTrue(resultAsDict.ContainsKey("Key"));

      var firstLevel = resultAsDict["Key"];
      Assert.IsNotNull(firstLevel);
      Assert.IsInstanceOf<ExpandoObject>(firstLevel);

      var firstLevelAsDict = (IDictionary<string, object>)firstLevel;
      Assert.AreEqual(2, firstLevelAsDict.Count);
      Assert.IsTrue(firstLevelAsDict.ContainsKey("SourceKey1"));
      Assert.IsTrue(firstLevelAsDict.ContainsKey("SourceKey2"));
      Assert.AreEqual(firstLevelAsDict["SourceKey1"], "source value 1");

      var secondLevel = firstLevelAsDict["SourceKey2"];
      Assert.IsNotNull(secondLevel);
      Assert.IsInstanceOf<ExpandoObject>(secondLevel);

      var secondLevelAsDict = (IDictionary<string, object>)secondLevel;
      Assert.AreEqual(2, secondLevelAsDict.Count);
      Assert.IsTrue(secondLevelAsDict.ContainsKey("SourceKey3"));
      Assert.IsTrue(secondLevelAsDict.ContainsKey("SourceKey4"));
      Assert.AreEqual(secondLevelAsDict["SourceKey3"], "source value 3");
      Assert.AreEqual(secondLevelAsDict["SourceKey4"], "source value 4");

      // check object references
      Assert.AreSame(sourceSecondLevelObject, secondLevel);
      Assert.AreSame(sourceFirstLevelObject, firstLevel);
    }
  }
}
