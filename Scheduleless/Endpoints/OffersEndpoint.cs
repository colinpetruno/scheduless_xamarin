using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
    public class OffersEndpoint
    {
        private const string _baseRelativeUrl = "/";

        public OffersEndpoint()
        {
        }

        public async Task<ApiResponse<IEnumerable<Offer>>> IndexAsync<Offer>(Trade trade)
        {
            using (var client = new AuthenticatedApiRequest())
            {
                return await client.GetAsync<IEnumerable<Offer>>(
                    $"/mobile_api/trades/{trade.Id}/offers",
                    responseMapperKey: "offers"
                );
            }
        }
    }
}
