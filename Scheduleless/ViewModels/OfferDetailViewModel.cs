using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Endpoints;
using Scheduleless.Models;
using Scheduleless.Services;
using Scheduleless.Views;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
    public class OfferDetailViewModel : BaseViewModel
    {
        private OffersEndpoint _offersEndpoint;

        public Offer Offer { get; set; }

        public OfferDetailViewModel()
        {
            _offersEndpoint = new OffersEndpoint();
        }

        public string Day
        {
            get { return Offer.ShiftDate; }
        }

        public string Month
        {
            get { return Offer.ShiftShortMonth; }
        }

        public string Label
        {
            get { return Offer.ShiftLabel; }
        }

        public string LocationName
        {
            get { return Offer.LocationName; }
        }

        public string LocationLine1
        {
            get { return Offer.LocationLine1; }
        }

        public string LocationLine2
        {
            get { return Offer.LocationLine2; }
        }

        public string LocationCityStateZip
        {
            get { return Offer.LocationCityStateZip; }
        }

        Command _acceptOfferCommand;
        public Command AcceptOfferCommand
        {
            get { return _acceptOfferCommand ?? (_acceptOfferCommand = new Command(async () => await ExecuteAcceptOfferCommandAsync())); }
        }

        private async Task ExecuteAcceptOfferCommandAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            DialogService.ShowLoading();
            var response = await _offersEndpoint.AcceptAsync<Offer>(Offer.Id);
            DialogService.HideLoading();

            if (response.IsSuccess)
            {
                // TODO: Navigate properly to my trades tab
                Debug.WriteLine($"Accept Offer Succeeded: {response}");
            }
            else
            {
                // TODO: Figure out how to show error
                Debug.WriteLine($"Accept Offer Failed: {response.Exception}");
                DialogService.HideLoading();
            }

            IsBusy = false;
        }

        Command _declineOfferCommand;
        public Command DeclineOfferCommand
        {
            get { return _declineOfferCommand ?? (_declineOfferCommand = new Command(async () => await ExecuteDeclineOfferCommandAsync())); }
        }

        private async Task ExecuteDeclineOfferCommandAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            DialogService.ShowLoading();
            var response = await _offersEndpoint.DeclineAsync<Offer>(Offer.Id);
            DialogService.HideLoading();

            if (response.IsSuccess)
            {
                Debug.WriteLine($"Decline Offer Succeeded: {response}");
                Debug.WriteLine($"Attempt to Transition");
                // TODO: Navigate properly to my trades tab

                Application.Current.MainPage = new MyTradesPage();

            }
            else
            {
                // TODO: Figure out how to show error
                Debug.WriteLine($"Decline Offer Failed: {response.Exception}");
                DialogService.HideLoading();
            }

            IsBusy = false;
        }
    }
}
