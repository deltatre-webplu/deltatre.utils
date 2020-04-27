using Deltatre.Utils.Extensions.ExpandoObjects;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;

namespace Deltatre.Utils.Tests.Extensions.ExpandoObjects
{
  [TestFixture]
  public sealed partial class ExpandoObjectExtensionsTest
  {
    [Test]
    public void ShallowMergeWith_Throws_ArgumentNullException_When_Target_Is_Null()
    {
      // ARRANGE
      var other = new ExpandoObject();

      var otherAsDict = (IDictionary<string, object>)other;
      otherAsDict.Add("FirstName", "Mario");
      otherAsDict.Add("LastName", "Rossi");

      // ACT
      var exception = Assert.Throws<ArgumentNullException>(
        () => ExpandoObjectExtensions.ShallowMergeWith(null, other)
      );

      // ASSERT
      Assert.IsNotNull(exception);
      Assert.AreEqual("target", exception.ParamName);
    }

    [Test]
    public void ShallowMergeWith_Throws_ArgumentNullException_When_Other_Is_Null()
    {
      // ARRANGE
      var target = new ExpandoObject();

      var targetAsDict = (IDictionary<string, object>)target;
      targetAsDict.Add("FirstName", "Mario");
      targetAsDict.Add("LastName", "Rossi");

      // ACT
      var exception = Assert.Throws<ArgumentNullException>(
        () => target.ShallowMergeWith(null)
      );

      // ASSERT
      Assert.IsNotNull(exception);
      Assert.AreEqual("other", exception.ParamName);
    }

    [Test]
    public void ShallowMergeWith_Does_Not_Change_Target_When_Other_Has_No_Properties()
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
      target.ShallowMergeWith(new ExpandoObject());

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
      Assert.IsTrue(contactObjectAsDict.ContainsKey("Region"));
      Assert.IsTrue(contactObjectAsDict.ContainsKey("Province"));
      Assert.IsTrue(contactObjectAsDict.ContainsKey("City"));
      
      Assert.AreEqual(contactObjectAsDict["Country"], "Italy");
      Assert.AreEqual(contactObjectAsDict["Region"], "Lombardia");
      Assert.AreEqual(contactObjectAsDict["Province"], "MI");
      Assert.AreEqual(contactObjectAsDict["City"], "Milano");
    }

    [Test]
    public void ShallowMergeWith_Copies_Properties_Of_Other_To_Target_When_Target_Has_No_Properties() 
    {
      // ARRANGE
      dynamic contact = new ExpandoObject();
      contact.Country = "Italy";
      contact.Region = "Lombardia";
      contact.Province = "MI";
      contact.City = "Milano";

      var other = new ExpandoObject();
      var otherAsDict = (IDictionary<string, object>)other;
      otherAsDict.Add("FirstName", "Mario");
      otherAsDict.Add("LastName", "Rossi");
      otherAsDict.Add("Age", 50);
      otherAsDict.Add("Contact", contact);

      // ACT
      var target = new ExpandoObject();
      target.ShallowMergeWith(other);

      // ASSERT
      var targetAsDict = (IDictionary<string, object>)target;
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
      Assert.IsTrue(contactObjectAsDict.ContainsKey("Region"));
      Assert.IsTrue(contactObjectAsDict.ContainsKey("Province"));
      Assert.IsTrue(contactObjectAsDict.ContainsKey("City"));

      Assert.AreEqual(contactObjectAsDict["Country"], "Italy");
      Assert.AreEqual(contactObjectAsDict["Region"], "Lombardia");
      Assert.AreEqual(contactObjectAsDict["Province"], "MI");
      Assert.AreEqual(contactObjectAsDict["City"], "Milano");
    }

    [Test]
    public void ShallowMergeWith_Keeps_Properties_Of_Target_Missing_In_Other_Unchanged()
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

      var other = new ExpandoObject();
      var otherAsDict = (IDictionary<string, object>)other;
      otherAsDict.Add("Age", 45);
      otherAsDict.Add("Contact", contact);
      otherAsDict.Add("Hobbies", new[] { "gaming", "football" });

      // ACT
      target.ShallowMergeWith(other);

      // ASSERT
      Assert.GreaterOrEqual(targetAsDict.Count, 2);

      Assert.IsTrue(targetAsDict.ContainsKey("FirstName"));
      Assert.IsTrue(targetAsDict.ContainsKey("LastName"));

