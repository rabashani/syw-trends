using System;
using System.Runtime.Caching;

namespace SywTrends.Domain.Cache
{
	public interface ICacheProvider
	{
		T Get<T>(string key, Func<T> loader);
	}

	public class CacheProvider : ICacheProvider
	{
		public T Get<T>(string key, Func<T> loader)
		{
			T result;

			var cacheManger = MemoryCache.Default;
			if (!cacheManger.Contains(key) || cacheManger[key] == null)
			{
				var policy = new CacheItemPolicy();
				var funcResult = loader();

				if (funcResult == null)
					result = default(T);
				else
					result = (T)funcResult;

				cacheManger.Add(key, funcResult, policy);
			}
			else
			{
				result = (T)cacheManger[key];
			}
			return result;
		}
	}
}