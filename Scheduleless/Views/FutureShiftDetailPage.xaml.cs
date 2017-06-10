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
                return ViewModel.FutureShift.Month;
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

            ViewModel.FetchShiftDetailCommand.Execute(null);
        }

        public async void OnTradeShiftButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Transitioning to New Trade Page");

            var page = new NewTradePage(ViewModel.FutureShift);
            await Navigation.PushAsync(page);
        }

        public async void OnCancelShiftButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("Transitioning to Cancel Shift Page");

            var page = new CancelShiftPage(ViewModel.FutureShift);
            await Navigation.PushAsync(page);
        }


    }

    public partial class FutureShiftDetailPageXaml : BaseContentPage<FutureShiftDetailViewModel> { }
}
