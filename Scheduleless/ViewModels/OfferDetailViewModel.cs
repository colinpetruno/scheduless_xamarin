using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Endpoints;
using Scheduleless.Extensions;
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
				await NavigationService.Instance.GoToRoot();
				Debug.WriteLine($"Accept Offer Succeeded: {response}");
			}
			else
			{
				// TODO: Report to Bugsnag
				Debug.WriteLine($"Accept Offer Failed: {response.Exception}");
				DialogService.HideLoading();
				"Accept Offer Failed".ToastError();
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
				await NavigationService.Instance.GoBack();
			}
			else
			{
				// TODO: Bonus report to bugsnag
				Debug.WriteLine($"Decline Offer Failed: {response.Exception}");
				DialogService.HideLoading();
				"Decline Offer Failed".ToastError();
			}

			IsBusy = false;
		}
	}
}
