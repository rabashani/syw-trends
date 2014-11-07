using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Platform.Client;
using Platform.Client.Configuration;
using SywTrends.Domain.Interests;
using SywTrends.Domain.Users;

namespace SywTrends.Web.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly IApplicationSettings _applicationSettings;
		private readonly IPlatformTokenProvider _platformTokenProvider;
		private readonly IUsersApi _usersApi;
		private readonly IPlatformSettings _platformSettings;
	    private readonly IInterestsApi _interestsApi;

	    public HomeController(IApplicationSettings applicationSettings, IPlatformTokenProvider platformTokenProvider, IUsersApi usersApi, IPlatformSettings platformSettings, IInterestsApi interestsApi)
		{
			_applicationSettings = applicationSettings;
			_platformTokenProvider = platformTokenProvider;
			_usersApi = usersApi;
			_platformSettings = platformSettings;
	        _interestsApi = interestsApi;
		}

		public ActionResult Index()
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
				return Redirect("/landing");
			}

			if (_platformTokenProvider.Get() == null)
				return Redirect("/landing");

			var currentUser = _usersApi.Current();
			var currentUserFollowing = _usersApi.GetFollowing(currentUser.Id);
		    var myInterests = _interestsApi.Get();

			return View(ToModel(currentUser, currentUserFollowing));
		}

		private UserModel ToModel(UserDto userDto, IEnumerable<UserDto> following = null)
		{
			return new UserModel
						{
							Id = userDto.Id,
							Name = userDto.Name,
							ImageUrl = userDto.ImageUrl,
							ProfileUrl = new Uri(_platformSettings.SywWebSiteUrl, userDto.ProfileUrl),
							Following = following == null ?
											new UserModel[0] :
											following.Select(x => ToModel(x)).ToArray()
						};
		}
	}

	public class UserModel
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public Uri ProfileUrl { get; set; }
		public IList<UserModel> Following { get; set; }
	}
}