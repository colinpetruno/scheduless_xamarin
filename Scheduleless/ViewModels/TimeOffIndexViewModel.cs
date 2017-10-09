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
	public class TimeOffIndexViewModel : BaseViewModel
	{
		private Boolean _dataLoaded = false;
		public Boolean DataLoaded
		{
			get { return _dataLoaded; }
			set { SetProperty(ref _dataLoaded, value); }
		}

		// TODO: create a ShiftsService to manage all the shifts, but for now this is just POC
		private List<TimeOffRequest> _timeOffRequests = new List<TimeOffRequest>();
		public List<TimeOffRequest> TimeOffRequests
		{
			get { return _timeOffRequests; }
			set { SetProperty(ref _timeOffRequests, value); }
		}

		Command _refreshCommand;
		public Command RefreshCommand
		{
			get
			{
				return _refreshCommand;
			}
		}


		private TimeOffRequestEndpoint _timeOffRequestEndpoint;

		public TimeOffIndexViewModel()
		{
			_timeOffRequestEndpoint = new TimeOffRequestEndpoint();
			_refreshCommand = new Command(RefreshList);
		}

		async void RefreshList()
		{
			TimeOffRequests = await ExecuteFetchTimeOffRequestsLiteCommandAsync();
		}

		Command _fetchTimeOffRequestsCommand;
		public Command FetchTimeOffRequestsCommand
		{
			get { return _fetchTimeOffRequestsCommand ?? (_fetchTimeOffRequestsCommand = new Command(async () => await ExecuteFetchTimeOffRequestsCommandAsync())); }
		}


		async Task<List<TimeOffRequest>> ExecuteFetchTimeOffRequestsLiteCommandAsync()
		{
			Debug.WriteLine("REFRESHING VIEW");
			IsRefreshing = true;
			var response = await _timeOffRequestEndpoint.IndexAsync<TimeOffRequest>();
			IsRefreshing = false;

			return response.Result.ToList();
		}

		private async Task ExecuteFetchTimeOffRequestsCommandAsync()
		{
			if (IsBusy)
			{
				return;
			}

			IsBusy = true;

			var response = await _timeOffRequestEndpoint.IndexAsync<TimeOffRequest>();

			if (response.IsSuccess)
			{
				TimeOffRequests = response.Result.ToList();
			}

			DataLoaded = true;
			IsBusy = false;
		}
	}
}
