using System.Web.Mvc;
using System.Web.Routing;

namespace SywTrends.Web.UI.App_Start
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"PostLogin",
				"post-login",
				new {controller = "PostLogin", action = "Index"}
				);

			routes.MapRoute(
				"Default",
				"{controller}/{action}/{id}",
				defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
				);
		}
	}
}