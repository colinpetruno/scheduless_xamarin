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
    }
}
