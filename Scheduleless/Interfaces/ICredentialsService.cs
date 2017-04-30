using System;
using Scheduleless.Models;

namespace Scheduleless.Interfaces
{
	public interface ICredentialsService
	{
		CredentialInfo Credentials { get; }
		OAuth OAuthData { get; }
		bool IsAuthenticated { get; }
		void SetCredentials(string email, string password, OAuth oAuthData);
		void UpdateCredentials(OAuth oAuthData);
		void DeleteCredentials();
	}
}
