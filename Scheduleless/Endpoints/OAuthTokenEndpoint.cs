using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Interfaces;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
    public class OAuthTokenEndpoint
    {
        private const string _baseRelativeUrl = "/oauth";

        public async Task<ApiResponse<OAuth>> CreateAsync<OAuth>(string email, string password)
        {
            //  "grant_type"    : "password",
            //  "username"      : "demo@example.com",
            //  "password"      : "password"
            var parameters = new Dictionary<string, object>
            {
                {"grant_type", "password"},
                {"username", email},
                {"password", password},
            };

            using (var client = new ApiRequest())
            {
                return await client.PostAsync<OAuth>($"{_baseRelativeUrl}/token", parameters, forceLogoutOnUnauthorized: false);
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
                return await client.PostAsync<T>($"{_baseRelativeUrl}/token", parameters);
            }
        }

        public async Task<ApiResponse<object>> Revoke(string token)
        {
            var parameters = new Dictionary<string, object>
            {
                {"token", token}
            };

            using (var client = new AuthenticatedApiRequest())
            {
                return await client.PostAsync<object>($"{_baseRelativeUrl}/revoke", parameters);
            }
        }
    }
}
