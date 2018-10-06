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
    public void IsValidAbsoluteUrl_Returns_False_If_Value_Is_Null_Or_White_Space(string value)
    {
      // ACT
      var result = UrlHelpers.IsValidAbsoluteUrl(value);

      // ASSERT
      Assert.IsFalse(result);
    }

    [TestCase("http://contoso.it")]
    [TestCase("http://contoso.it/")]
    [TestCase("http://contoso.it:800")]
    [TestCase("http://contoso.it:800/")]
    [TestCase("http://www.facebook.com")]
    [TestCase("http://www.facebook.com/")]
    [TestCase("http://www.facebook.com:900")]
    [TestCase("http://www.facebook.com:800/")]
    [TestCase("http://www.facebook.com/foo")]
    [TestCase("http://www.facebook.com/foo/")]
    [TestCase("http://www.facebook.com:80/foo")]
    [TestCase("http://www.facebook.com:56/foo/")]
    [TestCase("http://www.facebook.com/foo/bar")]
    [TestCase("http://www.facebook.com/foo/bar/")]
    [TestCase("http://www.facebook.com:800/foo/bar")]
    [TestCase("http://www.facebook.com:800/foo/bar/")]
    [TestCase("http://www.facebook.com/foo/bar?key=value")]
    [TestCase("http://www.facebook.com:800/foo/bar?key=value")]
    [TestCase("http://www.facebook.com/foo/bar?key=value&pippo=pluto")]
    [TestCase("http://www.facebook.com:80/foo/bar?key=value&pippo=pluto")]
    [TestCase("http://www.facebook.com/foo/bar/?key=value")]
    [TestCase("http://www.facebook.com:800/foo/bar/?key=value")]
    [TestCase("http://www.facebook.com/foo/bar/?key=value&pippo=pluto")]
    [TestCase("http://www.facebook.com:800/foo/bar/?key=value&pippo=pluto")]
    [TestCase("http://www.facebook.com:800/foo/bar/?key=value&pippo=pluto#somewhere")]
    [TestCase("http://www.facebook.com:800/foo/bar/index.html")]
    public void IsValidAbsoluteUrl_Returns_True_If_Value_Is_Http_Absolute_Url(string value)
    {
      // ACT
      var result = UrlHelpers.IsValidAbsoluteUrl(value);

      // ASSERT
      Assert.IsTrue(result);
    }

    [TestCase("https://contoso.it")]
    [TestCase("https://contoso.it/")]
    [TestCase("https://contoso.it:800")]
    [TestCase("https://contoso.it:800/")]
    [TestCase("https://www.facebook.com")]
    [TestCase("https://www.facebook.com/")]
    [TestCase("https://www.facebook.com:900")]
    [TestCase("https://www.facebook.com:800/")]
    [TestCase("https://www.facebook.com/foo")]
    [TestCase("https://www.facebook.com/foo/")]
    [TestCase("https://www.facebook.com:80/foo")]
    [TestCase("https://www.facebook.com:56/foo/")]
    [TestCase("https://www.facebook.com/foo/bar")]
    [TestCase("https://www.facebook.com/foo/bar/")]
    [TestCase("https://www.facebook.com:800/foo/bar")]
    [TestCase("https://www.facebook.com:800/foo/bar/")]
    [TestCase("https://www.facebook.com/foo/bar?key=value")]
    [TestCase("https://www.facebook.com:800/foo/bar?key=value")]
    [TestCase("https://www.facebook.com/foo/bar?key=value&pippo=pluto")]
    [TestCase("https://www.facebook.com:80/foo/bar?key=value&pippo=pluto")]
    [TestCase("https://www.facebook.com/foo/bar/?key=value")]
    [TestCase("https://www.facebook.com:800/foo/bar/?key=value")]
    [TestCase("https://www.facebook.com/foo/bar/?key=value&pippo=pluto")]
    [TestCase("https://www.facebook.com:800/foo/bar/?key=value&pippo=pluto")]
    [TestCase("https://www.facebook.com:800/foo/bar/?key=value&pippo=pluto#somewhere")]
    [TestCase("https://www.facebook.com:800/foo/bar/index.html")]
    public void IsValidAbsoluteUrl_Returns_True_If_Value_Is_Https_Absolute_Url(string value)
    {
      // ACT
      var result = UrlHelpers.IsValidAbsoluteUrl(value);

      // ASSERT
      Assert.IsTrue(result);
    }

    [TestCase("hello")]
    [TestCase("hello world")]
    [TestCase("7")]
    [TestCase("13")]
    [TestCase("12345")]
    [TestCase("http:// not an url")]
    [TestCase("https:// not an url")]
    [TestCase("foo=bar&key=value")]
    [TestCase("?foo=bar&key=value")]
    [TestCase("http")]
    [TestCase("http://")]
    [TestCase("https")]
    [TestCase("https://")]
    [TestCase("Some Random text 1 here !!! ()/89")]
    public void IsValidAbsoluteUrl_Returns_False_If_Value_Is_Not_Url(string value)
    {
      // ACT
      var result = UrlHelpers.IsValidAbsoluteUrl(value);

      // ASSERT
      Assert.IsFalse(result);
    }

    [Test]
    public void IsValidAbsoluteUrl_Returns_False_If_Value_Has_Protocol_Other_Than_Http_And_Https()
    {
      // ACT
      var result = UrlHelpers.IsValidAbsoluteUrl("ftp://ftp.funet.fi/pub/standards/RFC/rfc959.txt");

      // ASSERT
      Assert.IsFalse(result);
    }

    [Test]
    public void IsValidAbsoluteUrl_Returns_False_If_Value_Has_Implicit_Protocol()
    {
      // ACT
      var result = UrlHelpers.IsValidAbsoluteUrl("//developer.mozilla.org/en-US/docs/Learn");

      // ASSERT
      Assert.IsFalse(result);
    }

    [Test]
    public void IsValidAbsoluteUrl_Returns_False_If_Value_Has_Implicit_HostName()
    {
      // ACT
      var result = UrlHelpers.IsValidAbsoluteUrl("/en-US/docs/Learn");

      // ASSERT
      Assert.IsFalse(result);
    }

    [TestCase("Skills/Infrastructure/Understanding_URLs")]
    [TestCase("../CSS/display")]
    public void IsValidAbsoluteUrl_Returns_False_If_Value_Is_Relative_Url(string value)
    {
      // ACT
      var result = UrlHelpers.IsValidAbsoluteUrl(value);

      // ASSERT
      Assert.IsFalse(result);
    }
  }
}
