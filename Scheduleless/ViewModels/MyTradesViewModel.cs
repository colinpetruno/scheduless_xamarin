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
	public class MyTradesViewModel : BaseViewModel
	{
		// TODO: create a ShiftsService to manage all the shifts, but for now this is just POC
		private List<Trade> _myTrades = new List<Trade>();
		public List<Trade> MyTrades
		{
			get { return _myTrades; }
			set { SetProperty(ref _myTrades, value); }
		}

		private MyTradesEndpoint _myTradesEndpoint;

		public MyTradesViewModel()
		{
			_myTradesEndpoint = new MyTradesEndpoint();
		}

		Command _fetchMyTradesCommand;
		public Command FetchMyTradesCommand
		{
			get { return _fetchMyTradesCommand ?? (_fetchMyTradesCommand = new Command(async () => await ExecuteFetchMyTradesCommandAsync())); }
		}

		private async Task ExecuteFetchMyTradesCommandAsync()
		{
			if (IsBusy)
			{
				return;
			}

			IsBusy = true;

			DialogService.ShowLoading(string.Empty);
			var response = await _myTradesEndpoint.IndexAsync<Trade>();
			DialogService.HideLoading();

			if (response.IsSuccess)
			{
				MyTrades = response.Result.ToList();
			}

			IsBusy = false;
		}
	}
}
