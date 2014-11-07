using Platform.Client;

namespace SywTrends.Domain
{
	public abstract class ApiBase
	{
		protected IPlatformProxy Proxy { get; private set; }

		protected abstract string BasePath { get; }

		protected ApiBase(IPlatformProxy platformProxy)
		{
			Proxy = platformProxy;
		}

		protected string GetEndpointPath(string endpoint)
		{
			return string.Format("/{0}/{1}", BasePath, endpoint);
		}

		protected T Get<T>(string endpoint, object parametersModel = null)
		{
			return Proxy.Get<T>(GetEndpointPath(endpoint), parametersModel);
		}

		protected T Post<T>(string endpoint, object parametersModel = null)
		{
			return Proxy.Post<T>(GetEndpointPath(endpoint), parametersModel);
		}
	}
}
