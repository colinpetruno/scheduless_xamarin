using System;
using Android.OS;

namespace Scheduleless.Droid.Utilities
{
	public class Utility
	{
		public static bool IsEmulator()
		{
			string fingerPrint = Build.Fingerprint;
			bool isEmulator = fingerPrint != null && (fingerPrint.Contains("vbox") || fingerPrint.Contains("generic"));
			return isEmulator;
		}
	}
}
