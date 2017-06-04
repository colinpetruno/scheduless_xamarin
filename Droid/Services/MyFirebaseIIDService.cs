using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using Scheduleless.Services;

namespace Scheduleless.Droid.Services
{
	/// <summary>
	/// This service implements an OnTokenRefresh method that is invoked when the registration token is 
	/// initially created or changed. When OnTokenRefresh runs, it retrieves the latest token from the 
	/// FirebaseInstanceId.Instance.Token property (which is updated asynchronously by FCM).
	/// </summary>
	[Service]
	[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
	public class MyFirebaseIIDService : FirebaseInstanceIdService
	{
		/// <summary>
		/// According to Google's Instance ID documentation, the FCM Instance ID service will request that the app 
		/// refresh its token periodically (typically, every 6 months).
		/// </summary>
		public override void OnTokenRefresh()
		{
			var refreshedToken = FirebaseInstanceId.Instance.Token;
			System.Diagnostics.Debug.WriteLine("Refreshed token: " + refreshedToken);
			PushNotificationService.Instance.OnTokenRefresh(refreshedToken);
		}
	}
}
