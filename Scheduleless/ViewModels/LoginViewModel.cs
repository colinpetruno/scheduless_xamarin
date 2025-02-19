﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Extensions;
using Scheduleless.Localization;
using Scheduleless.Models;
using Scheduleless.Services;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		// INFO: uncomment for debug
		//private string _email = "seedling.14@example.com";
		private string _email = string.Empty;
		public string Email
		{
			get { return _email; }
			set { SetProperty(ref _email, value); }
		}

		// INFO: for debug
		//private string _password = "password";
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

			if (!IsFieldValid(Email))
			{
				"Please enter an Email".ToastError();
				return;
			}

			if (!IsFieldValid(Password))
			{
				"Please enter your password".ToastError();
				return;
			}

			IsBusy = true;

			DialogService.ShowLoading(TranslationService.Localize(LocalizationConstants.SigningIn));
			var response = await AuthenticationService.Instance.AuthenticateAsync<OAuthTokenResponse>(Email, Password);
			DialogService.HideLoading();

			if (response.IsSuccess)
			{
				await NavigationService.Instance.DisplayShiftsPageAsync(isFromLoginScreen: true);
			}
			else
			{
				Debug.WriteLine($"Login failed: {response.Exception}");
				DialogService.HideLoading();
				"That username / password is not valid".ToastError();
			}

			IsBusy = false;
		}
	}
}
