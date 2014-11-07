using System;
using System.Web;
using System.Web.Mvc;
using SywTrends.Domain.Context;

namespace SywTrends.Common.Filters
{
	public class RequestDataExtractorFilter : IActionFilter
	{
		private readonly IRequestContextProvider _requestContextProvider;
		private readonly IRequestContentProvider _requestContentProvider;
		private readonly ILoggingContext _loggingContext;

		public RequestDataExtractorFilter(IRequestContextProvider requestContextProvider,
										IRequestContentProvider requestContentProvider,
										ILoggingContext loggingContext)
		{
			_requestContextProvider = requestContextProvider;
			_requestContentProvider = requestContentProvider;
			_loggingContext = loggingContext;
		}

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var request = _requestContextProvider.Get();

			_loggingContext.Set("requestUrl", request.RawUrl);
			_loggingContext.Set("clientIP", GetClientIP(request));
			_loggingContext.Set("requestContent", _requestContentProvider.Get());
		}

		private static string GetClientIP(HttpRequest request)
		{
			try
			{
				var xForwardForIps = request.ServerVariables["X-Forwarded-For"];
				if (!String.IsNullOrEmpty(xForwardForIps))
				{
					var ips = xForwardForIps.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
					return ips[0];
				}

				return request.UserHostAddress;
			}
			catch (HttpException)
			{
				return null;
			}
		}

		public void OnActionExecuted(ActionExecutedContext filterContext) {}
	}
}