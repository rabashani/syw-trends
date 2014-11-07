using System;
using System.Web.Mvc;
using Platform.Client.Configuration;

namespace SywTrends.Web.UI.Filters
{
	public class TokenPersistenceFilter : IActionFilter
	{
		private readonly IApplicationSettings _applicationSettings;

		public TokenPersistenceFilter(IApplicationSettings applicationSettings)
		{
			_applicationSettings = applicationSettings;
		}

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			CreateOrUpdateCookie(filterContext);
		}

		private void CreateOrUpdateCookie(ActionExecutingContext filterContext)
		{
			var token = filterContext.HttpContext.Request["token"];

			if (String.IsNullOrEmpty(token)) return;

			var httpCookie = filterContext.HttpContext.Response.Cookies[_applicationSettings.CookieName];
			if (httpCookie != null) httpCookie["token"] = token;
		}

		public void OnActionExecuted(ActionExecutedContext filterContext) {}
	}
}