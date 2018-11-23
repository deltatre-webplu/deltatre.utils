using Deltatre.Utils.Extensions.String;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Deltatre.Utils.Tests.Extensions.String
{
  [TestFixture]
  public partial class StringExtensionsTest
  {
    [Test]
    public void Truncate_Throws_ArgumentOutOfRangeException_When_Parameter_MaximumAllowedLength_Is_Less_Than_Zero()
    {
      // ARRANGE
      const string source = "Hello world";
      const string ellipsis = "...";

      // ACT
      Assert.Throws<ArgumentOutOfRangeException>(() => source.Truncate(-3, ellipsis));
    }

    [TestCase(null)]
    [TestCase("")]
    public void Truncate_Returns_Source_String_When_Source_String_Is_Null_Or_Empty(string source)
    {
      // ACT
      var result = source.Truncate(5, "...");

      // ASSERT
      Assert.AreEqual(source, result);
    }

    [TestCaseSource(nameof(GetTestCasesForSourceShorterThanMaxAllowedLength))]
    public void Truncate_Returns_Source_When_Source_Lenght_Is_Less_Than_Max_Allowed_Length(
      (string source, int maxLength) tuple)
    {
      // ACT
      var result = tuple.source.Truncate(tuple.maxLength, "...");

      // ASSERT
      Assert.AreEqual(tuple.source, result);
    }

    [TestCaseSource(nameof(GetTestCasesForSourceLengthEqualToMaxAllowedLength))]
    public void Truncate_Returns_Source_When_Source_Lenght_Equals_Max_Allowed_Length(
      (string source, int maxLength) tuple)
    {
      // ACT
      var result = tuple.source.Truncate(tuple.maxLength, "...");

      // ASSERT
      Assert.AreEqual(tuple.source, result);
    }

    [TestCaseSource(nameof(GetTestCasesForSourceLengthGreaterThanMaxAllowedLength))]
    public void Truncate_Returns_Substring_When_Source_Lenght_Exceeds_Max_Allowed_Length(
      (string source, int maxLength, string ellipsis, string expected) tuple)
    {
      // ACT
      var result = tuple.source.Truncate(tuple.maxLength, tuple.ellipsis);

      // ASSERT
      Assert.AreEqual(tuple.expected, result);
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("    ")]
    public void Truncate_Does_Not_Add_Ellipsis_If_Ellipsis_Is_Null_Or_White_Space_And_Source_String_Is_Truncated(
      string ellipsis)
    {
      // ACT
      var result = "ciao".Truncate(2, ellipsis);

      // ASSERT
      Assert.AreEqual("ci", result);
    }

    private static IEnumerable<(string source, int maxLength)>
      GetTestCasesForSourceShorterThanMaxAllowedLength()
    {
      yield return ("ciao", 5);
      yield return ("   ", 4);
      yield return ("hello world", 12);
    }

    private static IEnumerable<(string source, int maxLength)>
      GetTestCasesForSourceLengthEqualToMaxAllowedLength()
    {
      yield return ("ciao", 4);
      yield return ("   ", 4);
      yield return ("hello world", 11);
      yield return (string.Empty, 0);
    }

    private static IEnumerable<(string source, int maxLength, string ellipsis, string expected)>
      GetTestCasesForSourceLengthGreaterThanMaxAllowedLength()
    {
      yield return ("ciao", 0, "...", "...");
      yield return ("hello world", 0, "...", "...");

      yield return ("ciao", 3, "...", "cia...");
      yield return ("hello world", 3, "...", "hel...");
      yield return ("hello world", 5, "...", "hello...");
      yield return ("hello world", 6, "...", "hello ...");
      yield return ("hello world", 7, "...", "hello w...");

      yield return ("    ", 3, "...", "   ...");
    }
  }
}
