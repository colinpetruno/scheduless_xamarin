using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
	public class MyTradesEndpoint
	{
		private const string _baseRelativeUrl = "/mobile_api/my_trades";

		public MyTradesEndpoint()
		{
		}

		public async Task<ApiResponse<IEnumerable<Trade>>> IndexAsync<Trade>(
			RequestCachePolicy cachePolicy = RequestCachePolicy.RefreshIfNeeded)
		{
			using (var client = new AuthenticatedApiRequest())
			{
				return await client.GetAsync<IEnumerable<Trade>>(
					_baseRelativeUrl, responseMapperKey: "my_trades", cachePolicy: cachePolicy);
			}
		}
	}
}
