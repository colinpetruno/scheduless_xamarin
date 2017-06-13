using System;
using System.Collections.Generic;
using System.Diagnostics;
using Scheduleless.Exceptions;
using Scheduleless.Interfaces;
using Scheduleless.Models;
using Xamarin.Forms;

namespace Scheduleless.Services
{
    public struct CredentialsConstants
    {
        public const string ProviderName = "Whatever";
        public const string UserSecretKey = "UserSecret";
        public const string UserNameKey = "UserName";
    }

    public class CredentialsService : ICredentialsService
    {
        private ISecuredDataProvider _secureDataProvider;

        public OAuth OAuthData
        {
            get
            {
                return Credentials?.OAuthData;
            }
        }

        public CredentialsService()
        {
            _secureDataProvider = DependencyService.Get<ISecuredDataProvider>();
        }

        private CredentialInfo _credentials;
        public CredentialInfo Credentials
        {
            get
            {
                if (_credentials == null)
                {
                    FetchCredentials();
                }

                return _credentials;
            }
        }

        public bool IsAuthenticated
        {
            get { return !Credentials?.OAuthData.IsExpired ?? false; }
        }

        public string Token
        {
            get { return Credentials.OAuthData.AccessToken; }
        }

        public void UpdateCredentials(OAuth oAuthData)
        {
            if (oAuthData == null)
            {
                Debug.WriteLine("UpdateCredentials: Error: oAuthData is null");
                throw new MissingOAuthDataException();
            }

            var userInfo = _secureDataProvider.Retrieve(CredentialsConstants.ProviderName);

            if (CanUpdateCredentials(userInfo))
            {
                Debug.WriteLine($"Attempting to update secure credentials with oAuthData:\n" +
                                $"access token: {oAuthData.AccessToken}" +
                                $"refresh token: {oAuthData.RefreshToken}");

                var email = userInfo[CredentialsConstants.UserNameKey];
                var password = userInfo[CredentialsConstants.UserSecretKey];
                StoreCredentials(email, password, oAuthData);
                // update credentials after storing
                _credentials = new CredentialInfo { UserName = email, UserSecret = password, OAuthData = oAuthData };
                Debug.WriteLine("Successfully updated secure credentials");
            }
            else
            {
                var errorMessage = "Missing user info to update secure credentials";
                Debug.WriteLine(errorMessage);
                throw new Exception(errorMessage);
            }
        }

        public void SetCredentials(string email, string password, OAuth oAuthData)
        {
            if (email == null || password == null || oAuthData == null)
            {
                throw new MissingOAuthDataException();
            }

            Debug.WriteLine("Attempting to store user info to secure credentials.\n" +
                            $"email: {email}\n" +
                            $"oAuthData access token: {oAuthData.AccessToken}\n" +
                            $"oAuthData refresh token: {oAuthData.RefreshToken}");
            StoreCredentials(email, password, oAuthData);
            Debug.WriteLine("User info successfully stored to secure credentials");
            _credentials = new CredentialInfo { UserName = email, UserSecret = password, OAuthData = oAuthData };
        }

        public void DeleteCredentials()
        {
            _credentials = null;
            _secureDataProvider.Clear(CredentialsConstants.ProviderName);
        }

        private void StoreCredentials(string email, string password, OAuth oAuthData)
        {
            _secureDataProvider.Store(
                email,
                CredentialsConstants.ProviderName,
                new Dictionary<string, string>
                {
                    { CredentialsConstants.UserNameKey, email },
                    { CredentialsConstants.UserSecretKey, password },
                    { JsonOAuthKeys.AccessToken, oAuthData.AccessToken },
                    { JsonOAuthKeys.TokenType, oAuthData.TokenType },
                    { JsonOAuthKeys.RefreshToken, oAuthData.RefreshToken },
                    { JsonOAuthKeys.ExpiresIn, oAuthData.ExpiresIn.ToString() },
                    { JsonOAuthKeys.CreatedAt, oAuthData.CreatedAt.ToString() },
                }
            );
        }

        private void FetchCredentials()
        {
            var userInfo = _secureDataProvider.Retrieve(CredentialsConstants.ProviderName);
            if (userInfo.Count > 0)
            {
                _credentials = new CredentialInfo
                {
                    UserName = userInfo[CredentialsConstants.UserNameKey],
                    UserSecret = userInfo[CredentialsConstants.UserSecretKey],
                    OAuthData = OAuth.ToObject(userInfo)
                };
            }
            else
            {
                Debug.WriteLine("No user info found.");
            }
        }

        private bool CanUpdateCredentials(Dictionary<string, string> userInfo)
        {
            return userInfo.Count > 0
               && userInfo.ContainsKey(CredentialsConstants.UserNameKey)
               && userInfo.ContainsKey(CredentialsConstants.UserSecretKey);
        }
    }
}
