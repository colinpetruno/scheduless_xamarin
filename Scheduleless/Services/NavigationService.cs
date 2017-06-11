using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Renderers;
using Scheduleless.Views;
using Scheduleless.Models;
using Xamarin.Forms;

namespace Scheduleless.Services
{
    public class NavigationService
    {
        private static readonly Lazy<NavigationService> lazy = new Lazy<NavigationService>(() => new NavigationService());
        private CredentialsService _credentialsService;
        private TabbedPage TabbedPage { get; set; } = new TabbedPage();

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


            TabbedPage = GetInitialTabbedPages() as TabbedPage;
            Navigation = TabbedPage.Navigation;

            if (_credentialsService.IsAuthenticated)
            {
                Debug.WriteLine($"CredentialsSerivce Is Authenticated");
                var return_page = TabbedPage;
                Navigation = TabbedPage.Navigation;
                return return_page;
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
            TabbedPage = GetInitialTabbedPages() as TabbedPage;
            await Navigation.PushModalAsync(TabbedPage);

        }

        public async Task GoToRoot()
        {
            Debug.WriteLine($"Going to root of tab");
            await Navigation.PopToRootAsync();
        }

        public void DisplayTabFor(int tabPageIndex)
        {

            TabbedPage.CurrentPage = TabbedPage.Children[tabPageIndex];
        }

        private Page GetInitialTabbedPages()
        {
            var tabbedPage = new MainTabbedPage();
            tabbedPage.SetupTabbedPages();
            return tabbedPage;
        }
    }
}
