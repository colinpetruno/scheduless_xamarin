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

    public async Task<ApiResponse<TimeOffRequest>> CreateAsync<TimeOffRequest>(DateTime endDate, TimeSpan endTime, DateTime startDate, TimeSpan startTime)
    {
      var parameters = new Dictionary<string, object>
            {
                {"time_off_request", new Dictionary<string, object>
                    {
                        {"end_date", $"{endDate.Year}{endDate.Month.ToString().PadLeft(2, '0')}{endDate.Day.ToString().PadLeft(2, '0')}"},
                        {"end_minutes", endTime.TotalMinutes.ToString()},
                        {"start_date", $"{startDate.Year}{startDate.Month.ToString().PadLeft(2, '0')}{startDate.Day.ToString().PadLeft(2, '0')}"},
                        {"start_minutes", startTime.TotalMinutes.ToString()}
                    }
                }
            };

      using (var client = new AuthenticatedApiRequest())
      {
        return await client.PostAsync<TimeOffRequest>(
            $"/mobile_api/time_off_requests",
            parameters: parameters,
            responseMapperKey: "time_off_request",
            forceLogoutOnUnauthorized: false
        );
      }
    }
  }
}
