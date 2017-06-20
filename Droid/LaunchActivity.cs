using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Scheduleless.Droid
{
	[Activity(MainLauncher = true, NoHistory = true, Label = "Scheduleless", Icon = "@drawable/icon", Theme = "@style/LaunchTheme",
			 ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
			 ScreenOrientation = ScreenOrientation.Portrait)]
	public class LaunchActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		protected override void OnResume()
		{
			base.OnResume();
			StartActivity(typeof(MainActivity));
		}
	}
}
