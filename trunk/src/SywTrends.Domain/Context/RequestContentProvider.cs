using System.IO;
using System.Web;
using Platform.Client.Common.Context;

namespace SywTrends.Domain.Context
{
	public interface IRequestContentProvider
	{
		string Get();
	}

	public class RequestContentProvider : IRequestContentProvider
	{
		private const string Key = "request:content";

		private readonly IContextProvider _contextProvider;
		private readonly IRequestContextProvider _requestContextProvider;

		public RequestContentProvider(IContextProvider contextProvider,
									IRequestContextProvider requestContextProvider)
		{
			_contextProvider = contextProvider;
			_requestContextProvider = requestContextProvider;
		}

		public string Get()
		{
			var content = _contextProvider.Get<string>(Key);

			if (!string.IsNullOrEmpty(content))
				return content;

			var request = _requestContextProvider.Get();
			content = GetRawContent(request);
			_contextProvider.Set(Key, content);

			return content;
		}

		private static string GetRawContent(HttpRequest request)
		{
			try
			{
				request.InputStream.Seek(0, SeekOrigin.Begin);

				using (var input = new StreamReader(request.InputStream))
					return input.ReadToEnd();
			}
			catch
			{
				return string.Empty;
			}
			finally
			{
				request.InputStream.Seek(0, SeekOrigin.End);
			}
		}
	}
}