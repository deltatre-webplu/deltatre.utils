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
			Assert.IsTrue(result.IsValid);
			
			// Check parsed value
			var expected = new DateTimeOffset(new DateTime(2008, 4, 10, 6, 30, 0), TimeSpan.FromHours(2));
			Assert.AreEqual(expected, result.ParsedValue);
		}

		// see https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#the-round-trip-o-o-format-specifier
		[TestCaseSource(nameof(Microsoft_RoundTrip_Format_Test_Case_Source))]
		public void ParseIso8601Date_Support_Microsoft_RoundTrip_Format_Specifier((string toBeParsed, DateTimeOffset expected) tuple)
		{
			// ACT
			var result = ParseIso8601Date(tuple.toBeParsed);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsValid);
			Assert.AreEqual(tuple.expected, result.ParsedValue);
		}

		[TestCaseSource(nameof(Extended_Formats_Test_Case_Source))]
		public void ParseIso8601Date_Support_Extended_Formats((string toBeParsed, DateTimeOffset expected) tuple)
		{
			// ACT
			var result = ParseIso8601Date(tuple.toBeParsed);

			// ASSERT
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsValid);
			Assert.AreEqual(tuple.expected, result.ParsedValue);
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
	}
}
