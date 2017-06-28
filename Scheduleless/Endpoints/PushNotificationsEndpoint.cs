using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
	public class PushNotificationsEndpoint
	{
		private const string _baseRelativeUrl = "/mobile_api/firebase_tokens";

		public async Task<ApiResponse<bool>> UpdateAsync(string deviceToken)
		{
			var parameters = new Dictionary<string, object>
			{
				{"firebase_token", new Dictionary<string, object>
					{
						{"token", deviceToken}
					}
				}
			};

			using (var client = new AuthenticatedApiRequest())
			{
				return await client.PostAsync<bool>(_baseRelativeUrl, parameters);
			}
		}
	}
}
