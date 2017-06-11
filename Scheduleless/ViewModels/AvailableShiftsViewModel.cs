﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scheduleless.Endpoints;
using Scheduleless.Models;
using Scheduleless.Services;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
    public class AvailableShiftsViewModel : BaseViewModel
    {
        private Boolean _dataLoaded = false;
        public Boolean DataLoaded
        {
            get { return _dataLoaded; }
            set { SetProperty(ref _dataLoaded, value); }
        }

        // TODO: create a ShiftsService to manage all the shifts, but for now this is just POC
        private List<AvailableShift> _availableShifts = new List<AvailableShift>();
        public List<AvailableShift> AvailableShifts
        {
            get { return _availableShifts; }
            set { SetProperty(ref _availableShifts, value); }
        }

        private AvailableShiftsEndpoint _availableShiftsEndpoint;

        public AvailableShiftsViewModel()
        {
            _availableShiftsEndpoint = new AvailableShiftsEndpoint();
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
            var response = await _availableShiftsEndpoint.IndexAsync<AvailableShift>();
            DialogService.HideLoading();

            if (response.IsSuccess)
            {
                AvailableShifts = response.Result.ToList();
            }

            DataLoaded = true;
            IsBusy = false;
        }
    }
}
