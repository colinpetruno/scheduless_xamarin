using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Interfaces;
using Xamarin.Forms;

namespace Scheduleless.Services
{
	public class PushNotificationService
	{
		private static readonly Lazy<PushNotificationService> lazy = new Lazy<PushNotificationService>(() => new PushNotificationService());
		public static PushNotificationService Instance
		{
			get
			{
				return lazy.Value;
			}
		}

		private PushNotificationService() { }

		public async Task UpdateUserPushTokenAsync(string deviceToken)
		{
			// TODO: Make call to server
		}

		public void OnTokenRefresh(object deviceToken)
		{
			Debug.WriteLine("OnTokenRefresh");
			// TODO: Make call to server

			//if (FirebaseService.Instance.CurrentUser == null)
			//{
			//	return;
			//}

			DependencyService.Get<IPushNotificationService>().OnTokenRefresh(
				deviceToken,
				async (registeredToken) =>
				{
					// TODO: Make call to server
					//await FirebaseService.Instance.UpdateUserPushTokenAsync(registeredToken);
				});
		}

		#region - IPushNotificationService

		public string Token
		{
			get
			{
				return DependencyService.Get<IPushNotificationService>().Token;
			}
		}

		public void HandleRegister(Action<bool, string> requestAuthPermissionCallback)
		{
			DependencyService.Get<IPushNotificationService>().HandleRegister(requestAuthPermissionCallback);
		}

		public void Connect()
		{
			DependencyService.Get<IPushNotificationService>().Connect();
		}

		public void Disconnect()
		{
			DependencyService.Get<IPushNotificationService>().Disconnect();
		}

		#endregion
	}
}
