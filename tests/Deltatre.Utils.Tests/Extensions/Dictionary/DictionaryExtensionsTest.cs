using System;
using System.Collections.Generic;
using System.Globalization;
using Deltatre.Utils.Extensions.Dictionary;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Extensions.Dictionary
{
  [TestFixture]
  public class DictionaryExtensionsTest
  {
    [Test]
    public void AsReadOnly_Throws_When_Source_Is_Null()
    {
      // ACT
      Assert.Throws<ArgumentNullException>(() => DictionaryExtensions.AsReadOnly<string, object>(null));
    }

    [Test]
    public void AsReadOnly_Wraps_Source_In_ReadOnlyDictionary()
    {
      // ARRANGE
      var source = new Dictionary<string, int>
      {
        ["Foo"] = 10,
        ["Bar"] = 45
      };

      // ACT
      var result = source.AsReadOnly();

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreEqual(2, result.Count);
      Assert.IsTrue(result.ContainsKey("Foo"));
      Assert.IsTrue(result.ContainsKey("Bar"));
      Assert.AreEqual(10, result["Foo"]);
      Assert.AreEqual(45, result["Bar"]);
    }

    [Test]
    public void GetValueOrDefault_Should_Throw_When_Source_Is_Null()
    {
      // ACT
      Assert.Throws<ArgumentNullException>(() => DictionaryExtensions.GetValueOrDefault<object>(null, "key"));
    }

    [Test]
    public void GetValueOrDefault_Should_Return_Specified_String_Type()
    {
      var source = new Dictionary<string, object>
      {
        ["stringKey"] = "stringValue"
      };

      var result = source.GetValueOrDefault<string>("stringKey");
      var resultType = result.GetType();

      Assert.AreEqual(resultType, typeof(string));
    }

    [Test]
    public void GetValueOrDefault_Should_Return_Specified_Int_Type()
    {
      var source = new Dictionary<string, object>
      {
        ["IntKey"] = 2
      };

      var result = source.GetValueOrDefault<int>("intKey");
      var resultType = result.GetType();

      Assert.AreEqual(resultType, typeof(int));
    }

    [Test]
    public void GetValueOrDefault_Should_Return_Specified_Bool_Type()
    {
      var source = new Dictionary<string, object>
      {
        ["boolKey"] = true
      };

      var result = source.GetValueOrDefault<bool>("boolKey");
      var resultType = result.GetType();

      Assert.AreEqual(resultType, typeof(bool));
    }

    [Test]
    public void GetValueOrDefault_Should_Return_Default_Type_If_Not_Able_To_Cast_Type()
    {
      var source = new Dictionary<string, object>
      {
        ["boolKey"] = true
      };

      var result = source.GetValueOrDefault<int>("boolKey");
      var resultType = result.GetType();

      Assert.AreEqual(resultType, typeof(int));
    }

    [Test]
    public void GetValueOrDefault_Should_Return_Catch_Default_Value_If_Not_Able_To_Cast_Type()
    {
      var source = new Dictionary<string, object>
      {
        ["stringKey"] = "stringValue"
      };

      var result = source.GetValueOrDefault<int>("stringKey");
      var resultType = result.GetType();

      Assert.AreEqual(resultType, typeof(int));
    }

    [Test]
    public void GetValueOrDefault_Should_Return_Default_Type_If_Key_Is_Not_Found()
    {
      var source = new Dictionary<string, object>
      {
        ["boolKey"] = true
      };

      var result = source.GetValueOrDefault<int>("noFoundKey");
      var resultType = result.GetType();

      Assert.AreEqual(resultType, typeof(int));
    }

    [Test]
    public void GetValueOrDefault_Should_Return_Default_Object_Type()
    {
      var source = new Dictionary<string, object>
      {
        ["boolKey"] = new object()
      };

      var result = source.GetValueOrDefault<object>("boolKey");
      var resultType = result.GetType();

      Assert.AreEqual(resultType, typeof(object));
    }

    [Test]
    public void GetValueOrDefault_Should_Return_Default_Type_For_Custom_Class()
    {
      var source = new Dictionary<string, object>
      {
        ["customKey"] = new ObjectDisposedException("x")
      };

      var result = source.GetValueOrDefault<ObjectDisposedException>("customKey");
      var resultType = result.GetType();

      Assert.AreEqual(resultType, typeof(ObjectDisposedException));
    }

    [Test]
    public void GetValueOrDefault_Should_Return_DateTime_Culture_Info_Based()
    {
      var source = new Dictionary<string, object>
      {
        ["dateKey"] = "8/18/2010"
      };

      var culture = new CultureInfo("en-CA");
      var result = source.GetValueOrDefault<DateTime>("dateKey",culture);
      var expectedDate = new DateTime(2010, 8, 18);

      Assert.AreEqual(result.Date, expectedDate.Date);
    }

    [Test]
    public void GetValueOrDefault_Should_Return_Default_DateTime_If_No_CultureInfo_Conversion_Is_Possible()
    {
      var source = new Dictionary<string, object>
      {
        ["dateKey"] = "8/18/2010"
      };

      var culture = new CultureInfo("it-IT");
      var result = source.GetValueOrDefault<DateTime>("dateKey", culture);
      var notExpectedDate = new DateTime(2010, 8, 18);
      var expectedDate = new DateTime(0001, 1, 1).Date;

      Assert.AreNotEqual(result.Date, notExpectedDate.Date);
      Assert.AreEqual(result.Date, expectedDate);
    }

    [Test]
    public void GetValueOrDefault_Throws_If_Provider_Is_Null()
    {
      var source = new Dictionary<string, object>
      {
        ["dateKey"] = "16/7/2018"
      };

      Assert.Throws<ArgumentNullException>(() => source.GetValueOrDefault<DateTime>("dateKey", null));
    }

    [Test]
    public void GetStringOrDefault_Should_Return_The_Expected_String_Value()
    {
      var source = new Dictionary<string, object>
      {
        ["stringKey"] = "valueString"
      };

      var result = source.GetStringOrDefault("stringKey");

      Assert.AreEqual(result, "valueString");
    }

    [Test]
    public void GetStringOrDefault_Should_Return_The_Default_String_Value()
    {
      var source = new Dictionary<string, object>
      {
        ["stringKey"] = "valueString"
      };

      var result = source.GetStringOrDefault("stringKeyNotFound");

      Assert.AreEqual(result, null);
    }

    [Test]
    public void GetStringOrDefault_Should_Return_String_CultureInfo_Based()
    {

      var source = new Dictionary<string, object>
      {
        ["doubleKey"] = 16325.62901
      };

      
      var culture = CultureInfo.CreateSpecificCulture("de-DE");
      var result = source.GetStringOrDefault("doubleKey", culture);

      Assert.AreEqual("16325,62901",result);
    }

    [Test]
    public void GetBoolOrDefault_Should_Return_The_Expected_Bool_Value()
    {
      var source = new Dictionary<string, object>
      {
        ["boolKey"] = true
      };

      var result = source.GetBoolOrDefault("boolKey");

      Assert.AreEqual(result, true);
    }

    [Test]
    public void GetBoolOrDefault_Should_Return_The_Default_Bool_Value()
    {
      var source = new Dictionary<string, object>
      {
        ["boolKey"] = true
      };

      var result = source.GetBoolOrDefault("boolKeyNotFound");

      Assert.AreEqual(result, false);
    }


    [Test]
    public void GetIntOrDefault_Should_Return_The_Expected_Int_Value()
    {
      var source = new Dictionary<string, object>
      {
        ["intKey"] = 10
      };

      var result = source.GetIntOrDefault("intKey");

      Assert.AreEqual(result, 10);
    }

    [Test]
    public void GetIntOrDefault_Should_Return_The_Default_Int_Value()
    {
      var source = new Dictionary<string, object>
      {
        ["intKey"] = 15
      };

      var result = source.GetIntOrDefault("intKeyNotFound");

      Assert.AreEqual(result, 0);
    }
  }
}