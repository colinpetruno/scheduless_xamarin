using System;
using System.Collections.Generic;
using Scheduleless.Models;
using Scheduleless.Services;
using Scheduleless.ViewModels;
using Xamarin.Forms;

namespace Scheduleless.Views
{
    public partial class OffersPage : OffersPageXaml
    {
        public OffersPage(Trade trade)
        {
            ViewModel.Trade = trade;
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

            ViewModel.FetchOffersCommand.Execute(null);
        }

        private void SetupEventHandlers()
        {
            OffersListView.ItemSelected += (s, e) =>
            {
                OffersListView.SelectedItem = null;
                if (e.SelectedItem == null)
                {
                    return; // ItemSelected is called on deselection, which results in SelectedItem being set to null
                }

                var offer = e.SelectedItem as Offer;
                if (offer != null)
                {
                    // TODO: GO SOMEWHERE
                    var page = new OfferDetailPage(offer);
                    Navigation.PushAsync(page);
                }
            };
        }
    }

    public partial class OffersPageXaml : BaseContentPage<OffersViewModel> { }
}
