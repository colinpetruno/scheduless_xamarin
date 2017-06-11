using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Scheduleless.Endpoints;
using Scheduleless.Models;
using Scheduleless.Services;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
    public class NewOfferViewModel : BaseViewModel
    {
        private OffersEndpoint _offersEndpoint;
        private FutureShiftsEndpoint _futureShiftsEndpoint;

        public NewOfferViewModel()
        {
            _offersEndpoint = new OffersEndpoint();
            _futureShiftsEndpoint = new FutureShiftsEndpoint();
        }

        public int OfferedShift { get; set; }


        private string _note = string.Empty;
        public string Note
        {
            get { return _note; }
            set { SetProperty(ref _note, value); }
        }

        Command _createOfferCommand;
        public Command CreateOfferCommand
        {
            get { return _createOfferCommand ?? (_createOfferCommand = new Command(async () => await ExecuteCreateOfferCommandAsync())); }
        }

        private async Task ExecuteCreateOfferCommandAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            //DialogService.ShowLoading();
            var response = await _offersEndpoint.CreateAsync<Offer>(3);
            //DialogService.HideLoading();

            //if (response.IsSuccess)
            //{
            //    // TODO: Navigate properly to my trades tab
            //    Debug.WriteLine($"Create Trade Succeeded: {response}");
            //}
            //else
            //{
            //    // TODO: Figure out how to show error
            //    Debug.WriteLine($"Create Trade Failed: {response.Exception}");
            //    DialogService.HideLoading();
            //}

            IsBusy = false;
        }






        // GET LIST OF SHIFTS I CAN OFFER

        private List<FutureShift> _shiftsToOffer = new List<FutureShift>();
        public List<FutureShift> ShiftsToOffer
        {
            get { return _shiftsToOffer; }
            set { SetProperty(ref _shiftsToOffer, value); }
        }



        Command _fetchShiftsCommand;
        public Command FetchShiftsCommand
        {
            get { return _fetchShiftsCommand ?? (_fetchShiftsCommand = new Command(async () => await ExecuteFetchShiftsCommandAsync())); }
        }

        private async Task ExecuteFetchShiftsCommandAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            DialogService.ShowLoading(string.Empty);
            var response = await _futureShiftsEndpoint.IndexAsync<FutureShift>();
            DialogService.HideLoading();

            if (response.IsSuccess)
            {
                ShiftsToOffer = response.Result.ToList();
            }

            IsBusy = false;
        }
    }
}
