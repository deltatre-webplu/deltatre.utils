using System.Text.RegularExpressions;
using Deltatre.Utils.Extensions.String;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Extensions.String
{
  [TestFixture]
  public class StringExtensionsTest
  {
    [Test]
    public void StripHtmlTags_Returns_Null_For_Null_Source()
    {
      // ACT
      var result = StringExtensions.StripHtmlTags(null);

      // ASSERT
      Assert.IsNull(result);
    }

    [Test]
    public void StripHtmlTags_Returns_Empty_String_For_Empty_String_Source()
    {
      // ACT
      var result = string.Empty.StripHtmlTags();

      // ASSERT
      Assert.IsNotNull(result);
      Assert.IsEmpty(result);
    }

    [Test]
    public void StripHtmlTags_Returns_White_Space_String_For_White_Space_String_Source()
    {
      // ACT
      var result = StringExtensions.StripHtmlTags("   ");

      // ASSERT
      Assert.IsNotNull(result);
      Assert.AreEqual("   ", result);
    }

    [Test]
    public void StripHtmlTags_Should_Return_Given_String_Without_Html_Tags()
    {
      // ARRANGE
      const string source = "This <b>is a bold tag</b> and this <span>is a span tag</span>";

      // ACT
      var result = source.StripHtmlTags();

      // ASSERT
      Assert.IsNotNull(result);
      const string expectedString = "This is a bold tag and this is a span tag";
      Assert.AreEqual(expectedString, result);
    }

    [Test]
    public void StripHtmlTags_Should_Return_Given_String_Without_Html_Tags_Leaving_Escaped_Html()
    {
      // ARRANGE
      const string source = "<p>When you want to include <b>literal html</b> inside a web page you should encode it (e.g.: &lt;h1&gt; Title here &lt;/h1&gt;)</p>";

      // ACT
      var result = source.StripHtmlTags();

      // ASSERT
      Assert.IsNotNull(result);
      const string expectedString = "When you want to include literal html inside a web page you should encode it (e.g.: &lt;h1&gt; Title here &lt;/h1&gt;)";
      Assert.AreEqual(expectedString, result);
    }

    [Test]
    public void Truncate_Returns_Null_For_Null_Source()
    {
      // ACT
      var result = StringExtensions.Truncate(null);

      // ASSERT
      Assert.IsNull(result);
    }


    [Test]
    public void Truncate_Returns_Empty_String_For_Empty_String_Source()
    {
      // ACT
      var result = string.Empty.Truncate();

      // ASSERT
      Assert.IsNotNull(result);
      Assert.IsEmpty(result);
    }

    [Test]
    public void Truncate_Returns_Original_String_If_Minor_Or_Equal_To_Lenght_Params()
    {
      // ARRANGE
      const string source = "Lorem ipsum dolor sit amet, consectetur cras amet.";

      // ACT
      var result = source.Truncate(100);

      // ASSERT
      Assert.IsNotNull(result);
      const string expectedString = "Lorem ipsum dolor sit amet, consectetur cras amet.";
      Assert.AreEqual(expectedString, result);
    }

    [Test]
    public void Truncate_Returns_Truncate_String_With_Ellipsis_String_At_The_End()
    {
      // ARRANGE
      const string source = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.";

      // ACT
      var result = source.Truncate(50);

      // ASSERT
      Assert.IsNotNull(result);
      const string expectedString = "Lorem Ipsum is simply dummy text of the printing a...";
      Assert.AreEqual(expectedString, result);
    }

    [Test]
    public void Truncate_Returns_Truncate_String_With_No_Ellipsis_String_At_The_End_If_Null_Or_Whitespace()
    {
      // ARRANGE
      const string source = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.";

      // ACT
      var result = source.Truncate(50, "");

      // ASSERT
      Assert.IsNotNull(result);
      const string expectedString = "Lorem Ipsum is simply dummy text of the printing a";
      Assert.AreEqual(expectedString, result);
    }
  }
}
