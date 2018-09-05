using System;
using System.Collections.Generic;
using NUnit.Framework;
using static Deltatre.Utils.Parsers.DateParsers;

namespace Deltatre.Utils.Tests.Parsers
{
	[TestFixture]
	public class DateParsersTest
	{
		[TestCase(null)]
		[TestCase("")]
		[TestCase("    ")]
		public void ParseIso8601Date_Throws_When_Parameter_ToBeParsedIsNullOrWhiteSpace(string toBeParsed)
		{
			// ACT
			Assert.Throws<ArgumentException>(() => ParseIso8601Date(toBeParsed));
		}

		// see https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Sortable
		[Test]
		public void ParseIso8601Date_Support_Microsoft_Sortable_Format_Specifier()
		{
			// ARRANGE
			const string toBeParsed = "2008-04-10T06:30:00";

			// ACT
			var result = ParseIso8601Date(toBeParsed);
			
			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			
			// Check parsed value
			var expected = new DateTimeOffset(new DateTime(2008, 4, 10, 6, 30, 0), TimeSpan.FromHours(2));
			Assert.AreEqual(expected, result.Output);
		}

		// see https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#the-round-trip-o-o-format-specifier
		[TestCaseSource(nameof(Microsoft_RoundTrip_Format_Test_Case_Source))]
		public void ParseIso8601Date_Support_Microsoft_RoundTrip_Format_Specifier((string toBeParsed, DateTimeOffset expected) tuple)
		{
			// ACT
			var result = ParseIso8601Date(tuple.toBeParsed);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(tuple.expected, result.Output);
		}

		[TestCaseSource(nameof(Extended_Formats_Test_Case_Source))]
		public void ParseIso8601Date_Support_Extended_Formats((string toBeParsed, DateTimeOffset expected) tuple)
		{
			// ACT
			var result = ParseIso8601Date(tuple.toBeParsed);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(tuple.expected, result.Output);
		}

		[TestCaseSource(nameof(Basic_Formats_Test_Case_Source))]
		public void ParseIso8601Date_Support_Basic_Formats((string toBeParsed, DateTimeOffset expected) tuple)
		{
			// ACT
			var result = ParseIso8601Date(tuple.toBeParsed);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(tuple.expected, result.Output);
		}

		[TestCaseSource(nameof(Extended_Formats_With_Minutes_Accuracy_Test_Case_Source))]
		public void ParseIso8601Date_Support_Extended_Formats_With_Minutes_Accuracy((string toBeParsed, DateTimeOffset expected) tuple)
		{
			// ACT
			var result = ParseIso8601Date(tuple.toBeParsed);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(tuple.expected, result.Output);
		}

		[TestCaseSource(nameof(Basic_Formats_With_Minutes_Accuracy_Test_Case_Source))]
		public void ParseIso8601Date_Support_Basic_Formats_With_Minutes_Accuracy((string toBeParsed, DateTimeOffset expected) tuple)
		{
			// ACT
			var result = ParseIso8601Date(tuple.toBeParsed);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(tuple.expected, result.Output);
		}

		[TestCaseSource(nameof(Extended_Formats_With_Hours_Accuracy_Test_Case_Source))]
		public void ParseIso8601Date_Support_Extended_Formats_With_Hours_Accuracy((string toBeParsed, DateTimeOffset expected) tuple)
		{
			// ACT
			var result = ParseIso8601Date(tuple.toBeParsed);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(tuple.expected, result.Output);
		}

		[TestCaseSource(nameof(Basic_Formats_With_Hours_Accuracy_Test_Case_Source))]
		public void ParseIso8601Date_Support_Basic_Formats_With_Hours_Accuracy((string toBeParsed, DateTimeOffset expected) tuple)
		{
			// ACT
			var result = ParseIso8601Date(tuple.toBeParsed);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);
			Assert.AreEqual(tuple.expected, result.Output);
		}

