using System;
using System.Collections.Generic;
using Scheduleless.ViewModels;
using Scheduleless.Views;
using Xamarin.Forms;

namespace Scheduleless.Views
{
	public partial class LoginPage : LoginPageXaml
	{
		public LoginPage()
		{
			NavigationPage.SetHasNavigationBar(this, false);
			Initialize();
		}

		protected override void Initialize()
		{
			InitializeComponent();
		}
	}

	public partial class LoginPageXaml : BaseContentPage<LoginViewModel> { }
}
