using System;
using System.Collections.Generic;

namespace Scheduleless.Helpers
{
	public class RequestBuilder
	{
		public static string BuildQueryString(Dictionary<string, object> requestParameters)
		{
			if (requestParameters.Count == 0)
			{
				return "";
			}

			int counter = 0;
			string queryString = "";
			foreach (KeyValuePair<string, object> requestParameter in requestParameters)
			{
				if (counter == 0)
				{
					queryString += $"?{requestParameter.Key}={requestParameter.Value.ToString()}";
				}
				else
				{
					queryString += $"&{requestParameter.Key}={requestParameter.Value.ToString()}";
				}

				counter++;
			}

			return queryString;
		}
	}
}
