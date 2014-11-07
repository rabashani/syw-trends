using System.Web;

namespace SywTrends.Domain.Context
{
	public interface IRequestContextProvider
	{
		HttpRequest Get();
	}

	public class RequestContextProvider : IRequestContextProvider
	{
		public HttpRequest Get()
		{
			return HttpContext.Current == null ? null : HttpContext.Current.Request;
		}
	}
}