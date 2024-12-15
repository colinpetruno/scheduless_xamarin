using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Interfaces;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
	public class TradesEndpoint
	{
		private const string _baseRelativeUrl = "/trades";

		public async Task<ApiResponse<Trade>> CreateAsync<Trade>(string note, FutureShift shift)
		{
			var parameters = new Dictionary<string, object>
			{
				{"trade", new Dictionary<string, object>
					{
						{"note", note},
						{"accept_offers", true}
					}
				}
			};

			using (var client = new AuthenticatedApiRequest())
			{
				var result = await client.PostAsync<Trade>(
					$"/mobile_api/shifts/{shift.Id}/trades",
					parameters: parameters,
					responseMapperKey: "trade",
					forceLogoutOnUnauthorized: false
				);

				// TODO: double check with Colin; what needs to be refreshed? (e.g., index)
				client.DeleteCacheKeyIfPresent("GET", $"/mobile_api/shifts/{shift.Id}/trades");

				return result;
			}
		}
	}
}
