using System;
using Newtonsoft.Json.Linq;

namespace Scheduleless.Extensions
{
	public static class JTokenExtensions
	{
		// Reference: http://stackoverflow.com/questions/24066400/checking-for-empty-null-jtoken-in-a-jobject
		public static bool IsNullOrEmpty(this JToken token)
		{
			return (token == null) ||
				   (token.Type == JTokenType.Array && !token.HasValues) ||
				   (token.Type == JTokenType.Object && !token.HasValues) ||
				   (token.Type == JTokenType.String && token.ToString() == string.Empty) ||
				   (token.Type == JTokenType.Null);
		}
	}
}
