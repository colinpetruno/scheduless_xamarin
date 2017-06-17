﻿using System;
using System.Threading.Tasks;
using MessageBar;
using Scheduleless.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(Scheduleless.iOS.Providers.ToastNotifier_iOS))]
namespace Scheduleless.iOS.Providers
{
	public class ToastNotifier_iOS : IToastNotifier
	{
		public Task<bool> Notify(ToastNotificationType type, string title, string description, TimeSpan duration, object context = null)
		{
			MessageType msgType = MessageType.Info;

			switch (type)
			{
				case ToastNotificationType.Error:
				case ToastNotificationType.Warning:
					msgType = MessageType.Error;
					break;

				case ToastNotificationType.Success:
					msgType = MessageType.Success;
					break;
			}

			var taskCompletionSource = new TaskCompletionSource<bool>();
			MessageBarManager.SharedInstance.ShowMessage(title, description, msgType, b => taskCompletionSource.TrySetResult(b));
			return taskCompletionSource.Task;
		}

		public void HideAll()
		{
			MessageBarManager.SharedInstance.HideAll();
		}
	}
}
