using System;
namespace Scheduleless.Models
{
	public class CredentialInfo
	{
		public string UserName { get; set; }
		public string UserSecret { get; set; }
		public OAuth OAuthData { get; set; }
	}
}
