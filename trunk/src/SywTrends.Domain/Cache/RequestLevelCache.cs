using System;
using Platform.Client.Common.Context;

namespace SywTrends.Domain.Cache
{
	public interface IRequestLevelCache
	{
		T Get<T>(string key, Func<string, T> getter) where T : class;
		void Invalidate<T>(string key) where T : class;
	}

	public class RequestLevelCache : IRequestLevelCache
	{
		private readonly IContextProvider _contextProvider;

		public RequestLevelCache(IContextProvider contextProvider)
		{
			_contextProvider = contextProvider;
		}

		public T Get<T>(string key, Func<string, T> getter) where T : class
		{
			var val = _contextProvider.Get<T>(key);

			if (val == null)
			{
				val = getter(key);
				_contextProvider.Set(key, val);
			}

			return val;
		}

		public void Invalidate<T>(string key) where T : class
		{
			_contextProvider.Set<T>(key, null);
		}
	}
}