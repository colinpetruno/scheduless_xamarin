using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Interfaces;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
  public class TimeOffRequestEndpoint
  {
    private const string _baseRelativeUrl = "/mobile_api/time_off_requests";

    public async Task<ApiResponse<IEnumerable<TimeOffRequest>>> IndexAsync<TimeOffRequest>()
    {
      using (var client = new AuthenticatedApiRequest())
      {
        return await client.GetAsync<IEnumerable<TimeOffRequest>>(_baseRelativeUrl, responseMapperKey: "time_off_requests");
      }
    }

    public async Task<ApiResponse<TimeOffRequest>> CreateAsync<TimeOffRequest>(string note, FutureShift shift)
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
        return await client.PostAsync<TimeOffRequest>(
            $"/mobile_api/shifts/{shift.Id}/trades",
            parameters: parameters,
            responseMapperKey: "trade",
            forceLogoutOnUnauthorized: false
        );
      }
    }
  }
}
