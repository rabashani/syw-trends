using System.Web.Mvc;
using Castle.Core.Logging;

namespace SywTrends.Common.Filters
{
	public interface IExceptionHandler
	{
		void Handle(ExceptionContext filterContext);
	}

	public class ExceptionHandler : IExceptionHandler
	{
		private readonly ILoggerFactory _loggerFactory;

		public ExceptionHandler(ILoggerFactory loggerFactory)
		{
			_loggerFactory = loggerFactory;
		}

		public void Handle(ExceptionContext filterContext)
		{
			var logger = _loggerFactory.Create(filterContext.Controller.ControllerContext.Controller.GetType());

			logger.Error(filterContext.Exception.Message, filterContext.Exception);
		}
	}
}