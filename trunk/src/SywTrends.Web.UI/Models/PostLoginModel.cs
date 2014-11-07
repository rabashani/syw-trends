using System;

namespace SywTrends.Web.UI.Models
{
	public class PostLoginModel
	{
		public PostLoginModel(long appId, Uri shopYourWayUrl)
		{
			CanvasPageUrl = new Uri(shopYourWayUrl, "app/" + appId + "/r");
		}

		public Uri CanvasPageUrl { get; private set; }
	}
}