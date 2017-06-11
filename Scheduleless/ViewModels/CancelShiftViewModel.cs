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
    public class CancelShiftViewModel : BaseViewModel
    {
        public FutureShift Shift { get; set; }

        private FutureShiftsEndpoint _futureShiftsEndpoint;

        private string _note = string.Empty;

        public CancelShiftViewModel()
        {
            _futureShiftsEndpoint = new FutureShiftsEndpoint();
        }

        public string Note
        {
            get { return _note; }
            set { SetProperty(ref _note, value); }
        }

        Command _cancelShiftCommand;
        public Command CancelShiftCommand
        {
            get { return _cancelShiftCommand ?? (_cancelShiftCommand = new Command(async () => await ExecuteCancelShiftCommandAsync())); }
        }

        private async Task ExecuteCancelShiftCommandAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            DialogService.ShowLoading();
            var response = await _futureShiftsEndpoint.CancelAsync<FutureShift>(Note, Shift.Id);
            DialogService.HideLoading();

            if (response.IsSuccess)
            {
                Debug.WriteLine($"Cancel Shift Succeeded: {response}");
                await NavigationService.Instance.GoToRoot();
                // TODO: Add toast notification
            }
            else
            {
                // TODO: Add toast notification error
                // TODO: Bonus? Can we report to bugsnag?
                Debug.WriteLine($"Cancel Shift Failed: {response.Exception}");
                DialogService.HideLoading();
            }

            IsBusy = false;
        }
    }
}
