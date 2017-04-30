using System;

namespace Scheduleless.Models
{
	public class ApiResponse<T>
	{
		public ApiResponse()
		{
			IsSuccess = false;
		}

		public bool IsSuccess { get; set; }
		public T Result { get; set; }
		public Exception Exception { get; set; }
	}
}
