﻿using System;
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

        public async Task<ApiResponse<Offer>> AcceptAsync<Offer>(int offer_id)
        {


            using (var client = new AuthenticatedApiRequest())
            {
                return await client.PostAsync<Offer>(
                    $"/mobile_api/offers/{offer_id}/accept",
                    // parameters: parameters,
                    responseMapperKey: "offer"
                );
            }
        }

        // FIXME: WHY WON'T IT LET ME PASS THE REFERENCE? <Offer>(Offer offer)... 
        public async Task<ApiResponse<Offer>> DeclineAsync<Offer>(int offer_id)
        {
            //     var parameters = new Dictionary<string, object> { };

            using (var client = new AuthenticatedApiRequest())
            {
                return await client.PostAsync<Offer>(
                    $"/mobile_api/offers/{offer_id}/decline",
                    //           parameters: parameters,
                    responseMapperKey: "offer"
                );
            }
        }
    }
}
