using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Renderers;
using Scheduleless.Views;
using Scheduleless.Models;
using Xamarin.Forms;

namespace Scheduleless.Services
{
	public enum TabPageIndex
	{
		YourSchedule = 0,
		AvailableShifts,
		YourTrades
	}

	public class NavigationService
	{
		private static readonly Lazy<NavigationService> lazy = new Lazy<NavigationService>(() => new NavigationService());
		private TabbedPage TabbedPage { get; set; } = new TabbedPage();


		public static NavigationService Instance
		{
			get
			{
				return lazy.Value;
			}
		}

		public INavigation Navigation { get; private set; }

		public async Task<Page> GetInitialScreenAsync()
		{
			Debug.WriteLine($"GetInitialScreen");

			if (await AuthenticationService.Instance.IsAuthenticatedAsync())
			{
				Debug.WriteLine($"CredentialsSerivce Is Authenticated");
				TabbedPage = GetInitialTabbedPages() as TabbedPage;
				Navigation = TabbedPage.Navigation;
				return TabbedPage;
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


		public async Task DisplayLoginScreenAsync()
		{
			var page = new LoginPage();
			await Navigation.PushModalAsync(page);
		}

		public async Task DisplayShiftsPageAsync(bool isFromLoginScreen)
		{
			TabbedPage = GetInitialTabbedPages() as TabbedPage;
			await Navigation.PushModalAsync(TabbedPage);
		}

		public async Task DisplayNewTradePageAsync(ContentPage sourcePage, FutureShift futureShift)
		{
			var page = DisplayNewPage(sourcePage, new NewTradePage(futureShift));
			await sourcePage.Navigation.PushAsync(page);
		}

		public async Task DisplayCancelShiftPageAsync(ContentPage sourcePage, FutureShift futureShift)
		{
			var page = DisplayNewPage(sourcePage, new CancelShiftPage(futureShift));
			await sourcePage.Navigation.PushAsync(page);
		}

		internal void DisplayNewOfferPageFor(ContentPage sourcePage, AvailableShift availableShift)
		{
			var page = DisplayNewPage(sourcePage, new NewOfferPage(availableShift));
			sourcePage.Navigation.PushAsync(page);
		}

		internal void DisplayNewRequestTimeOffPageAsync(ContentPage sourcePage)
		{
			var page = DisplayNewPage(sourcePage, new NewTimeOffRequestPage());
			sourcePage.Navigation.PushAsync(page);
		}

		internal void DisplayFutureShiftDetailFor(ContentPage sourcePage, FutureShift futureShift)
		{
			var page = DisplayNewPage(sourcePage, new FutureShiftDetailPage(futureShift));
			sourcePage.Navigation.PushAsync(page);
		}

		internal void DisplayOfferDetailFor(ContentPage sourcePage, Offer offer)
		{
			var page = DisplayNewPage(sourcePage, new OfferDetailPage(offer));
			sourcePage.Navigation.PushAsync(page);
		}

		public async Task GoBack()
		{
			Debug.WriteLine($"Going back one tab");
			await Navigation.PopAsync();
		}


		public async Task GoToRoot()
		{
			Debug.WriteLine($"Going to root of tab");
			await Navigation.PopToRootAsync();
		}

		public void DisplayTabFor(TabPageIndex tabPageIndex)
		{
			TabbedPage.CurrentPage = TabbedPage.Children[(int)tabPageIndex];
		}

		private Page GetInitialTabbedPages()
		{
			var tabbedPage = new MainTabbedPage();
			tabbedPage.SetupTabbedPages();
			return tabbedPage;
		}

		/// <summary>
		/// Wrapper to set the Navigation so it knows where it is at in the nav stack.
		/// </summary>
		/// <returns>The new page.</returns>
		/// <param name="sourcePage">Source page.</param>
		/// <param name="destinationPage">Destination page.</param>
		private ContentPage DisplayNewPage(ContentPage sourcePage, ContentPage destinationPage)
		{
			Navigation = sourcePage.Navigation;
			return destinationPage;
		}
	}
}
