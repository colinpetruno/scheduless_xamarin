using System;
using System.Threading.Tasks;
using Scheduleless.Interfaces;
using Scheduleless.Models;
using Scheduleless.Exceptions;
using Scheduleless.Endpoints;
using System.Diagnostics;
using Scheduleless.Services;

namespace Scheduleless.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private static readonly Lazy<AuthenticationService> lazy = new Lazy<AuthenticationService>(() => new AuthenticationService());
		public static AuthenticationService Instance
		{
			get
			{
				return lazy.Value;
			}
		}

		protected OAuthTokenEndpoint _oAuthTokenEndpoint;
		protected CredentialsService _credentialsService;

		public AuthenticationService()
		{
			_credentialsService = new CredentialsService();
			_oAuthTokenEndpoint = new OAuthTokenEndpoint();
		}

		public async Task<ApiResponse<TL>> AuthenticateAsync<TL>(string email, string password) where TL : IOAuthTokenResponse
		{
			var response = await _oAuthTokenEndpoint.CreateAsync<TL>(email, password);
			if (response.IsSuccess)
			{
				// SL NOTE: the IOAuthTokenResponse is just returning OAuth
				_credentialsService.SetCredentials(email, password, response.Result.OAuth);
			}
			return response;
		}

		public async Task<ApiResponse<TL>> ReauthenticateAsync<TL>() where TL : IOAuthTokenResponse
		{
			var oauthData = _credentialsService.OAuthData;
			if (oauthData == null)
			{
				return new ApiResponse<TL>
				{
					Exception = new MissingOAuthDataException()
				};
			}

			var response = await _oAuthTokenEndpoint.RefreshAsync<TL>(oauthData.RefreshToken);
			if (response.IsSuccess)
			{
				_credentialsService.UpdateCredentials(response.Result.OAuth);
			}
			return response;
		}

		public async Task<ApiResponse<object>> LogoutAsync(bool isAuthenticationError)
		{
			var response = await _oAuthTokenEndpoint.Revoke();

			_credentialsService.DeleteCredentials();

			await PostLogout(isAuthenticationError);

			return response;
		}

		protected async virtual Task PostLogout(bool isAuthenticationError)
		{
		}

		public async Task<string> HandleAuthenticationAsync()
		{
			try
			{
				// Only query credentialsManager once to get data
				var oauthData = _credentialsService.OAuthData;

				if (oauthData == null)
				{
					throw new MissingOAuthDataException();
				}

				if (oauthData.IsExpired)
				{
					Debug.WriteLine($"OAuth access token is expired." +
									$" Attempting to refresh with: {oauthData.RefreshToken}");
					var response = await _oAuthTokenEndpoint.RefreshAsync<OAuthTokenResponse>(oauthData.RefreshToken);

					if (response.IsSuccess)
					{
						_credentialsService.UpdateCredentials(response.Result.OAuth);
						return response.Result.OAuth.AccessToken;
					}
					else
					{
						await LogoutAsync(true);
						return string.Empty;
					}
				}
				else
				{
					// refresh token not needed, continue
					return oauthData.AccessToken;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"HandleAuthentication: {ex}");
				throw ex;
			}
		}
	}
}
