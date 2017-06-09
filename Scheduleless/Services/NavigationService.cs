using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Renderers;
using Scheduleless.Views;
using Xamarin.Forms;

namespace Scheduleless.Services
{
	public class NavigationService
	{
		private static readonly Lazy<NavigationService> lazy = new Lazy<NavigationService>(() => new NavigationService());
		private CredentialsService _credentialsService;

		private NavigationService()
		{
			_credentialsService = new CredentialsService();
		}

		public static NavigationService Instance
		{
			get
			{
				return lazy.Value;
			}
		}

		public INavigation Navigation { get; private set; }

		public Page GetInitialScreen()
		{
			Debug.WriteLine($"GetInitialScreen");

            if (_credentialsService.IsAuthenticated)
			{
				Debug.WriteLine($"CredentialsSerivce Is Authenticated");
				var tabbedPage = GetInitialTabbedPages();
				Navigation = tabbedPage.Navigation;
				return tabbedPage;
			}
			else
			{
				Debug.WriteLine($"CredentialsSerivce Is Not Authenticated");
				var page = new LoginPage();
				var navPage = new ThemedNavigationPage(page);
				Navigation = navPage.Navigation;
				return navPage;
			}
		}

		public async Task DisplayShiftsPageAsync(bool isFromLoginScreen)
		{
			if (isFromLoginScreen)
			{
				var tabbedPage = GetInitialTabbedPages();
				await Navigation.PushModalAsync(tabbedPage);
			}
			else
			{
				var page = new ShiftsPage();
				await Navigation.PushModalAsync(page.WithinNavigationPage());
			}
		}

		private Page GetInitialTabbedPages()
		{
			var tabbedPage = new MainTabbedPage();
			tabbedPage.SetupTabbedPages();
			return tabbedPage;
		}
	}
}
