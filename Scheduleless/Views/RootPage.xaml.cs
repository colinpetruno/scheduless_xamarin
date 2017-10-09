using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduleless.Services;
using Scheduleless.ViewModels;
using Xamarin.Forms;

namespace Scheduleless.Views
{
	/// <summary>
	/// This class is used as a placeholder when the app starts, so it 
	/// can await for any endpoint calls and decide from there.
	/// </summary>
	public partial class RootPage : RootPageXaml
	{
		public RootPage()
		{
			Initialize();
		}

		protected override void Initialize()
		{
			InitializeComponent();

			Device.BeginInvokeOnMainThread(async () =>
			{
				App.Instance.MainPage = await NavigationService.Instance.GetInitialScreenAsync();
			});
		}
	}

	public partial class RootPageXaml : BaseContentPage<RootViewModel> { }
}
