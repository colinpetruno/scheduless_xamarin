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
            //  "grant_type"    : "password",
            //  "username"      : "demo@example.com",
            //  "password"      : "password"
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
                return await client.PostAsync<Trade>(
                    $"/mobile_api/shifts/{shift.Id}/trades",
                    parameters: parameters,
                    responseMapperKey: "trade",
                    forceLogoutOnUnauthorized: false
                );
            }
        }
    }
}
