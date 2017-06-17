using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Scheduleless.Endpoints;
using Scheduleless.Extensions;
using Scheduleless.Models;
using Scheduleless.Services;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
	public class NewOfferViewModel : BaseViewModel
	{
		// This is the trade  
		public AvailableShift AvailableShift { get; set; }

		private List<FutureShift> _availableShifts = new List<FutureShift>();
		public List<FutureShift> AvailableShifts
		{
			get { return _availableShifts; }
			set { SetProperty(ref _availableShifts, value); }
		}


		private FutureShiftsEndpoint _futureShiftsEndpoint;
		private OffersEndpoint _offersEndpoint;
		private string _note = string.Empty;

		private int _selectedShiftIndex = 0;
		public int SelectedShiftIndex
		{
			get { return _selectedShiftIndex; }
			set { SetProperty(ref _selectedShiftIndex, value); }
		}

		public NewOfferViewModel()
		{
			_offersEndpoint = new OffersEndpoint();
			_futureShiftsEndpoint = new FutureShiftsEndpoint();
		}

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

			DialogService.ShowLoading();

			var selectedShift = AvailableShifts[SelectedShiftIndex];

			// Note, OfferedShiftId, TradeId
			var response = await _offersEndpoint.CreateAsync<Offer>(Note, selectedShift.Id, AvailableShift.Id);
			DialogService.HideLoading();

			if (response.IsSuccess)
			{
				Debug.WriteLine($"Create Offer Succeeded: {response}");
				await NavigationService.Instance.GoToRoot();
			}
			else
			{
				// TODO: BONUS: Report to bugsnag?
				Debug.WriteLine($"Create Offer Failed: {response.Exception}");
				DialogService.HideLoading();
				"Create Offer Failed".ToastError();
			}

			IsBusy = false;
		}

		// GET SHIFTS TO OFFER
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
				AvailableShifts = response.Result.ToList();
			}

			IsBusy = false;
		}
	}
}
