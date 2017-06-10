using System;
using System.Collections.Generic;
using System.Diagnostics;
using Scheduleless.Models;
using Scheduleless.Services;
using Scheduleless.ViewModels;
using Xamarin.Forms;

namespace Scheduleless.Views
{
    public partial class OfferDetailPage : OfferDetailPageXaml
    {
        public OfferDetailPage(Offer offer)
        {
            ViewModel.Offer = offer;

            Initialize();
        }

        public string Month
        {
            get
            {
                return "July";
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

        //public async void OnTradeShiftButtonClicked(object sender, EventArgs e)
        //{
        //	Debug.WriteLine("Transitioning to New Trade Page");
        //	// await NavigationService.Instance.DisplayNewTradePageAsync(ViewModel.FutureShift);

        //	var page = new NewTradePage(ViewModel.FutureShift);
        //	await Navigation.PushAsync(page);
        //}


    }

    public partial class OfferDetailPageXaml : BaseContentPage<OfferDetailViewModel> { }
}
