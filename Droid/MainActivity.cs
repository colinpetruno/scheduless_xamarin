﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;
using Scheduleless.Droid.Utilities;
using Acr.UserDialogs;


namespace Scheduleless.Droid
{
	[Activity(Label = "Scheduleless.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		private const string HockeyAppId = "938821bc74244436a8380ee15befc913";

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			UserDialogs.Init(this);

			base.OnCreate(bundle);

			// only record crashes if it's on a device
			if (!Utility.IsEmulator())
			{
				CrashManager.Register(this, HockeyAppId, new AppCrashManagerListener());
			}

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());

			// IMPORTANT: Initialize XFGloss AFTER calling LoadApplication on the Android platform
			XFGloss.Droid.Library.Init(this, bundle);
		}
	}
}
