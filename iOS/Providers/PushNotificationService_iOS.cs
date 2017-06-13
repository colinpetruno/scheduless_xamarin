using System;
using System.Threading.Tasks;
using Firebase.CloudMessaging;
using Foundation;
using Scheduleless.Interfaces;
using Scheduleless.Services;
using UIKit;
using UserNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(Scheduleless.iOS.Providers.PushNotificationService_iOS))]
namespace Scheduleless.iOS.Providers
{
	public class PushNotificationService_iOS : IPushNotificationService
	{
		/// <summary>
		/// Only register for push once per session.
		/// </summary>
		public bool IsConnectedToFirebaseMessenger { get; set; } = false;

		#region - IPushNotificationService

		public string Token
		{
			get
			{
				return Firebase.InstanceID.InstanceId.SharedInstance.Token;
			}
		}

		public void HandleRegister(Action<bool, string> requestAuthPermissionCallback = null)
		{
			// TODO: check if user is null

			// Register your app for remote notifications.
			if (UIDevice.CurrentDevice.CheckSystemVersion (10, 0)) 
			{
				// iOS 10
				var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
				UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
				{
					if (requestAuthPermissionCallback != null)
					{
						requestAuthPermissionCallback(granted, Token);
					}
				});

				// For iOS 10 display notification (sent via APNS)
				UNUserNotificationCenter.Current.Delegate = AppDelegate.Instance;
			} 
			else 
			{
				// iOS 9 or before
				var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
				var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
				UIApplication.SharedApplication.RegisterUserNotificationSettings (settings);
			}

			UIApplication.SharedApplication.RegisterForRemoteNotifications();

			Firebase.InstanceID.InstanceId.Notifications.ObserveTokenRefresh(async (sender, e) =>
			{
				// if you want to send notification per user, use this token
				System.Diagnostics.Debug.WriteLine($"Firebase InstanceID token: {Token}");

				// update user push token
				await Scheduleless.Services.PushNotificationService.Instance.UpdateUserPushTokenAsync(Token);

				ConnectToFCM();
			});
		}

		public void OnTokenRefresh(object deviceToken, Action<string> completionHandler)
		{
			System.Diagnostics.Debug.WriteLine("OnTokenRefresh");
			var deviceTokenData = deviceToken as NSData;

#if DEBUG
			Firebase.InstanceID.InstanceId.SharedInstance.SetApnsToken(deviceTokenData, Firebase.InstanceID.ApnsTokenType.Sandbox);
#endif
#if RELEASE
			Firebase.InstanceID.InstanceId.SharedInstance.SetApnsToken(deviceTokenData, Firebase.InstanceID.ApnsTokenType.Prod);
#endif

			ConnectToFCM();

			System.Diagnostics.Debug.WriteLine($"Firebase InstanceID token: {Token}");
			completionHandler(Token);
		}

		public void Connect()
		{
			ConnectToFCM();
		}

		public void Disconnect()
		{
			Messaging.SharedInstance.Disconnect();
			IsConnectedToFirebaseMessenger = false;
		}

		#endregion

		private void ConnectToFCM()
		{
			if (IsConnectedToFirebaseMessenger)
			{
				return;
			}

			Messaging.SharedInstance.Connect((error) =>
			{
				if (error == null)
				{
					IsConnectedToFirebaseMessenger = true;
					System.Diagnostics.Debug.WriteLine($"Firebase InstanceID token: {Token}");
					//TODO: Change Topic to what is required
					//Messaging.SharedInstance.Subscribe("/topics/all");
				}
				else
				{
					IsConnectedToFirebaseMessenger = false;
					System.Diagnostics.Debug.WriteLine(error);
				}
				System.Diagnostics.Debug.WriteLine(error != null ? "FirebaseMessge error occured" : "FirebaseMessage connect success");
			});
		}
	}
}
