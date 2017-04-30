using System;
using Scheduleless.Models;

namespace Scheduleless.Interfaces
{
	public interface IOAuthTokenResponse
	{
		OAuth OAuth { get; set; }
		User User { get; set; }
	}
}
