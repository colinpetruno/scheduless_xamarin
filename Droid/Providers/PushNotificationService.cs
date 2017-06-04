using System;
using Android.Gms.Common;
using Firebase.Iid;
using Scheduleless.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(Scheduleless.Droid.Providers.PushNotificationService))]
namespace Scheduleless.Droid.Providers
{
	public class PushNotificationService : IPushNotificationService
	{
		public PushNotificationService() { }

		public bool IsPlayServicesAvailable()
		{
			int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(MainActivity.CurrentActivity);
			if (resultCode != ConnectionResult.Success)
			{
				if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
					System.Diagnostics.Debug.WriteLine(GoogleApiAvailability.Instance.GetErrorString(resultCode));
				else
				{
					System.Diagnostics.Debug.WriteLine("This device is not supported");
				}
				return false;
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Google Play Services is available.");
				return true;
			}
		}

		#region - IPushNotificationService

		public string Token
		{
			get
			{
				return FirebaseInstanceId.Instance.Token;
			}
		}

		public void Connect()
		{
			// no-op
		}

		public void Disconnect()
		{
			// no-op
		}

		public void HandleRegister(Action<bool, string> requestAuthPermissionCallback)
		{
			if (requestAuthPermissionCallback != null)
			{
				requestAuthPermissionCallback(IsPlayServicesAvailable(), Token);
			}
		}

		public void OnTokenRefresh(object deviceToken, Action<string> completionHandler)
		{
			if (completionHandler != null && deviceToken != null)
			{
				completionHandler(deviceToken.ToString());
			}
		}

		#endregion
	}
}
