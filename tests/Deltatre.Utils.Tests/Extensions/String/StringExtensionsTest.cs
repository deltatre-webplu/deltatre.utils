using System;
using System.Text.RegularExpressions;
using Deltatre.Utils.Extensions.String;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Extensions.String
{
    [TestFixture]
    class StringExtensionsTest
    {
        [Test]
        public void StripTags_Should_Return_Empty_String_For_Null_Source()
        {
            // ARRANGE
            var expectedString = string.Empty;

            // ACT
            var actualString = StringExtensions.StripTags(null);

            // ASSERT
            Assert.AreEqual(expectedString, actualString);
        }

        [Test]
        public void StripTags_Should_Return_Empty_String_For_Empty_Source()
        {
            // ARRANGE
            var expectedString = string.Empty;

            // ACT
            var actualString = StringExtensions.StripTags(string.Empty);

            // ASSERT
            Assert.AreEqual(expectedString, actualString);
        }

        [Test]
        public void StripTags_Should_Return_Given_String_Without_Html_Tags()
        {
            string input = "This <b>is a bold tag</b> and this <span>is a span tag</span>";

            // ARRANGE
            string expectedString = "This is a bold tag and this is a span tag";

            // ACT
            string actualString = StringExtensions.StripTags(input);

            // ASSERT
            Assert.IsFalse(Regex.IsMatch(actualString, "<(.|\n)*?>"));
            Assert.AreEqual(expectedString, actualString);
        }

        [Test]
        public void StripTags_Should_Return_Given_String_Without_Html_Tags_Leaving_Escaped_Html()
        {
            string input = "<p>When you want to include <b>literal html</b> inside a web page you should escape it (e.g.: &lt;h1&gt; Title here &lt;/h1&gt;)</p>";

            // ARRANGE
            string expectedString = "When you want to include literal html inside a web page you should escape it (e.g.: &lt;h1&gt; Title here &lt;/h1&gt;)";

            // ACT
            string actualString = StringExtensions.StripTags(input);

            // ASSERT
            Assert.IsFalse(Regex.IsMatch(actualString, "<(.|\n)*?>"));
            Assert.AreEqual(expectedString, actualString);
        }
    }
}
