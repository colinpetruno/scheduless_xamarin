using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
  // TODO: RENAME to ShiftsEndpoint
  public class FeaturesEndpoint
  {
    private const string _baseRelativeUrl = "/mobile_api/features";

    public async Task<ApiResponse<Features>> FeaturesAsync<Features>(RequestCachePolicy cachePolicy = RequestCachePolicy.RefreshIfNeeded)
    {
      using (var client = new AuthenticatedApiRequest())
      {
        Debug.WriteLine("In Features Async Endpoint");
        return await client.GetAsync<Features>(_baseRelativeUrl, responseMapperKey: "features", cachePolicy: cachePolicy);
      }
    }
  }
}
