using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scheduleless.Extensions;

namespace Scheduleless.Models
{
	// NOTE: these keys are mapped 1:1 to the OAuth's [JsonProperty("key")]
	// If either change, the other should change; otherwise the ToObject will not work
	public struct JsonOAuthKeys
	{
		public const string AccessToken = "access_token";
		public const string TokenType = "token_type";
		public const string RefreshToken = "refresh_token";
		public const string ExpiresIn = "expires_in";
		public const string CreatedAt = "created_at";
	}

	public class OAuth
	{
		public struct Constants
		{
			public const int ExpirationBuffer = 5;
		}

		[JsonProperty(JsonOAuthKeys.AccessToken)]
		public string AccessToken { get; set; }

		[JsonProperty(JsonOAuthKeys.TokenType)]
		public string TokenType { get; set; }

		[JsonProperty(JsonOAuthKeys.ExpiresIn)]
		public int ExpiresIn { get; set; }

		[JsonProperty(JsonOAuthKeys.RefreshToken)]
		public string RefreshToken { get; set; }

		[JsonProperty(JsonOAuthKeys.CreatedAt)]
		public long CreatedAt { get; set; }

		// Validates that the current access token expiration time is less than the current time minus the expiration buffer
		// Buffer is used to ensure we refresh before waiting until the last minute because of potential time differences between
		// server and device
		public bool IsExpired
		{
			get
			{
				return (DateTime.UtcNow.ToUnixTimeStamp() - Constants.ExpirationBuffer) > (CreatedAt + ExpiresIn);
			}
		}

		public static OAuth ToObject(Dictionary<string, string> userInfo)
		{
			var result = JsonConvert.SerializeObject(userInfo);
			JToken jsonData = JObject.Parse(result);
			return jsonData.ToObject<OAuth>();
		}
	}
}
