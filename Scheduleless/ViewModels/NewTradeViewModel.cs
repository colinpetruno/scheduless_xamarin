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
    public class NewTradeViewModel : BaseViewModel
    {
        public FutureShift Shift { get; set; }

        private TradesEndpoint _tradesEndpoint;

        private string _note = string.Empty;

        public NewTradeViewModel()
        {
            _tradesEndpoint = new TradesEndpoint();
        }

        public string Note
        {
            get { return _note; }
            set { SetProperty(ref _note, value); }
        }

        Command _createTradeCommand;
        public Command CreateTradeCommand
        {
            get { return _createTradeCommand ?? (_createTradeCommand = new Command(async () => await ExecuteCreateTradeCommandAsync())); }
        }

        private async Task ExecuteCreateTradeCommandAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            DialogService.ShowLoading();
            var response = await _tradesEndpoint.CreateAsync<FutureShift>(Note, Shift);
            DialogService.HideLoading();

            if (response.IsSuccess)
            {
                await NavigationService.Instance.GoToRoot();
                //NavigationService.Instance.DisplayTabFor(2);
                Debug.WriteLine($"Create Trade Succeeded: {response}");
            }
            else
            {
                // TODO: Figure out how to show error
                Debug.WriteLine($"Create Trade Failed: {response.Exception}");
                DialogService.HideLoading();
            }

            IsBusy = false;
        }
    }
}
