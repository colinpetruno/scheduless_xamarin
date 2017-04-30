﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Interfaces;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
	public class OAuthTokenEndpoint
	{
		private Func<ApiRequest> _apiRequestFactory;
		Func<AuthenticatedApiRequest> _authenticatedApiRequestFactory;
		private const string _baseRelativeUrl = "/oauth";
		//private OAuthCredentials _oAuthCredentials;

		public OAuthTokenEndpoint()
		{
			//_apiRequestFactory = new Func<ApiRequest>();
			//_authenticatedApiRequestFactory = new AuthenticatedApiRequest();
			//_oAuthCredentials = new OAuthCredentials();
		}

		public async Task<ApiResponse<T>> CreateAsync<T>(string email, string password) where T : IOAuthTokenResponse
		{

			//"grant_type"    : "password",
			//  "username"      : "demo@example.com",
			//  "password"      : "password"
			var parameters = new Dictionary<string, object>
				   {
					{"grant_type", "password"},
					{"username", "demo@example.com"},
					{"password", "password"},
				   };

			using (var client = new ApiRequest())
			{
				//https://gentle-brushlands-30942.herokuapp.com
				return await client.PostAsync<T>($"https://gentle-brushlands-30942.herokuapp.com/oauth/token", parameters, forceLogoutOnUnauthorized: false);
			}
		}

		public async Task<ApiResponse<T>> RefreshAsync<T>(string refreshToken)
		{
			var parameters = new Dictionary<string, object>
			{
				{"grant_type", "refresh_token"},
				{"refresh_token", refreshToken}
			};

			using (var client = new ApiRequest())
			{
				return await client.PostAsync<T>($"https://gentle-brushlands-30942.herokuapp.com/oauth/token", parameters);
			}
		}

		public async Task<ApiResponse<object>> Revoke()
		{
			using (var client = new ApiRequest())
			{
				return await client.PostAsync<object>($"https://gentle-brushlands-30942.herokuapp.com/oauth/revoke");
			}
		}
	}
}
