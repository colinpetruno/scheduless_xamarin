using System;
using System.Collections.Generic;
using System.Diagnostics;
using Scheduleless.Models;

namespace Scheduleless.Services
{
	public class RequestCacheService
	{
		const int CacheExpireTimeInMinutes = 30;
		private static readonly Lazy<RequestCacheService> lazy = new Lazy<RequestCacheService>(() => new RequestCacheService());
		public static RequestCacheService Instance
		{
			get
			{
				return lazy.Value;
			}
		}

		Dictionary<string, RequestCache> _requestResponseData = new Dictionary<string, RequestCache>();

		public void SaveDataIfNeededFor(string requestKey, string responseData, RequestCachePolicy cachePolicy)
		{
			// update if expired
			if (cachePolicy != RequestCachePolicy.Ignore)
			{
				// refresh cache, if needed
				if (DoesCacheKeyExistFor(requestKey) && HasCacheExpired(GetRequestCacheFor(requestKey).CreatedAt))
				{
					_requestResponseData[requestKey] = new RequestCache { Data = responseData, CreatedAt = DateTime.UtcNow };
					Debug.WriteLine($"RequestCacheService - cacheKey: {requestKey} has expired. Refreshing key.");
				}
				else if (!DoesCacheKeyExistFor(requestKey))
				{
					_requestResponseData[requestKey] = new RequestCache { Data = responseData, CreatedAt = DateTime.UtcNow };
					Debug.WriteLine($"RequestCacheService - cacheKey: {requestKey} does not exist. Creating a key.");
				}
			}
		}

		public bool DoesCacheKeyExistFor(string requestKey)
		{
			return _requestResponseData.ContainsKey(requestKey);
		}

		public RequestCache GetRequestCacheFor(string requestKey)
		{
			return _requestResponseData[requestKey] as RequestCache;
		}

		public string GetResponseKeyFor(string requestKey)
		{
			if (DoesCacheKeyExistFor(requestKey))
			{
				var requestCache = GetRequestCacheFor(requestKey);

				if (!HasCacheExpired(requestCache.CreatedAt))
				{
					Debug.WriteLine($"RequestCacheService - cacheKey: {requestKey} found. Skipping request call.");
					return requestCache.Data;
				}
				else
				{
					Debug.WriteLine($"RequestCacheService - No cacheKey found for {requestKey}.");
					return null;
				}
			}
			else
			{
				return null;
			}
		}

		public void DeleteCacheKeyIfNeeded(string requestKey)
		{
			if (DoesCacheKeyExistFor(requestKey))
			{
				_requestResponseData.Remove(requestKey);
				Debug.WriteLine($"RequestCacheService - Removing: {requestKey}.");
			}
		}

		private bool HasCacheExpired(DateTime cacheCreateTime)
		{
			if (cacheCreateTime.AddMinutes(CacheExpireTimeInMinutes) <= DateTime.UtcNow)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
