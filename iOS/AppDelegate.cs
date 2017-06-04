using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using HockeyApp.iOS;
using Scheduleless.iOS.Fonts;
using Scheduleless.iOS.Utilities;
using Scheduleless.Services;
using UIKit;
using UserNotifications;

namespace Scheduleless.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate
	{
		static AppDelegate _instance = null;
		public static AppDelegate Instance
		{
			get
			{
				return _instance;
			}
		}

		private const string HockeyAppId = "6425d2408f6b4cadb1d013fa9e6b8134";

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			_instance = this;

			// Load up custom fonts
			Plugin.Iconize.Iconize
				  //.With(new IconicModule())
				  .With(new FontAwesomeModule()
			);

			// only record crashes if on a device
			if (!Utility.IsSimulator())
			{
				var manager = BITHockeyManager.SharedHockeyManager;
				manager.Configure(HockeyAppId);
				manager.CrashManager.CrashManagerStatus = BITCrashManagerStatus.AutoSend;
				manager.StartManager();
				manager.Authenticator.AuthenticateInstallation(); // This line is obsolete in crash only builds
			}

			global::Xamarin.Forms.Forms.Init();

			// NOTE: order matters here
			FormsPlugin.Iconize.iOS.IconControls.Init();

			LoadApplication(new App());

			/********** ADD THIS CALL TO INITIALIZE XFGloss *********/
			XFGloss.iOS.Library.Init();

			return base.FinishedLaunching(app, options);
		}

		public override void DidEnterBackground(UIApplication uiApplication)
		{
			PushNotificationService.Instance.Disconnect();
		}

		public override void OnActivated(UIApplication uiApplication)
		{
			PushNotificationService.Instance.Connect();
			base.OnActivated(uiApplication);
		}

		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			System.Diagnostics.Debug.WriteLine("RegisteredForRemoteNotifications");
			PushNotificationService.Instance.OnTokenRefresh(deviceToken);
		}
	}
}
