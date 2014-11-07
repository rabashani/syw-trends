using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using Platform.Client.Configuration;
using Rhino.Mocks;
using SywTrends.Web.UI.Filters;

namespace SywTrends.Tests.Web.UI.Filters
{
	[TestFixture]
	class TokenPersistenceFilterTests
	{
		private ActionExecutingContext _actionExecutingContext;
		private IApplicationSettings _applicationSettings;
		private TokenPersistenceFilter _target;

		[SetUp]
		public void Setup()
		{
			_actionExecutingContext = MockRepository.GenerateStub<ActionExecutingContext>();
			_applicationSettings = MockRepository.GenerateStub<IApplicationSettings>();
			_applicationSettings.Stub(x => x.CookieName).Return("TestCookie");

			_target = new TokenPersistenceFilter(_applicationSettings);
		}

		[Test]
		public void OnActionExecuting_WhenQuerystringHasNoToken_ShouldStopExecution()
		{
			NoToken();
			
			_target.OnActionExecuting(_actionExecutingContext);

			_actionExecutingContext.AssertWasNotCalled(x => x.HttpContext);
		}

		[Test]
		public void OnActionExecuting_WhenQuerystringHasToken_ShouldCreateOrUpdateCookie()
		{
			SetToken("Test Token");

			_target.OnActionExecuting(_actionExecutingContext);

			Assert.That(_actionExecutingContext.HttpContext.Response.Cookies[_applicationSettings.CookieName].Value == "token=Test Token");
		}

		private void NoToken()
		{
			_actionExecutingContext.HttpContext = new HttpContextWrapper(new HttpContext(new HttpRequest("test", "http://test.com", string.Empty), new HttpResponse(new StringWriter(new StringBuilder("test")))));
		}

		private void SetToken(string tokenValue)
		{
			_actionExecutingContext.HttpContext = new HttpContextWrapper(new HttpContext(new HttpRequest("test", "http://test.com", "token=" + tokenValue), new HttpResponse(new StringWriter(new StringBuilder("test")))));
		}
	}
}
