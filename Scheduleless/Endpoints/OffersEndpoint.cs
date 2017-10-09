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

		public async Task<ApiResponse<Offer>> AcceptAsync<Offer>(int offerId)
		{
			using (var client = new AuthenticatedApiRequest())
			{
				var result = await client.PostAsync<Offer>(
					$"/mobile_api/offers/{offerId}/accept",
					responseMapperKey: "offer"
				);

				// FIXME: ask colin if this needs to be cache blown and how to get the trade ID
				//client.DeleteCacheKeyIfPresent("GET", $"/mobile_api/trades/{trade.Id}/offers"

				return result;
			}
		}

		// FIXME: WHY WON'T IT LET ME PASS THE REFERENCE? <Offer>(Offer offer)... 
		public async Task<ApiResponse<Offer>> DeclineAsync<Offer>(int offerId)
		{
			using (var client = new AuthenticatedApiRequest())
			{
				var result = await client.PostAsync<Offer>(
					$"/mobile_api/offers/{offerId}/decline",
					responseMapperKey: "offer"
				);

				// FIXME: ask colin if this needs to be cache blown and how to get the trade ID
				//client.DeleteCacheKeyIfPresent("GET", $"/mobile_api/trades/{trade.Id}/offers"

				return result;
			}
		}

		public async Task<ApiResponse<Offer>> CreateAsync<Offer>(string note, int offeredShiftId, int tradeId)
		{
			var parameters = new Dictionary<string, object>
			{
				{"offer", new Dictionary<string, object>
					{
						{"note", note},
						{"offered_shift_id", offeredShiftId}
					}
				}
			};

			using (var client = new AuthenticatedApiRequest())
			{
				var result = await client.PostAsync<Offer>(
					$"/mobile_api/trades/{tradeId}/offers",
					parameters: parameters,
					responseMapperKey: "offer"
				);

				client.DeleteCacheKeyIfPresent("GET", $"/mobile_api/trades/{tradeId}/offers");

				return result;
			}
		}
	}
}