      Assert.AreEqual(targetAsDict["FirstName"], "Mario");
      Assert.AreEqual(targetAsDict["LastName"], "Rossi");
    }

    [Test]
    public void ShallowMergeWith_Adds_Properties_Of_Other_Missing_In_Target_To_Target()
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

      var other = new ExpandoObject();
      var otherAsDict = (IDictionary<string, object>)other;
      otherAsDict.Add("FirstName", "Mario");
      otherAsDict.Add("LastName", "Rossi");

      // ACT
      target.ShallowMergeWith(other);

      // ASSERT
      Assert.GreaterOrEqual(targetAsDict.Count, 2);

      Assert.IsTrue(targetAsDict.ContainsKey("FirstName"));
      Assert.IsTrue(targetAsDict.ContainsKey("LastName"));

      Assert.AreEqual(targetAsDict["FirstName"], "Mario");
      Assert.AreEqual(targetAsDict["LastName"], "Rossi");
    }

    [Test]
    public void ShallowMergeWith_Updates_Common_Properties_With_The_Value_Of_Other() 
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

      var other = new ExpandoObject();
      var otherAsDict = (IDictionary<string, object>)other;
      otherAsDict.Add("FirstName", "Giuseppe");
      otherAsDict.Add("LastName", "Verdi");
      otherAsDict.Add("Age", 25);
      otherAsDict.Add("Contact", otherContact);

      // ACT
      target.ShallowMergeWith(other);

      // ASSERT
      Assert.AreEqual(targetAsDict.Count, 4);

      Assert.IsTrue(targetAsDict.ContainsKey("FirstName"));
      Assert.IsTrue(targetAsDict.ContainsKey("LastName"));
      Assert.IsTrue(targetAsDict.ContainsKey("Age"));
      Assert.IsTrue(targetAsDict.ContainsKey("Contact"));

      Assert.AreEqual(targetAsDict["FirstName"], "Giuseppe");
      Assert.AreEqual(targetAsDict["LastName"], "Verdi");
      Assert.AreEqual(targetAsDict["Age"], 25);

      // check contact object
      var contactObject = targetAsDict["Contact"];
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
    public void ShallowMergeWith_Does_Not_Change_Common_Properties_Having_Same_Value_In_Target_And_Other()
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

      var other = new ExpandoObject();
      var otherAsDict = (IDictionary<string, object>)other;
      otherAsDict.Add("FirstName", "Mario");
      otherAsDict.Add("LastName", "Rossi");
      otherAsDict.Add("Age", 25);
      otherAsDict.Add("Contact", otherContact);

      // ACT
      target.ShallowMergeWith(other);

      // ASSERT
      Assert.GreaterOrEqual(targetAsDict.Count, 2);

      Assert.IsTrue(targetAsDict.ContainsKey("FirstName"));
      Assert.IsTrue(targetAsDict.ContainsKey("LastName"));
      
      Assert.AreEqual(targetAsDict["FirstName"], "Mario");
      Assert.AreEqual(targetAsDict["LastName"], "Rossi");
    }

    [Test]
    public void ShallowMergeWith_Executes_First_Level_Merge_Only()
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

      var other = new ExpandoObject();
      var otherAsDict = (IDictionary<string, object>)other;
      otherAsDict.Add("FooProperty", nestedObject2);

      // ACT
      target.ShallowMergeWith(other);

      // ASSERT
      Assert.AreEqual(targetAsDict.Count, 1);
      Assert.IsTrue(targetAsDict.ContainsKey("FooProperty"));

      // check nested object
      var nestedObject = targetAsDict["FooProperty"];
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
    public void ShallowMergeWith_Does_Not_Modify_Other_Parameter()
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

      var other = new ExpandoObject();
      var otherAsDict = (IDictionary<string, object>)other;
      otherAsDict.Add("FirstName", "Giuseppe");
      otherAsDict.Add("LastName", "Verdi");
      otherAsDict.Add("Age", 25);
      otherAsDict.Add("Contact", otherContact);

      // ACT
      target.ShallowMergeWith(other);

      // ASSERT
      Assert.AreEqual(otherAsDict.Count, 4);

      Assert.IsTrue(otherAsDict.ContainsKey("FirstName"));
      Assert.IsTrue(otherAsDict.ContainsKey("LastName"));
      Assert.IsTrue(otherAsDict.ContainsKey("Age"));
      Assert.IsTrue(otherAsDict.ContainsKey("Contact"));

      Assert.AreEqual(otherAsDict["FirstName"], "Giuseppe");
      Assert.AreEqual(otherAsDict["LastName"], "Verdi");
      Assert.AreEqual(otherAsDict["Age"], 25);

      // check contact object
      var contactObject = otherAsDict["Contact"];
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
    public void ShallowMergeWith_Is_Able_To_Shallow_Merge_Deeply_Nested_Objects() 
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

      dynamic otherSecondLevelObject = new ExpandoObject();
      otherSecondLevelObject.OtherKey3 = "other value 3";
      otherSecondLevelObject.OtherKey4 = "other value 4";

      dynamic otherFirstLevelObject = new ExpandoObject();
      otherFirstLevelObject.OtherKey1 = "other value 1";
      otherFirstLevelObject.OtherKey2 = otherSecondLevelObject;

      var other = new ExpandoObject();
      var otherAsDict = (IDictionary<string, object>)other;
      otherAsDict["Key"] = otherFirstLevelObject;

      // ACT
      target.ShallowMergeWith(other);

      // ASSERT
      Assert.AreEqual(1, targetAsDict.Count);
      Assert.IsTrue(targetAsDict.ContainsKey("Key"));

      var firstLevel = targetAsDict["Key"];
      Assert.IsNotNull(firstLevel);
      Assert.IsInstanceOf<ExpandoObject>(firstLevel);

      var firstLevelAsDict = (IDictionary<string, object>)firstLevel;
      Assert.AreEqual(2, firstLevelAsDict.Count);
      Assert.IsTrue(firstLevelAsDict.ContainsKey("OtherKey1"));
      Assert.IsTrue(firstLevelAsDict.ContainsKey("OtherKey2"));
      Assert.AreEqual(firstLevelAsDict["OtherKey1"], "other value 1");

      var secondLevel = firstLevelAsDict["OtherKey2"];
      Assert.IsNotNull(secondLevel);
      Assert.IsInstanceOf<ExpandoObject>(secondLevel);

      var secondLevelAsDict = (IDictionary<string, object>)secondLevel;
      Assert.AreEqual(2, secondLevelAsDict.Count);
      Assert.IsTrue(secondLevelAsDict.ContainsKey("OtherKey3"));
      Assert.IsTrue(secondLevelAsDict.ContainsKey("OtherKey4"));
      Assert.AreEqual(secondLevelAsDict["OtherKey3"], "other value 3");
      Assert.AreEqual(secondLevelAsDict["OtherKey4"], "other value 4");

      // check object references
      Assert.AreSame(otherSecondLevelObject, secondLevel);
      Assert.AreSame(otherFirstLevelObject, firstLevel);
    }
  }
}
