using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Models;

namespace Scheduleless.Endpoints
{
    public class PushNotificationsEndpoint
    {
        // FIXME: change this to whatever the endpoint is
        private const string _baseRelativeUrl = "/mobile_api/firebase_tokens";

        public async Task<ApiResponse<bool>> UpdateAsync(string deviceToken)
        {
            var parameters = new Dictionary<string, object>
            {
                {"firebase_token", new Dictionary<string, object>
                    {
                        {"token", deviceToken}
                    }
                }
            };

            // FIXME: This is getting called twice. Once without the token
            // before you push the accept and once after the accept push
            // notification is pushed. The first time it has no token so it
            // fails in the api
            using (var client = new AuthenticatedApiRequest())
            {
                return await client.PostAsync<bool>(_baseRelativeUrl, parameters);
            }
        }
    }
}
