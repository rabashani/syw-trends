using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Caching;

namespace SywTrends.Domain.Cache
{
	public interface ICacheProvider
	{
	    void Clean(string key);
	    bool IsExists(string key);
		T Get<T>(string key, Func<T> loader);
	    T[] AddOrCreate<T>(string key, Func<T> loader);
	}

	public class CacheProvider : ICacheProvider
	{
        public bool IsExists(string key)
        {
            var cacheManger = MemoryCache.Default;
            if (!cacheManger.Contains(key) || cacheManger[key] == null)
            {
                return false;
            }
            return true;
        }

        public void Clean(string key)
        {
            var cacheManger = MemoryCache.Default;
            cacheManger.Remove(key);
        }

        public T[] AddOrCreate<T>(string key, Func<T> loader)
        {
            T singleResult;
            List<T> allResults;
            var cacheManger = MemoryCache.Default;
            var objresult = cacheManger[key];

   			if (!cacheManger.Contains(key) || cacheManger[key] == null)
			{
				var policy = new CacheItemPolicy();
				var funcResult = loader();

				if (funcResult == null)
					singleResult = default(T);
				else
					singleResult = (T)funcResult;

   			    allResults = new List<T>{singleResult};

				cacheManger.Add(key, allResults, policy);
			}
			else
   			{
   			    allResults = (List<T>)cacheManger[key];
   			    var funcResult = loader();
                allResults.Add((T)funcResult);
   			    cacheManger[key] = allResults;
   			}


            return allResults.ToArray();
        }

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