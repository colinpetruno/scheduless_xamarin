﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Endpoints;
using Scheduleless.Models;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
	public class FutureShiftDetailViewModel : BaseViewModel
	{
		private FutureShiftsEndpoint _futureShiftsEndpoint;

		private FutureShift _futureShift;
		public FutureShift FutureShift
		{
			get { return _futureShift; }
			set { SetProperty(ref _futureShift, value); }
		}

		public FutureShiftDetailViewModel()
		{
			_futureShiftsEndpoint = new FutureShiftsEndpoint();
		}

		Command _fetchShiftDetailCommand;
		public Command FetchShiftDetailCommand
		{
			get { return _fetchShiftDetailCommand ?? (_fetchShiftDetailCommand = new Command(async () => await ExecuteFetchShiftDetailCommandAsync())); }
		}

		private async Task ExecuteFetchShiftDetailCommandAsync()
		{
			if (IsBusy)
			{
				return;
			}

			IsBusy = true;

			var response = await _futureShiftsEndpoint.ShowAsync<FutureShift>(FutureShift.Id);
			if (response.IsSuccess)
			{
				// TODO: Display data
				Debug.WriteLine(response.Result.Id);
			}

			IsBusy = false;
		}
	}
}
