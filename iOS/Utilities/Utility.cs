using System;
namespace Scheduleless.iOS.Utilities
{
	public class Utility
	{
		public static bool IsSimulator()
		{
			return ObjCRuntime.Runtime.Arch == ObjCRuntime.Arch.SIMULATOR;
		}
	}
}
