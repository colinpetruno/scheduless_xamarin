using System;
namespace Scheduleless.Models
{
	public enum RequestCachePolicy
	{
		Ignore,
		RefreshIfNeeded,
		AlwaysGetCache
	}

	public class RequestCache
	{
		public string Data { get; set; }
		public DateTime CreatedAt { get; set; }
		public RequestCachePolicy CachePolicy { get; set; }
	}
}
