using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scheduleless.Endpoints;
using Scheduleless.Models;
using Scheduleless.Services;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
    public class OffersViewModel : BaseViewModel
    {
        public Trade Trade { get; set; }

        // TODO: create a ShiftsService to manage all the shifts, but for now this is just POC
        private List<Offer> _offers = new List<Offer>();
        public List<Offer> Offers
        {
            get { return _offers; }
            set { SetProperty(ref _offers, value); }
        }

        private OffersEndpoint _offersEndpoint;

        public OffersViewModel()
        {
            _offersEndpoint = new OffersEndpoint();
        }

        Command _fetchOffersCommand;
        public Command FetchOffersCommand
        {
            get { return _fetchOffersCommand ?? (_fetchOffersCommand = new Command(async () => await ExecuteFetchOffersCommandAsync())); }
        }

        private async Task ExecuteFetchOffersCommandAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            DialogService.ShowLoading(string.Empty);
            var response = await _offersEndpoint.IndexAsync<Offer>(Trade);
            DialogService.HideLoading();

            if (response.IsSuccess)
            {
                Offers = response.Result.ToList();
            }

            IsBusy = false;
        }
    }
}
