using System;
using Newtonsoft.Json;
using Scheduleless.Interfaces;

namespace Scheduleless.Models
{
	public class OAuthTokenResponse : IOAuthTokenResponse
	{
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

		// SL NOTE: currently, the response just returns OAuth data, but if you want to send OAuth and User data, you can do it here
		public OAuth OAuth
		{
			get
			{
				// SL NOTE: i normalized the data because  expects OAuthTokenResponse to return an OAuth key and a User key
				return new OAuth { AccessToken = AccessToken, TokenType = TokenType, RefreshToken = RefreshToken, CreatedAt = CreatedAt };
			}
		}
		//public User User { get; set; }
	}
}
