using Scheduleless.Services;
using Scheduleless.Views;
using Xamarin.Forms;

namespace Scheduleless
{
	public partial class App : Application
	{
		static App _instance;
		public static App Instance
		{
			get
			{
				return _instance;
			}
		}

		public App()
		{
			_instance = this;

			InitializeComponent();

			MainPage = NavigationService.Instance.GetInitialScreen();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
