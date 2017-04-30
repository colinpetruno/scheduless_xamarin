using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scheduleless.Extensions;
using Scheduleless.Interfaces;
using Scheduleless.Models;

namespace Scheduleless.Services
{
	/// <summary>
	/// Handles all the communication between the server and client
	/// </summary>
	public class CommunicationService : HttpClient, IApiRequest
	{
		private Dictionary<string, object> _parameters;
		private string _relativeUrl;
		protected bool _useAuthentication = false;

		public CommunicationService()
		{
			//_parameters = parameters ?? new Dictionary<string, object>();
		}

		// TODO: this is what i want
		//https://gentle-brushlands-30942.herokuapp.com/oauth/token

		//{
		//  "grant_type"    : "password",
		//  "username"      : "demo@example.com",
		//  "password"      : "password"
		//}

		//{
		//  "access_token": "0737aaf0ae29161fdbae7e68fc10ac7e47ff7815463277bd83d49946ff57b34c",
		//  "token_type": "bearer",
		//  "expires_in": 7200,
		//  "refresh_token": "6a3ba9c07828a800ce80293a46b3fe1108aedeb818732a14048b8fb91b05bf2f",
		//  "created_at": 1493496252
		//}

		private string FullUrl
		{
			get
			{
				return "https://gentle-brushlands-30942.herokuapp.com";
			}
		}

		private async Task<HttpResponseMessage> ActionForMethod(HttpMethod method)
		{
			if (method == HttpMethod.Post)
			{
				var jsonString = JsonConvert.SerializeObject(_parameters, Formatting.Indented);
				return await PostAsync(FullUrl, new StringContent(jsonString, Encoding.UTF8, "application/json"));
			}
			else if (method == HttpMethod.Put)
			{
				var jsonString = JsonConvert.SerializeObject(_parameters, Formatting.Indented);
				return await PutAsync(FullUrl, new StringContent(jsonString, Encoding.UTF8, "application/json"));
			}
			else if (method == HttpMethod.Delete)
			{
				return await DeleteAsync(FullUrl);
			}
			else
			{
				// FIXME: return fuck you
				return null;
				// default: GET
				//return await GetAsync($"{FullUrl}{RequestBuilder.BuildQueryString(_parameters)}");
			}
		}

		public async Task<ApiResponse<T>> GetAsync<T>(
	string relativeUrl, Dictionary<string, object> parameters, string responseMapperKey)
		{
			SetInitialValues(relativeUrl, parameters);
			return await MakeRequestAsync<T>(HttpMethod.Get, responseMapperKey);
		}

		public async Task<ApiResponse<T>> PostAsync<T>(
			string relativeUrl, Dictionary<string, object> parameters, string responseMapperKey, bool forceLogoutOnUnauthorized)
		{
			SetInitialValues(relativeUrl, parameters);
			return await MakeRequestAsync<T>(HttpMethod.Post, responseMapperKey, forceLogoutOnUnauthorized);
		}

		public async Task<ApiResponse<T>> DeleteAsync<T>(
			string relativeUrl, Dictionary<string, object> parameters, string responseMapperKey)
		{
			SetInitialValues(relativeUrl, parameters);
			return await MakeRequestAsync<T>(HttpMethod.Delete, responseMapperKey);
		}

		private void SetInitialValues(string relativeUrl, Dictionary<string, object> parameters)
		{
			_relativeUrl = relativeUrl;
			_parameters = parameters ?? new Dictionary<string, object>();
		}

		private async Task<ApiResponse<T>> MakeRequestAsync<T>(HttpMethod method, string responseMapperKey, bool forceLogoutOnUnauthorized = true)
		{
			Debug.WriteLine($"MakeRequest: Request\nMethod: {method}\nURL: {FullUrl}\nParameters: {JsonConvert.SerializeObject(_parameters, Formatting.Indented)}");

			var apiResponse = new ApiResponse<T>();
			HttpResponseMessage response;

			try
			{
				if (_useAuthentication)
				{
					await DoSomething();
				}

				response = await ActionForMethod(method);
				response.EnsureSuccessStatusCode();

				var rawResponseString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine($"MakeRequest: Response\nMethod: {method}\nURL: {FullUrl}");

				JToken jsonData = JObject.Parse(rawResponseString);
				// TODO: handle value types
				if (!jsonData.IsNullOrEmpty())
				{
					var jsonString = !string.IsNullOrEmpty(responseMapperKey)
											? jsonData.SelectToken(responseMapperKey).ToString()
											: rawResponseString;
					apiResponse.Result = JsonConvert.DeserializeObject<T>(jsonString);
				}
			}
			catch (HttpRequestException ex)
			{
				Debug.WriteLine($"MakeRequest: An exception occured - Message: {ex}\nDetails: {ex.InnerException}");
				if (forceLogoutOnUnauthorized && ex.Message.StartsWith("401", StringComparison.Ordinal))
				{
					// FIXME: handle logout
					//await _authenticationService.LogoutAsync(true);
				}
				apiResponse.Exception = ex;
				return apiResponse;
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"MakeRequest: An exception occured - Message: {ex}\nDetails: {ex.InnerException}");
				apiResponse.Exception = ex;
				return apiResponse;
			}

			apiResponse.IsSuccess = true;

			return apiResponse;
		}


		// TODO: make a request to the endpoint
		private async Task DoSomething()
		{
			await Task.Run(() =>
			{
				// FIXME: handle auth
				Task continuation = HandleAuthenticationAsync()
					.ContinueWith((antecedent) =>
				{
					if (!antecedent.IsFaulted)
					{
						DefaultRequestHeaders.Authorization =
							new AuthenticationHeaderValue(
								"Bearer",
								antecedent.Result);
					}
					else
					{
						Debug.WriteLine($"HandleAuthentication: The task did not run to completion.");
						throw antecedent.Exception.GetBaseException();
					}
				});

				continuation.Wait();
			});
		}

		public async Task<string> HandleAuthenticationAsync()
		{
			try
			{
				// TODO: just make a request to the endopint

				// Only query credentialsManager once to get data
				// TODO: check cache to see if data is available
				//var oauthData = _credentialsService.OAuthData;

				//if (oauthData == null)
				//{
				//	// FIXME: throw exception
				//	//throw new MissingOAuthDataException();
				//}

				//if (oauthData.IsExpired)
				//{
				//Debug.WriteLine($"OAuth access token is expired." +
				//				$" Attempting to refresh with: {oauthData.RefreshToken}");
				//var response = await _oAuthTokenEndpoint.RefreshAsync<T>(oauthData.RefreshToken);

				//if (response.IsSuccess)
				//{
				//	//_credentialsService.UpdateCredentials(response.Result.OAuth);
				//	return response.Result.OAuth.AccessToken;
				//}
				//else
				//{
				//	await LogoutAsync(true);
				//	return string.Empty;
				//}
				//}
				//else
				//{
				//	// refresh token not needed, continue
				//	return oauthData.AccessToken;
				//}

				var parameters = new Dictionary<string, object>
					{
						{"grant_type", "refresh_token"},
						//{"refresh_token", refreshToken}
					};

				using (var client = new AuthenticatedApiRequest())
				{
					Debug.WriteLine(client.PostAsync("https://gentle-brushlands-30942.herokuapp.com/oauth/token", null));
					//return await client.PostAsync("https://gentle-brushlands-30942.herokuapp.com/oauth/token", null);
					return null;
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
