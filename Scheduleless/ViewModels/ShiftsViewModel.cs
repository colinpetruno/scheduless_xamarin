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
	public class ShiftsViewModel : BaseViewModel
	{
		Command _refreshCommand;
		public Command RefreshCommand
		{
			get { return _refreshCommand; }
		}

		private Boolean _dataLoaded = false;
		public Boolean DataLoaded
		{
			get { return _dataLoaded; }
			set { SetProperty(ref _dataLoaded, value); }
		}

		// TODO: create a ShiftsService to manage all the shifts, but for now this is just POC
		private List<FutureShift> _futureShifts = new List<FutureShift>();
		public List<FutureShift> FutureShifts
		{
			get { return _futureShifts; }
			set { SetProperty(ref _futureShifts, value); }
		}

		private FutureShift _featuredShift;
		public FutureShift FeaturedShift
		{
			get { return _featuredShift; }
			set { SetProperty(ref _featuredShift, value); }
		}

		private bool _shouldDisplayFeaturedShift = false;
		public bool ShouldDisplayFeaturedShift
		{
			get { return _shouldDisplayFeaturedShift; }
			set { SetProperty(ref _shouldDisplayFeaturedShift, value); }
		}

		private bool _shouldDisplayEmptyShiftsView = false;
		public bool ShouldDisplayEmptyShiftsView
		{
			get { return _shouldDisplayEmptyShiftsView; }
			set { SetProperty(ref _shouldDisplayEmptyShiftsView, value); }
		}

		public string FeaturedShiftMonth
		{
			get
			{
				if (FeaturedShift != null)
				{
					return FeaturedShift.Month;
				}
				else
				{
					return "";
				}

			}
		}

		public string FeaturedShiftHours
		{
			get
			{

				return FeaturedShift.Label;
			}
		}

		private FutureShiftsEndpoint _futureShiftsEndpoint;
		private FeaturedShiftEndpoint _featuredShiftEndpoint;

		public ShiftsViewModel()
		{
			_futureShiftsEndpoint = new FutureShiftsEndpoint();
			_featuredShiftEndpoint = new FeaturedShiftEndpoint();
			_refreshCommand = new Command(RefreshList);
		}

		async void RefreshList()
		{
			IsRefreshing = true;
			IsBusy = true;
			FutureShifts = await ExecuteFetchShiftsLiteAsync();
			FeaturedShift = await ExecuteFetchFeaturedShiftLiteAsync();
			IsBusy = false;
			IsRefreshing = false;
		}

		Command _fetchAllDataCommand;
		public Command FetchAllDataCommand
		{
			get { return _fetchAllDataCommand ?? (_fetchAllDataCommand = new Command(async () => await ExecuteFetchAllDataCommandAsync())); }
		}

		private async Task ExecuteFetchAllDataCommandAsync()
		{
			if (IsBusy)
			{
				return;
			}

			DialogService.ShowLoading(string.Empty);
			try
			{
				await Task.WhenAll(MakeFutureShiftsEndpointIndexCall(), MakeFeaturedShiftEndpointFeaturedCall());
				DialogService.HideLoading();
				DataLoaded = true;

				HandlePageDisplay();
			}
			catch (Exception ex)
			{
				HandlePageDisplay();

				Debug.WriteLine(ex);
			}

			IsBusy = false;
		}

		private void HandlePageDisplay()
		{
			if (FutureShifts == null || FutureShifts.Count == 0)
			{
				ShouldDisplayEmptyShiftsView = true;
			}
			else
			{
				ShouldDisplayEmptyShiftsView = false;
			}

			if (FeaturedShift != null || (FutureShifts != null && FutureShifts.Count > 0))
			{
				ShouldDisplayFeaturedShift = true;
			}
			else
			{
				ShouldDisplayFeaturedShift = false;
			}
		}

		private async Task<List<FutureShift>> ExecuteFetchShiftsLiteAsync()
		{
			var response = await _futureShiftsEndpoint.IndexAsync<FutureShift>();

			return response.Result.ToList();
		}

		async Task<FutureShift> ExecuteFetchFeaturedShiftLiteAsync()
		{
			var response = await _featuredShiftEndpoint.FeaturedAsync<FutureShift>();

			return response.Result;
		}

		Command _checkInCommand;
		public Command CheckInCommand
		{
			get { return _checkInCommand ?? (_checkInCommand = new Command(async () => await ExecuteCheckInCommandAsync())); }
		}


		private async Task ExecuteCheckInCommandAsync()
		{
			Debug.WriteLine("ExecuteFetchFeatureShiftCommandAsync");
			if (IsBusy)
			{
				return;
			}

			IsBusy = true;

			DialogService.ShowLoading(string.Empty);
			var response = await _featuredShiftEndpoint.CheckInAsync<FutureShift>(FeaturedShift.Id);
			DialogService.HideLoading();

			if (response.IsSuccess)
			{
				FeaturedShift = response.Result;
			}

			IsBusy = false;
		}

		Command _checkOutCommand;
		public Command CheckOutCommand
		{
			get { return _checkOutCommand ?? (_checkOutCommand = new Command(async () => await ExecuteCheckOutCommandAsync())); }
		}
		private async Task ExecuteCheckOutCommandAsync()
		{
			Debug.WriteLine("ExecuteFetchFeatureShiftCommandAsync");
			if (IsBusy)
			{
				return;
			}

			IsBusy = true;

			DialogService.ShowLoading(string.Empty);
			var response = await _featuredShiftEndpoint.CheckOutAsync<FutureShift>(FeaturedShift.Id);
			DialogService.HideLoading();

			if (response.IsSuccess)
			{
				FeaturedShift = response.Result;
			}

			IsBusy = false;
		}

		Command _fetchFeaturedShiftCommand;
		public Command FetchFeaturedShiftCommand
		{
			get { return _fetchFeaturedShiftCommand ?? (_fetchFeaturedShiftCommand = new Command(async () => await ExecuteFetchFeaturedShiftCommandAsync())); }
		}

		private async Task ExecuteFetchFeaturedShiftCommandAsync()
		{
			Debug.WriteLine("ExecuteFetchFeatureShiftCommandAsync");
			if (IsBusy)
			{
				return;
			}

			IsBusy = true;

			DialogService.ShowLoading(string.Empty);
			var response = await _featuredShiftEndpoint.FeaturedAsync<FutureShift>();
			DialogService.HideLoading();

			if (response.IsSuccess)
			{
				FeaturedShift = response.Result;
			}

			IsBusy = false;
		}

		private async Task MakeFutureShiftsEndpointIndexCall()
		{
			var response = await _futureShiftsEndpoint.IndexAsync<FutureShift>();
			if (response.IsSuccess)
			{
				FutureShifts = response.Result.ToList();
			}
		}

		private async Task MakeFeaturedShiftEndpointFeaturedCall()
		{
			var response = await _featuredShiftEndpoint.FeaturedAsync<FutureShift>();
			if (response.IsSuccess)
			{
				FeaturedShift = response.Result;
			}
		}
	}
}
