using System.Globalization;
using Platform.Client;
using SywTrends.Domain.Context;
using SywTrends.Domain.Users;

namespace SywTrends.Domain.Auth
{
	public interface IPlatformTokenDistributer
	{
		void Distribute(string token);
	}

	public class PlatformTokenDistributer : IPlatformTokenDistributer
	{
		private readonly IPlatformTokenProvider _platformTokenProvider;
		private readonly IPlatformHashProvider _platformHashProvider;
		private readonly IUsersApi _usersApi;
		private readonly ILoggingContext _loggingContext;

		public PlatformTokenDistributer(IPlatformTokenProvider platformTokenProvider,
										IPlatformHashProvider platformHashProvider,
										IUsersApi usersApi,
										ILoggingContext loggingContext)
		{
			_platformTokenProvider = platformTokenProvider;
			_platformHashProvider = platformHashProvider;
			_usersApi = usersApi;
			_loggingContext = loggingContext;
		}

		public void Distribute(string token)
		{
			_platformTokenProvider.Set(token);

			_loggingContext.Set("token", token);
			_loggingContext.Set("hash", _platformHashProvider.GetHash());
			_loggingContext.Set("userId", _usersApi.Current().Id.ToString(CultureInfo.InvariantCulture));
		}
	}
}