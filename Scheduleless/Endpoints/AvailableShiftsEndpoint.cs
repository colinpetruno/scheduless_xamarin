﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Models;
using Scheduleless.Services;

namespace Scheduleless.Endpoints
{
  public class AvailableShiftsEndpoint
  {
    private const string _baseRelativeUrl = "/mobile_api/trades";

    public AvailableShiftsEndpoint()
    {
    }

    public async Task<ApiResponse<IEnumerable<AvailableShift>>> IndexAsync<AvailableShift>(
      RequestCachePolicy cachePolicy = RequestCachePolicy.RefreshIfNeeded)
    {
      using (var client = new AuthenticatedApiRequest())
      {
        // DialogService.ShowLoading(string.Empty);
        return await client.GetAsync<IEnumerable<AvailableShift>>(_baseRelativeUrl, responseMapperKey: "trades", cachePolicy: cachePolicy);
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
