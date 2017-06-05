using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Endpoints;
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

		private PushNotificationsEndpoint _endpoint;

		private PushNotificationService()
		{
			_endpoint = new PushNotificationsEndpoint();
		}

		public async Task UpdateUserPushTokenAsync(string deviceToken)
		{
			await _endpoint.UpdateAsync(deviceToken);
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
					await _endpoint.UpdateAsync(registeredToken);
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
