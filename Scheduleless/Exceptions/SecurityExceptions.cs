using System;
namespace Scheduleless.Exceptions
{
	public class MissingOAuthDataException : Exception
	{
		public MissingOAuthDataException() : base("OAuthData is not present.") { }
	}

	public class UserNotFoundException : Exception
	{
		public UserNotFoundException() : base("The user was not found.") { }
	}
}
