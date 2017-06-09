using System;
using System.Collections.Generic;
using Scheduleless.Models;
using Scheduleless.Services;
using Scheduleless.ViewModels;
using Xamarin.Forms;

namespace Scheduleless.Views
{
	public partial class MyTradesPage : MyTradesPageXaml
	{
		public MyTradesPage()
		{
			Initialize();
		}

		protected override void Initialize()
		{
			InitializeComponent();

			SetupEventHandlers();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (ViewModel == null || ViewModel.IsBusy)
			{
				return;
			}

			ViewModel.FetchMyTradesCommand.Execute(null);
		}

		private void SetupEventHandlers()
		{
			MyTradesListView.ItemSelected += (s, e) =>
			{
				
				MyTradesListView.SelectedItem = null;
				if (e.SelectedItem == null)
				{
					return; // ItemSelected is called on deselection, which results in SelectedItem being set to null
				}

				var trade = e.SelectedItem as Trade;
				if (trade != null)
				{
                    // FIXME GO SOMEWHERE
					// var page = new FutureShiftDetailPage(futureShift);
					// Navigation.PushAsync(page);
				}
			};
		}
	}

	public partial class MyTradesPageXaml : BaseContentPage<MyTradesViewModel> { }
}
