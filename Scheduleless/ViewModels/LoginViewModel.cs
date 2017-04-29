using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public LoginViewModel()
		{
		}

		Command _login;
		public Command LoginCommand
		{
			get { return _login ?? (_login = new Command(async () => await ExecuteLoginCommandAsync())); }
		}

		private async Task ExecuteLoginCommandAsync()
		{
			if (IsBusy)
			{
				return;
			}

			IsBusy = true;

			// TODO: add comm service

		}
	}
}
