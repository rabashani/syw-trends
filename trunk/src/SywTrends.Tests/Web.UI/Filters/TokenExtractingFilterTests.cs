﻿using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using SywTrends.Domain.Auth;
using SywTrends.Web.UI.Filters;

namespace SywTrends.Tests.Web.UI.Filters
{
	[TestFixture]
	class TokenExtractingFilterTests
	{
		private IPlatformTokenDistributer _platformTokenDistributer;
		private AuthorizationContext _authorizationContext;
		private TokenExtractingFilter _target;

		[SetUp]
		public void Setup()
		{
			_platformTokenDistributer = MockRepository.GenerateStub<IPlatformTokenDistributer>();
			_authorizationContext = MockRepository.GenerateStub<AuthorizationContext>();
			_target = new TokenExtractingFilter(_platformTokenDistributer);
		}

		[Test]
		public void OnAuthorization_WhenQuerystringHasNoToken_ShouldStopExecution()
		{
			NoToken();

			_target.OnAuthorization(_authorizationContext);

			_platformTokenDistributer.AssertWasNotCalled(x => x.Distribute(Arg<string>.Is.Anything));
		}

		[Test]
		public void OnAuthorization_WhenQuerystringHasToken_ShouldDistributeToken()
		{
			SetToken("Test Token");

			_target.OnAuthorization(_authorizationContext);

			_platformTokenDistributer.AssertWasCalled(x => x.Distribute(Arg<string>.Is.Equal("Test Token")));
		}

		private void NoToken()
		{
			_authorizationContext.HttpContext = new HttpContextWrapper(new HttpContext(new HttpRequest("test", "http://test.com", string.Empty), new HttpResponse(new StringWriter(new StringBuilder("test")))));
		}

		private void SetToken(string tokenValue)
		{
			_authorizationContext.HttpContext = new HttpContextWrapper(new HttpContext(new HttpRequest("test", "http://test.com", "token="+tokenValue), new HttpResponse(new StringWriter(new StringBuilder("test")))));
		}
	}
}
