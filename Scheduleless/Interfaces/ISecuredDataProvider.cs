using System;
using System.Collections.Generic;

namespace Scheduleless.Interfaces
{
	public interface ISecuredDataProvider
	{
		void Store(string userId, string providerName, IDictionary<string, string> data);

		void Clear(string providerName);

		Dictionary<string, string> Retrieve(string providerName);
	}
}
