using System;

namespace Scheduleless.Interfaces
{
	public interface IPushNotificationService
	{
		string Token { get; }
		void HandleRegister(Action<bool, string> requestAuthPermissionCallback);
		void OnTokenRefresh(object deviceToken, Action<string> completionHandler);
		void Connect();
		void Disconnect();
	}
}
