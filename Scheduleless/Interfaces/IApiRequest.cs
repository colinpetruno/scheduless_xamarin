using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Models;

namespace Scheduleless.Interfaces
{
	public interface IApiRequest : IDisposable
	{
		/// <summary>
		/// Sends a RESTful GET request to the url provided.
		/// </summary>
		/// <returns>An <see cref="ApiResponse{T}"/> where T is the type of <see cref="ApiResponse{T}.Result"/> to be returned.</returns>
		/// <param name="relativeUrl">The relative URL to make a request to.</param>
		/// <param name="parameters">Any parameters to send along with the request.</param>
		/// <param name="responseMapperKey">The key used to deserialze the response, if necessary.</param>
		/// <typeparam name="T">The type of object returned from the request.</typeparam>
		Task<ApiResponse<T>> GetAsync<T>(string relativeUrl, Dictionary<string, object> parameters = null, string responseMapperKey = null, RequestCachePolicy cachePolicy = RequestCachePolicy.Ignore);

		/// <summary>
		/// Sends a RESTful POST request to the url provided.
		/// </summary>
		/// <returns>An <see cref="ApiResponse{T}"/> where T is the type of <see cref="ApiResponse{T}.Result"/> to be returned.</returns>
		/// <param name="relativeUrl">The relative URL to make a request to.</param>
		/// <param name="parameters">Any parameters to send along with the request.</param>
		/// <param name="responseMapperKey">The key used to deserialze the response, if necessary.</param>
		/// <param name="forceLogoutOnUnauthorized">Whether or not to log the user out if a 401 response is recieved.</param>
		/// <typeparam name="T">The type of object returned from the request.</typeparam>
		Task<ApiResponse<T>> PostAsync<T>(string relativeUrl, Dictionary<string, object> parameters = null, string responseMapperKey = null, bool forceLogoutOnUnauthorized = true, RequestCachePolicy cachePolicy = RequestCachePolicy.Ignore);

		/// <summary>
		/// Sends a RESTful DELETE request to the url provided.
		/// </summary>
		/// <returns>An <see cref="ApiResponse{T}"/> where T is the type of <see cref="ApiResponse{T}.Result"/> to be returned.</returns>
		/// <param name="relativeUrl">The relative URL to make a request to.</param>
		/// <param name="parameters">Any parameters to send along with the request.</param>
		/// <param name="responseMapperKey">The key used to deserialze the response, if necessary.</param>
		/// <typeparam name="T">The type of object returned from the request.</typeparam>
		Task<ApiResponse<T>> DeleteAsync<T>(string relativeUrl, Dictionary<string, object> parameters = null, string responseMapperKey = null, RequestCachePolicy cachePolicy = RequestCachePolicy.Ignore);
	}

	public interface IAuthenticatedApiRequest : IApiRequest { }
}
