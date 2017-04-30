using System;
using System.Threading.Tasks;
using Scheduleless.Renderers;
using Scheduleless.Views;
using Xamarin.Forms;

namespace Scheduleless.Services
{
	public class NavigationService
	{
		private static readonly Lazy<NavigationService> lazy = new Lazy<NavigationService>(() => new NavigationService());
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
			// TODO: if user is already logged in - take them to the shifts page
			var page = new LoginPage();
			var navPage = new ThemedNavigationPage(page);
			Navigation = navPage.Navigation;
			return navPage;
		}

		public async Task DisplayShiftsPageAsync()
		{
			var page = new ShiftsPage();
			await Navigation.PushModalAsync(page.WithinNavigationPage());
		}
	}
}
