using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
	public class PushNotificationsEndpoint
	{
		// FIXME: change this to whatever the endpoint is
		private const string _baseRelativeUrl = "/mobile_api/push_notifications";

		public async Task<ApiResponse<bool>> UpdateAsync(string deviceToken)
		{
			// FIXME: add whatever else is needed
			var parameters = new Dictionary<string, object>
			{
				{"device_token", "deviceToken"}
			};

			using (var client = new AuthenticatedApiRequest())
			{
				return await client.GetAsync<bool>(_baseRelativeUrl, parameters);
			}
		}
	}
}
