using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
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
				return await client.GetAsync<IEnumerable<FutureShift>>(_baseRelativeUrl, responseMapperKey: "future_shifts");
			}
		}

		// FIXME: i couldn't find the call for details so I just put up an example of how to call a Show
		public async Task<ApiResponse<FutureShift>> ShowAsync<FutureShift>(int shiftId)
		{
			using (var client = new AuthenticatedApiRequest())
			{
				return await client.GetAsync<FutureShift>($"{_baseRelativeUrl}/{shiftId}", responseMapperKey: "shift");
			}
		}
	}
}
