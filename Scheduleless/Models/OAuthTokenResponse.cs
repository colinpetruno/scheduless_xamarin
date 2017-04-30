using System;
using Scheduleless.Interfaces;

namespace Scheduleless.Models
{
	public class OAuthTokenResponse : IOAuthTokenResponse
	{
		public OAuth OAuth { get; set; }
		public User User { get; set; }
	}
}