		[TestCase("not a date")]
		[TestCase("28 November 1988 13:45:60")]
		[TestCase("Monday 21st May 2018")]
		[TestCase("2011-January-12")]
		[TestCase("foo")]
		public void ParseIso8601Date_Returns_Invalid_Result_When_String_Passed_In_Is_Not_Iso8601_Date(string toBeParsed)
		{
			// ACT
			var result = ParseIso8601Date(toBeParsed);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsFalse(result.IsSuccess);
			Assert.AreEqual(default(DateTimeOffset), result.Output);
		}

		[TestCase("2018-05-21")]
		[TestCase("20180521")]
		public void ParseIso8601Date_Supports_Formats_With_Date_Only(string toBeParsed)
		{
			// ACT
			var result = ParseIso8601Date(toBeParsed);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsSuccess);

			// check parsed value
			var expected = new DateTimeOffset(new DateTime(2018, 5, 21), TimeSpan.FromHours(2));
			Assert.AreEqual(expected, result.Output);
		}

		private static IEnumerable<(string toBeParsed, DateTimeOffset expected)> Microsoft_RoundTrip_Format_Test_Case_Source()
		{
			yield return (
				"2009-06-15T13:45:30.0080000",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30, 8), TimeSpan.FromHours(2))
			);

			yield return (
				"2009-06-15T13:45:30.0080000Z",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30, 8), TimeSpan.Zero)
			);

			yield return (
				"2009-06-15T13:45:30.0080000-07:00",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30, 8), TimeSpan.FromHours(-7))
			);

			yield return (
				"2009-06-15T13:45:30.0080000+03:00",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30, 8), TimeSpan.FromHours(3))
			);

			yield return (
				"2009-06-15T13:45:30.0080000+00:00",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30, 8), TimeSpan.Zero)
			);
		}

		private static IEnumerable<(string toBeParsed, DateTimeOffset expected)> Extended_Formats_Test_Case_Source()
		{
			// not padded without minutes

			yield return (
				"2009-06-15T13:45:30-7",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.FromHours(-7))
			);

			yield return (
				"2009-06-15T13:45:30+3",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.FromHours(3))
			);

			yield return (
				"2009-06-15T13:45:30+0",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.Zero)
			);

			// padded without minutes

			yield return (
				"2009-06-15T13:45:30-07",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.FromHours(-7))
			);

			yield return (
				"2009-06-15T13:45:30+03",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.FromHours(3))
			);

			yield return (
				"2009-06-15T13:45:30+00",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.Zero)
			);

			// with minutes

			yield return (
				"2009-06-15T13:45:30-07:03",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), new TimeSpan(0, -7, -3, 0))
			);

			yield return (
				"2009-06-15T13:45:30+04:07",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), new TimeSpan(0, 4, 7, 0))
			);

			yield return (
				"2009-06-15T13:45:30+00:00",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.Zero)
			);

			// UTC

			yield return (
				"2009-06-15T13:45:30Z",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.Zero)
			);
		}

		private static IEnumerable<(string toBeParsed, DateTimeOffset expected)> Basic_Formats_Test_Case_Source()
		{
			// not padded without minutes

			yield return (
				"20090615T134530-7",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.FromHours(-7))
			);

			yield return (
				"20090615T134530+3",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.FromHours(3))
			);

			yield return (
				"20090615T134530+0",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.Zero)
			);

			// padded without minutes

			yield return (
				"20090615T134530-07",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.FromHours(-7))
			);

			yield return (
				"20090615T134530+03",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.FromHours(3))
			);

			yield return (
				"20090615T134530+00",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.Zero)
			);

			// with minutes

			yield return (
				"20090615T134530-0703",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), new TimeSpan(0, -7, -3, 0))
			);

			yield return (
				"20090615T134530+0407",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), new TimeSpan(0, 4, 7, 0))
			);

			yield return (
				"20090615T134530+0000",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.Zero)
			);

			// UTC

			yield return (
				"20090615T134530Z",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 30), TimeSpan.Zero)
			);
		}

		private static IEnumerable<(string toBeParsed, DateTimeOffset expected)> Extended_Formats_With_Minutes_Accuracy_Test_Case_Source()
		{
			// not padded without minutes

			yield return (
				"2009-06-15T13:45-7",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.FromHours(-7))
			);

			yield return (
				"2009-06-15T13:45+3",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.FromHours(3))
			);

			yield return (
				"2009-06-15T13:45+0",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.Zero)
			);

			// padded without minutes

			yield return (
				"2009-06-15T13:45-07",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.FromHours(-7))
			);

			yield return (
				"2009-06-15T13:45+03",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.FromHours(3))
			);

			yield return (
				"2009-06-15T13:45+00",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.Zero)
			);

			// with minutes

			yield return (
				"2009-06-15T13:45-07:03",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), new TimeSpan(0, -7, -3, 0))
			);

			yield return (
				"2009-06-15T13:45+04:07",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), new TimeSpan(0, 4, 7, 0))
			);

			yield return (
				"2009-06-15T13:45+00:00",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.Zero)
			);

			// UTC

			yield return (
				"2009-06-15T13:45Z",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.Zero)
			);
		}

		private static IEnumerable<(string toBeParsed, DateTimeOffset expected)> Basic_Formats_With_Minutes_Accuracy_Test_Case_Source()
		{
			// not padded without minutes

			yield return (
				"20090615T1345-7",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.FromHours(-7))
			);

			yield return (
				"20090615T1345+3",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.FromHours(3))
			);

			yield return (
				"20090615T1345+0",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.Zero)
			);

			// padded without minutes

			yield return (
				"20090615T1345-07",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.FromHours(-7))
			);

			yield return (
				"20090615T1345+03",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.FromHours(3))
			);

			yield return (
				"20090615T1345+00",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.Zero)
			);

			// with minutes

			yield return (
				"20090615T1345-0703",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), new TimeSpan(0, -7, -3, 0))
			);

			yield return (
				"20090615T1345+0407",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), new TimeSpan(0, 4, 7, 0))
			);

			yield return (
				"20090615T1345+0000",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.Zero)
			);

			// UTC

			yield return (
				"20090615T1345Z",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 45, 0), TimeSpan.Zero)
			);
		}

		private static IEnumerable<(string toBeParsed, DateTimeOffset expected)> Extended_Formats_With_Hours_Accuracy_Test_Case_Source()
		{
			// not padded without minutes

			yield return (
				"2009-06-15T13-7",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.FromHours(-7))
			);

			yield return (
				"2009-06-15T13+3",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.FromHours(3))
			);

			yield return (
				"2009-06-15T13+0",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.Zero)
			);

			// padded without minutes

			yield return (
				"2009-06-15T13-07",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.FromHours(-7))
			);

			yield return (
				"2009-06-15T13+03",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.FromHours(3))
			);

			yield return (
				"2009-06-15T13+00",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.Zero)
			);

			// with minutes

			yield return (
				"2009-06-15T13-07:03",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), new TimeSpan(0, -7, -3, 0))
			);

			yield return (
				"2009-06-15T13+04:07",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), new TimeSpan(0, 4, 7, 0))
			);

			yield return (
				"2009-06-15T13+00:00",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.Zero)
			);

			// UTC

			yield return (
				"2009-06-15T13Z",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.Zero)
			);
		}

		private static IEnumerable<(string toBeParsed, DateTimeOffset expected)> Basic_Formats_With_Hours_Accuracy_Test_Case_Source()
		{
			// not padded without minutes

			yield return (
				"20090615T13-7",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.FromHours(-7))
			);

			yield return (
				"20090615T13+3",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.FromHours(3))
			);

			yield return (
				"20090615T13+0",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.Zero)
			);

			// padded without minutes

			yield return (
				"20090615T13-07",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.FromHours(-7))
			);

			yield return (
				"20090615T13+03",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.FromHours(3))
			);

			yield return (
				"20090615T13+00",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.Zero)
			);

			// with minutes

			yield return (
				"20090615T13-0703",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), new TimeSpan(0, -7, -3, 0))
			);

			yield return (
				"20090615T13+0407",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), new TimeSpan(0, 4, 7, 0))
			);

			yield return (
				"20090615T13+0000",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.Zero)
			);

			// UTC

			yield return (
				"20090615T13Z",
				new DateTimeOffset(new DateTime(2009, 6, 15, 13, 0, 0), TimeSpan.Zero)
			);
		}
	}
}
