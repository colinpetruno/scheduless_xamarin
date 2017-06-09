using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
	public class AvailableShiftsEndpoint
	{
		private const string _baseRelativeUrl = "/mobile_api/trades";

		public AvailableShiftsEndpoint()
		{
		}

		public async Task<ApiResponse<IEnumerable<AvailableShift>>> IndexAsync<AvailableShift>()
		{
			using (var client = new AuthenticatedApiRequest())
			{
				return await client.GetAsync<IEnumerable<AvailableShift>>(_baseRelativeUrl, responseMapperKey: "trades");
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
