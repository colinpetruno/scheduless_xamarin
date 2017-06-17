using System;
using Scheduleless.Interfaces;
using Xamarin.Forms;

namespace Scheduleless.Extensions
{
	public static class String
	{
		#region - Toast Helpers

		public static void ToToast(this string message, ToastNotificationType type = ToastNotificationType.Info, string title = null)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				var toaster = DependencyService.Get<IToastNotifier>();
				toaster?.Notify(type, title ?? type.ToString().ToUpper(), message, TimeSpan.FromSeconds(2.5f));
			});
		}

		public static void ToastSuccess(this string input)
		{
			Device.BeginInvokeOnMainThread(() => { input.ToToast(ToastNotificationType.Success); });
		}

		public static void ToastInfo(this string input)
		{
			Device.BeginInvokeOnMainThread(() => { input.ToToast(ToastNotificationType.Info); });
		}

		public static void ToastWarning(this string input)
		{
			Device.BeginInvokeOnMainThread(() => { input.ToToast(ToastNotificationType.Success); });
		}

		public static void ToastError(this string input)
		{
			Device.BeginInvokeOnMainThread(() => { input.ToToast(ToastNotificationType.Error); });
		}

		#endregion
	}
}
