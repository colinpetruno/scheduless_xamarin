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
			Initialize();
		}

		protected override void Initialize()
		{
			InitializeComponent();

			// FIXME: resolve this
			SetupEventHandlers();
			//SetupBindings();
			//SetupPage();
		}

		private void SetupEventHandlers()
		{
			// FIXME: rename this button
			CallToActionButton.SetBinding(Button.CommandProperty, new Binding("LoginCommand"));
		}
	}

	public partial class LoginPageXaml : BaseContentPage<LoginViewModel> { }
}
