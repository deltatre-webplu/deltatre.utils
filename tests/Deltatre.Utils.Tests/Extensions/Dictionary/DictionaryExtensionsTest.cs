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
			// ARRABGE
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
	}
}

