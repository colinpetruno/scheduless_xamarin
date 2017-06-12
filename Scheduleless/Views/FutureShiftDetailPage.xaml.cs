using System;
using System.Collections.Generic;
using System.Diagnostics;
using Scheduleless.Models;
using Scheduleless.Services;
using Scheduleless.ViewModels;
using Xamarin.Forms;

namespace Scheduleless.Views
{
	public partial class FutureShiftDetailPage : FutureShiftDetailPageXaml
	{
		public FutureShiftDetailPage(FutureShift futureShift)
		{
			ViewModel.FutureShift = futureShift;

			Initialize();
		}

		public string Month
		{
			get
			{
				return ViewModel.FutureShift.ShortMonth;
			}
		}

		protected override void Initialize()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (ViewModel == null || ViewModel.IsBusy)
			{
				return;
			}
		}

		public async void OnTradeShiftButtonClicked(object sender, EventArgs e)
		{
			Debug.WriteLine("Transitioning to New Trade Page");

			await NavigationService.Instance.DisplayNewTradePageAsync(this, ViewModel.FutureShift);
		}

		public async void OnCancelShiftButtonClicked(object sender, EventArgs e)
		{
			Debug.WriteLine("Transitioning to Cancel Shift Page");

			await NavigationService.Instance.DisplayCancelShiftPageAsync(this, ViewModel.FutureShift);
		}
	}

	public partial class FutureShiftDetailPageXaml : BaseContentPage<FutureShiftDetailViewModel> { }
}
