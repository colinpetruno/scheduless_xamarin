using System;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace Scheduleless.Services
{
	public class DialogService
	{
		public static void ShowLoading()
		{
			UserDialogs.Instance.ShowLoading();
		}

		public static void ShowLoading(string loadingMessage)
		{
			UserDialogs.Instance.ShowLoading(loadingMessage);
		}

		public static void HideLoading()
		{
			UserDialogs.Instance.HideLoading();
		}

		public static void ShowError(string errorMessage, int timeOut = 2000)
		{
			UserDialogs.Instance.ShowError(errorMessage, timeOut);
		}

		public static void ShowSuccess(string successMessage)
		{
			UserDialogs.Instance.ShowSuccess(successMessage);
		}

		public static void ShowSuccess(string successMessage, int timeOut)
		{
			UserDialogs.Instance.ShowSuccess(successMessage, timeOut);
		}
	}
}

