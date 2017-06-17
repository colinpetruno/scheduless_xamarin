using System;
namespace Scheduleless.Extensions
{
	public static class DateTimeExtensions
	{
		public static long ToUnixTimeStamp(this DateTime datetime)
		{
			return (long)(datetime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))).TotalSeconds;
		}

		public static DateTime FromUnixTimeStamp(this long unixTimeStamp)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp);
		}
	}
}
