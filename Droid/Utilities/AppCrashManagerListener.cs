using HockeyApp.Android;

namespace Scheduleless.Droid.Utilities
{
	public class AppCrashManagerListener : CrashManagerListener
	{
		public override bool ShouldAutoUploadCrashes()
		{
			return true;
		}
	}
}
