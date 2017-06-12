using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
    // TODO: RENAME to ShiftsEndpoint
    public class FeaturedShiftEndpoint
    {
        private const string _baseRelativeUrl = "/mobile_api/featured_shift";

        public async Task<ApiResponse<FutureShift>> FeaturedAsync<FutureShift>()
        {
            using (var client = new AuthenticatedApiRequest())
            {
                Debug.WriteLine("In Featured Async Endpoint");
                return await client.GetAsync<FutureShift>(_baseRelativeUrl, responseMapperKey: "featured_shift");
            }
        }

        public async Task<ApiResponse<FutureShift>> CheckInAsync<FutureShift>(int shift_id)
        {
            using (var client = new AuthenticatedApiRequest())
            {
                return await client.PostAsync<FutureShift>(
                    $"/mobile_api/shifts/{shift_id}/check_in",
                    responseMapperKey: "featured_shift"
                );
            }
        }

        public async Task<ApiResponse<FutureShift>> CheckOutAsync<FutureShift>(int shift_id)
        {
            using (var client = new AuthenticatedApiRequest())
            {
                return await client.PostAsync<FutureShift>(
                    $"/mobile_api/shifts/{shift_id}/check_out",
                    responseMapperKey: "featured_shift"
                );
            }
        }
    }
}
