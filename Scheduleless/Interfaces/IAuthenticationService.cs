using System.Threading.Tasks;
using Scheduleless.Models;

namespace Scheduleless.Interfaces
{
	public interface IAuthenticationService
	{
		Task<ApiResponse<T>> AuthenticateAsync<T>(string email, string password) where T : IOAuthTokenResponse;
		Task<ApiResponse<T>> ReauthenticateAsync<T>() where T : IOAuthTokenResponse;
		Task<string> HandleAuthenticationAsync();
		Task<ApiResponse<object>> LogoutAsync(bool isAuthenticationError = false);
	}
}
