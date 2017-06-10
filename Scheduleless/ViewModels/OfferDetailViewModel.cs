using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Endpoints;
using Scheduleless.Models;
using Scheduleless.Services;
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
                // TODO: Navigate properly to my trades tab
                Debug.WriteLine($"Decline Offer Succeeded: {response}");
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
