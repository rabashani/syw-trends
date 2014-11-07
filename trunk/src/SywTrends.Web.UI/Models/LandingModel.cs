using System;

namespace SywTrends.Web.UI.Models
{
	public class LandingModel
	{
		public bool DisplayProperConfiguraionMessage { get; set; }
		public long AppId { get; set; }
		public Uri ShopYourWayUrl { get; set; }

		public Uri LoginPageUrl
		{
			get { return new Uri(ShopYourWayUrl, "app/" + AppId + "/login"); }
		}

		public Uri LandingPageUrl
		{
			get { return new Uri(ShopYourWayUrl, "app/" + AppId + "/l"); }
		}
	}
}