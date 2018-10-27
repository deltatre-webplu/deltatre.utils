using System;
using System.Collections.Generic;
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
    public void GetStringOrDefault_Should_Return_The_Expected_String_Value()
    {
      var source = new Dictionary<string, object>
      {
        ["stringKey"] = "valueString"
      };

      var result = source.GetValueOrDefault<string>("stringKey");

      Assert.AreEqual(result, "valueString");
    }

    [Test]
    public void GetStringOrDefault_Should_Return_The_Default_String_Value()
    {
      var source = new Dictionary<string, object>
      {
        ["stringKey"] = "valueString"
      };

      var result = source.GetValueOrDefault<string>("stringKeyNotFound");

      Assert.AreEqual(result, null);
    }

    [Test]
    public void GetBoolOrDefault_Should_Return_The_Expected_Bool_Value()
    {
      var source = new Dictionary<string, object>
      {
        ["boolKey"] = true
      };

      var result = source.GetValueOrDefault<bool>("boolKey");

      Assert.AreEqual(result, true);
    }

    [Test]
    public void GetBoolOrDefault_Should_Return_The_Default_Bool_Value()
    {
      var source = new Dictionary<string, object>
      {
        ["boolKey"] = true
      };

      var result = source.GetValueOrDefault<bool>("boolKeyNotFound");

      Assert.AreEqual(result, false);
    }

    [Test]
    public void GetIntOrDefault_Should_Return_The_Expected_Int_Value()
    {
      var source = new Dictionary<string, object>
      {
        ["intKey"] = 10
      };

      var result = source.GetValueOrDefault<int>("intKey");

      Assert.AreEqual(result, 10);
    }

    [Test]
    public void GetIntOrDefault_Should_Return_The_Default_Int_Value()
    {
      var source = new Dictionary<string, object>
      {
        ["intKey"] = 15
      };

      var result = source.GetValueOrDefault<int>("intKeyNotFound");

      Assert.AreEqual(result, 0);
    }
  }
}