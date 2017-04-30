using System;
using Scheduleless.Models;

namespace Scheduleless.Interfaces
{
	public interface IOAuthTokenResponse
	{
		string AccessToken { get; set; }
		string TokenType { get; set; }
		int ExpiresIn { get; set; }
		string RefreshToken { get; set; }
		long CreatedAt { get; set; }
		OAuth OAuth { get; }
		//User User { get; set; }
	}
}
