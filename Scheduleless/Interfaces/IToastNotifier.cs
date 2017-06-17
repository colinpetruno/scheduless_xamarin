using System;
using System.Threading.Tasks;

namespace Scheduleless.Interfaces
{
	public enum ToastNotificationType
	{
		Info,
		Success,
		Error,
		Warning,
	}

	public interface IToastNotifier
	{
		Task<bool> Notify(ToastNotificationType type, string title, string description, TimeSpan duration, object context = null);

		void HideAll();
	}
}
