using System;
using System.Threading.Tasks;
using Scheduleless.Models;

namespace Scheduleless.Interfaces
{
	public interface IOAuthTokenEndpoint
	{
		Task<ApiResponse<T>> CreateAsync<T>(string email, string password);
		Task<ApiResponse<T>> RefreshAsync<T>(string refreshToken);
		Task<ApiResponse<object>> Revoke();
	}
}
