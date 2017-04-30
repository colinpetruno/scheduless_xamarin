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
using Scheduleless.Helpers;
using Scheduleless.Interfaces;
using Scheduleless.Services;

namespace Scheduleless.Models
{
	public class ApiRequest : HttpClient, IApiRequest
	{
		private string _relativeUrl;
		private Dictionary<string, object> _parameters;
		protected bool _useAuthentication = false;

		private string FullUrl
		{
			get
			{
				return $"https://gentle-brushlands-30942.herokuapp.com{_relativeUrl}";
			}
		}

		public async Task<ApiResponse<T>> GetAsync<T>(
			string relativeUrl, Dictionary<string, object> parameters = null, string responseMapperKey = null)
		{
			SetInitialValues(relativeUrl, parameters);
			return await MakeRequestAsync<T>(HttpMethod.Get, responseMapperKey);

		}

		public async Task<ApiResponse<T>> PostAsync<T>(
			string relativeUrl, Dictionary<string, object> parameters = null, string responseMapperKey = null, bool forceLogoutOnUnauthorized = true)
		{
			SetInitialValues(relativeUrl, parameters);
			return await MakeRequestAsync<T>(HttpMethod.Post, responseMapperKey, forceLogoutOnUnauthorized);
		}

		public async Task<ApiResponse<T>> DeleteAsync<T>(
			string relativeUrl, Dictionary<string, object> parameters = null, string responseMapperKey = null)
		{
			SetInitialValues(relativeUrl, parameters);
			return await MakeRequestAsync<T>(HttpMethod.Delete, responseMapperKey);
		}

		private void SetInitialValues(string relativeUrl, Dictionary<string, object> parameters = null)
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
					await HandleAuthenticationAsync();
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
					await AuthenticationService.Instance.LogoutAsync(true);
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
				// default: GET
				return await GetAsync($"{FullUrl}{RequestBuilder.BuildQueryString(_parameters)}");
			}
		}

		private async Task HandleAuthenticationAsync()
		{
			await Task.Run(() =>
			{
				Task continuation = AuthenticationService.Instance.HandleAuthenticationAsync()
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
	}

	public class AuthenticatedApiRequest : ApiRequest, IAuthenticatedApiRequest
	{
		public AuthenticatedApiRequest()
			: base()
		{
			_useAuthentication = true;
		}
	}
}
