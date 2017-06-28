using System;
using System.Collections.Generic;
using System.Diagnostics;
using Scheduleless.Models;
using Scheduleless.Services;
using Scheduleless.ViewModels;
using Xamarin.Forms;

namespace Scheduleless.Views
{
	public partial class ShiftsPage : ShiftsPageXaml
	{
		public ShiftsPage()
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

			ViewModel.FetchAllDataCommand.Execute(null);
		}

		private void SetupEventHandlers()
		{
			FutureShiftsListView.ItemSelected += (s, e) =>
			{
				FutureShiftsListView.SelectedItem = null;
				if (e.SelectedItem == null)
				{
					return; // ItemSelected is called on deselection, which results in SelectedItem being set to null
				}

				var futureShift = e.SelectedItem as FutureShift;
				if (futureShift != null)
				{
					NavigationService.Instance.DisplayFutureShiftDetailFor(this, futureShift);
				}
			};
		}

		public void TappedCheckInOutButton(object sender, EventArgs e)
		{

		}
	}

	public partial class ShiftsPageXaml : BaseContentPage<ShiftsViewModel> { }
}
