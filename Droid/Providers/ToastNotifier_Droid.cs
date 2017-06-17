using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Android.Widget;
using Scheduleless.Interfaces;

[assembly: Dependency(typeof(Scheduleless.Droid.Providers.ToastNotifier_Droid))]
namespace Scheduleless.Droid.Providers
{
	public class ToastNotifier_Droid : IToastNotifier
	{
		public Task<bool> Notify(ToastNotificationType type, string title, string description, TimeSpan duration, object context = null)
		{
			var taskCompletionSource = new TaskCompletionSource<bool>();
			Toast.MakeText(Forms.Context, description, ToastLength.Short).Show();
			return taskCompletionSource.Task;
		}

		public void HideAll()
		{
		}
	}
}
