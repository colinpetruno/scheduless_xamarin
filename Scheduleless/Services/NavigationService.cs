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
            }
            else
            {
                Debug.WriteLine($"CredentialsSerivce Is Not Authenticated");
                var page = new LoginPage();
                Navigation.PushModalAsync(page);
            }

            return TabbedPage;
        }

        public async void ShowLoginScreen()
        {
            Debug.WriteLine("Displaying Login Modal");
            var page = new LoginPage();
            await Navigation.PushModalAsync(page);
        }

        public async void CloseLoginScreen()
        {
            await Navigation.PopModalAsync();
            TabbedPage = GetInitialTabbedPages() as TabbedPage;
            Application.Current.MainPage = TabbedPage;
        }

        private Page GetInitialTabbedPages()
        {
            var tabbedPage = new MainTabbedPage();
            tabbedPage.SetupTabbedPages();
            return tabbedPage;
        }
    }
}
