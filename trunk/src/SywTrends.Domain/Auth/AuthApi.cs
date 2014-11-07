using Platform.Client;
using System;

namespace SywTrends.Domain.Auth
{
	public interface IAuthApi
	{
		UserState GetUserState();
		string GetOfflineToken(long userId, long appId, DateTime timestamp, string signature);
		string GetAnonymousOfflineToken(long appId, DateTime timestamp, string signature);
	}

	public class AuthApi : ApiBase, IAuthApi
	{
		public AuthApi(IPlatformProxy platformProxy) : base(platformProxy) { }

		protected override string BasePath
		{
			get { return "auth"; }
		}

		public UserState GetUserState()
		{
			return Get<UserState>("user-state");
		}

		public string GetOfflineToken(long userId, long appId, DateTime timestamp, string signature)
		{
			return Proxy.SecuredAnonymousGet<string>(GetEndpointPath("get-token"),
													new
														{
															UserId = userId,
															AppId = appId,
															Timestamp = new DateTime(timestamp.Year,
																					timestamp.Month,
																					timestamp.Day,
																					timestamp.Hour,
																					timestamp.Minute,
																					timestamp.Second),
															Signature = signature
														});
		}

		public string GetAnonymousOfflineToken(long appId, DateTime timestamp, string signature)
		{
			return Proxy.SecuredAnonymousGet<string>(GetEndpointPath("get-token"),
													new
														{
															AppId = appId,
															Timestamp = new DateTime(timestamp.Year,
																					timestamp.Month,
																					timestamp.Day,
																					timestamp.Hour,
																					timestamp.Minute,
																					timestamp.Second),
															Signature = signature
														});
		}
	}
}