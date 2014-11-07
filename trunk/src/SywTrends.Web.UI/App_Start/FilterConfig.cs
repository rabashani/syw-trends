using System.Web.Mvc;
using Platform.Client.Configuration;
using SywTrends.Common.Filters;
using SywTrends.Domain.Auth;
using SywTrends.Domain.Context;
using SywTrends.Web.UI.Filters;

namespace SywTrends.Web.UI.App_Start
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			var container = IoCActivator.Container;
			
			filters.Add(new HandleErrorAttribute());
			filters.Add(new TokenExtractingFilter(container.Resolve<IPlatformTokenDistributer>()));
			filters.Add(new TokenPersistenceFilter(container.Resolve<IApplicationSettings>()));
			filters.Add(new DefaultExceptionHandlingFilter(container.Resolve<IExceptionHandler>()));
			filters.Add(new RequestDataExtractorFilter(container.Resolve<IRequestContextProvider>(),
														container.Resolve<IRequestContentProvider>(),
														container.Resolve<ILoggingContext>()));
		}
	}
}