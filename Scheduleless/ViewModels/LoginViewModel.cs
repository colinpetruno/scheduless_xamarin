using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Localization;
using Scheduleless.Models;
using Scheduleless.Services;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		// FIXME: change back
		private string _email = "demo@example.com";
		//private string _email = string.Empty;

		public string Email
		{
			get { return _email; }
			set { SetProperty(ref _email, value); }
		}

		// FIXME: change back
		private string _password = "password";
		//private string _password = string.Empty;
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

			DialogService.ShowLoading(TranslationService.Localize(LocalizationConstants.Keys.LoginSigningInMessage));
			var response = await AuthenticationService.Instance.AuthenticateAsync<OAuthTokenResponse>(Email, Password);
			DialogService.HideLoading();

			if (response.IsSuccess)
			{
				await NavigationService.Instance.DisplayShiftsPageAsync();
			}
			else
			{
				Debug.WriteLine($"Login failed: {response.Exception}");
				DialogService.ShowLoading(TranslationService.Localize(LocalizationConstants.Keys.LoginFailedMessage));
			}

			IsBusy = false;
		}
	}
}
