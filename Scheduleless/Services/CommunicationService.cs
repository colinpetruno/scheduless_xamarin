using System;
using System.Text;
using System.Threading.Tasks;

namespace Scheduleless.Services
{
	/// <summary>
	/// Handles all the communication between the server and client
	/// </summary>
	public class CommunicationService
	{
		public CommunicationService()
		{
		}

		// TODO: this is what i want
		//https://gentle-brushlands-30942.herokuapp.com/oauth/token

		//{
		//  "grant_type"    : "password",
		//  "username"      : "demo@example.com",
		//  "password"      : "password"
		//}

		//{
		//  "access_token": "0737aaf0ae29161fdbae7e68fc10ac7e47ff7815463277bd83d49946ff57b34c",
		//  "token_type": "bearer",
		//  "expires_in": 7200,
		//  "refresh_token": "6a3ba9c07828a800ce80293a46b3fe1108aedeb818732a14048b8fb91b05bf2f",
		//  "created_at": 1493496252
		//}

		//private async Task<HttpResponseMessage> ActionForMethod(HttpMethod method)
		//{
		//	if (method == HttpMethod.Post)
		//	{
		//		var jsonString = JsonConvert.SerializeObject(_parameters, Formatting.Indented);
		//		return await PostAsync(FullUrl, new StringContent(jsonString, Encoding.UTF8, "application/json"));
		//	}
		//	else if (method == HttpMethod.Put)
		//	{
		//		var jsonString = JsonConvert.SerializeObject(_parameters, Formatting.Indented);
		//		return await PutAsync(FullUrl, new StringContent(jsonString, Encoding.UTF8, "application/json"));
		//	}
		//	else if (method == HttpMethod.Delete)
		//	{
		//		return await DeleteAsync(FullUrl);
		//	}
		//	else
		//	{
		//		// default: GET
		//		return await GetAsync($"{FullUrl}{RequestBuilder.BuildQueryString(_parameters)}");
		//	}
		//}

	}
}
