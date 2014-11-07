﻿using System.Configuration;
using System.Web.Mvc;
using Platform.Client;
using Platform.Client.Common.Context;
using Platform.Client.Configuration;
using SywTrends.Domain.Configuration;
using SywTrends.Web.UI.Models;

namespace SywTrends.Web.UI.Controllers
{
	public class PostLoginController : Controller
	{
		private readonly IApplicationSettings _applicationSettings;
		private readonly PlatformTokenProvider _platformTokenProvider;
		private readonly IPlatformSettings _platformSettings;

		public PostLoginController()
		{
			_applicationSettings = new ApplicationSettings();
			_platformSettings = new PlatformSettings();
			_platformTokenProvider = new PlatformTokenProvider(new HttpContextProvider());
		}

		public ActionResult Index()
		{
			long appId;

			// Making sure the application is configured correctly and the application is called from a canvas
			// Delete this code once this is done
			try
			{
				appId = _applicationSettings.AppId;
				var appSecret = _applicationSettings.AppSecret;
			}
			catch (ConfigurationErrorsException)
			{
				return Redirect("/landing");
			}

			if (_platformTokenProvider.Get() == null)
				return Redirect("/landing");

			return View(new PostLoginModel(appId, _platformSettings.SywWebSiteUrl));
		}
	}
}