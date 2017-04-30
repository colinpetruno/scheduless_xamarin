using System;
using System.Collections.Generic;
using Scheduleless.Models;
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

			ViewModel.FetchShiftDetailCommand.Execute(null);
		}
	}

	public partial class FutureShiftDetailPageXaml : BaseContentPage<FutureShiftDetailViewModel> { }
}
