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
	public class ShiftsViewModel : BaseViewModel
	{
		// TODO: create a ShiftsService to manage all the shifts, but for now this is just POC
		private List<FutureShift> _futureShifts = new List<FutureShift>();
		public List<FutureShift> FutureShifts
		{
			get { return _futureShifts; }
			set { SetProperty(ref _futureShifts, value); }
		}

		private FutureShiftsEndpoint _futureShiftsEndpoint;

		public ShiftsViewModel()
		{
			_futureShiftsEndpoint = new FutureShiftsEndpoint();
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
				FutureShifts = response.Result.ToList();
			}

			IsBusy = false;
		}
	}
}
