using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
	// TODO: RENAME to ShiftsEndpoint
	public class FutureShiftsEndpoint
	{
		private const string _baseRelativeUrl = "/mobile_api/future_shifts";

		public FutureShiftsEndpoint()
		{
		}

		public async Task<ApiResponse<IEnumerable<FutureShift>>> IndexAsync<FutureShift>()
		{
			using (var client = new AuthenticatedApiRequest())
			{
				Debug.WriteLine("In Index Async Endpoint");
				return await client.GetAsync<IEnumerable<FutureShift>>(
					_baseRelativeUrl,
					responseMapperKey: "future_shifts",
					cachePolicy: RequestCachePolicy.RefreshIfNeeded
				);
			}
		}

		public async Task<ApiResponse<FutureShift>> CancelAsync<FutureShift>(string note, int shift_id)
		{
			var parameters = new Dictionary<string, object>
			{
				{"cancellation", new Dictionary<string, object>
					{
						{"note", note}
					}
				}
			};

			using (var client = new AuthenticatedApiRequest())
			{
				return await client.PostAsync<FutureShift>(
					$"/mobile_api/shifts/{shift_id}/cancel",
					parameters: parameters,
					responseMapperKey: "cancellation",
					forceLogoutOnUnauthorized: false
				);
			}
		}
	}
}
