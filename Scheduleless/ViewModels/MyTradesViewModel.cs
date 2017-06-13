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
    public class MyTradesViewModel : BaseViewModel
    {
        private Boolean _dataLoaded = false;
        public Boolean DataLoaded
        {
            get { return _dataLoaded; }
            set { SetProperty(ref _dataLoaded, value); }
        }

        // TODO: create a ShiftsService to manage all the shifts, but for now this is just POC
        private List<Trade> _myTrades = new List<Trade>();
        public List<Trade> MyTrades
        {
            get { return _myTrades; }
            set { SetProperty(ref _myTrades, value); }
        }

        Command _refreshCommand;
        public Command RefreshCommand
        {
            get
            {
                return _refreshCommand;
            }
        }


        private MyTradesEndpoint _myTradesEndpoint;

        public MyTradesViewModel()
        {
            _myTradesEndpoint = new MyTradesEndpoint();
            _refreshCommand = new Command(RefreshList);
        }

        async void RefreshList()
        {
            MyTrades = await ExecuteFetchMyTradesLiteCommandAsync();
        }

        Command _fetchMyTradesCommand;
        public Command FetchMyTradesCommand
        {
            get { return _fetchMyTradesCommand ?? (_fetchMyTradesCommand = new Command(async () => await ExecuteFetchMyTradesCommandAsync())); }
        }


        async Task<List<Trade>> ExecuteFetchMyTradesLiteCommandAsync()
        {
            Debug.WriteLine("REFRESHING VIEW");
            IsRefreshing = true;
            var response = await _myTradesEndpoint.IndexAsync<Trade>();
            IsRefreshing = false;

            return response.Result.ToList();
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
                Debug.WriteLine("test");
                MyTrades = response.Result.ToList();
            }

            DataLoaded = true;
            IsBusy = false;
        }
    }
}
