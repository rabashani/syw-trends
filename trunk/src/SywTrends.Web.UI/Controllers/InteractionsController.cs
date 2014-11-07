using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Platform.Client;
using Platform.Client.Configuration;
using SywTrends.Domain.Cache;
using SywTrends.Domain.Interests;
using SywTrends.Domain.Users;

namespace SywTrends.Web.UI.Controllers
{
	public class InteractionsController : Controller
	{
		private readonly IApplicationSettings _applicationSettings;
	    private readonly ICacheProvider _cache;

	    public InteractionsController(IApplicationSettings applicationSettings, ICacheProvider cache)
	    {
	        _applicationSettings = applicationSettings;
	        _cache = cache;
	    }

	    //Collect user Interactions /interactions/collect
        [HttpGet]
        public ActionResult Collect(string content)
		{
			// Making sure the application is configured correctly and the application is called from a canvas
			// Delete this code once this is done
			try
			{
				var appId = _applicationSettings.AppId;
				var appSecret = _applicationSettings.AppSecret;
			}
			catch (ConfigurationErrorsException)
			{
				return Json(new {error="configuarion-errors-exception"}, JsonRequestBehavior.AllowGet);
			}

            var value = StoreLocal(content);

            return Json(new { value }, JsonRequestBehavior.AllowGet);
		}

        private string StoreLocal(string content)
        {
            if (string.IsNullOrEmpty(content))
                return "empty";

            var value = _cache.Get("interactions", () =>  content );
            return value;
        }

        [HttpGet]
        public ActionResult Clean()
        {
            _cache.Clean("interactions");
            return Json(new {status = "done"}, JsonRequestBehavior.AllowGet);
        }

	}
}