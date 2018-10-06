using Deltatre.Utils.Validators;
using NUnit.Framework;

namespace Deltatre.Utils.Tests.Validators
{
  [TestFixture]
  public class UrlHelpersTest
  {
    [TestCase(null)]
    [TestCase("")]
    [TestCase("    ")]
    public void IsValidAbsoluteUrl_Returns_False_If_Url_Is_Null_Or_White_Space(string url)
    {
      // ACT
      var result = UrlHelpers.IsValidAbsoluteUrl(url);

      // ASSERT
      Assert.IsFalse(result);
    }

    [TestCase("http://contoso.it")]
    [TestCase("http://contoso.it/")]
    [TestCase("http://www.facebook.com")]
    [TestCase("http://www.facebook.com/")]
    [TestCase("http://www.facebook.com/foo")]
    [TestCase("http://www.facebook.com/foo/")]
    [TestCase("http://www.facebook.com/foo/bar")]
    [TestCase("http://www.facebook.com/foo/bar/")]
    [TestCase("http://www.facebook.com/foo/bar?key=value")]
    [TestCase("http://www.facebook.com/foo/bar?key=value&pippo=pluto")]
    [TestCase("http://www.facebook.com/foo/bar/?key=value")]
    [TestCase("http://www.facebook.com/foo/bar/?key=value&pippo=pluto")]
    public void IsValidAbsoluteUrl_Returns_True_If_Url_Is_Http_Absolute_Url(string url)
    {
      // ACT
      var result = UrlHelpers.IsValidAbsoluteUrl(url);

      // ASSERT
      Assert.IsTrue(result);
    }

    [TestCase("https://contoso.it")]
    [TestCase("https://contoso.it/")]
    [TestCase("https://www.facebook.com")]
    [TestCase("https://www.facebook.com/")]
    [TestCase("https://www.facebook.com/foo")]
    [TestCase("https://www.facebook.com/foo/")]
    [TestCase("https://www.facebook.com/foo/bar")]
    [TestCase("https://www.facebook.com/foo/bar/")]
    [TestCase("https://www.facebook.com/foo/bar?key=value")]
    [TestCase("https://www.facebook.com/foo/bar?key=value&pippo=pluto")]
    [TestCase("https://www.facebook.com/foo/bar/?key=value")]
    [TestCase("https://www.facebook.com/foo/bar/?key=value&pippo=pluto")]
    public void IsValidAbsoluteUrl_Returns_True_If_Url_Is_Https_Absolute_Url(string url)
    {
      // ACT
      var result = UrlHelpers.IsValidAbsoluteUrl(url);

      // ASSERT
      Assert.IsTrue(result);
    }

  }
}
