using System;
using System.Threading.Tasks;
using Scheduleless.Models;
using Scheduleless.Services;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private string _email = string.Empty;
		public string Email
		{
			get { return _email; }
			set { SetProperty(ref _email, value); }
		}

		private string _password = string.Empty;
		public string Password
		{
			get { return _password; }
			set { SetProperty(ref _password, value); }
		}

		Command _loginCommand;
		public Command LoginCommand
		{
			get { return _loginCommand ?? (_loginCommand = new Command(async () => await ExecuteLoginCommandAsync())); }
		}

		private async Task ExecuteLoginCommandAsync()
		{
			if (IsBusy)
			{
				return;
			}

			IsBusy = true;

			var response = await AuthenticationService.Instance.AuthenticateAsync<OAuthTokenResponse>(Email, Password);
			if (response.IsSuccess)
			{
				await NavigationService.Instance.DisplayShiftsPageAsync();
			}

			IsBusy = false;
		}
	}
}
