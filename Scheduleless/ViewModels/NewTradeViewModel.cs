using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Scheduleless.Endpoints;
using Scheduleless.Extensions;
using Scheduleless.Models;
using Scheduleless.Services;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
  public class NewTradeViewModel : BaseViewModel
  {
    public FutureShift Shift { get; set; }

    private TradesEndpoint _tradesEndpoint;



    public NewTradeViewModel()
    {
      _tradesEndpoint = new TradesEndpoint();
    }

    private string _note = string.Empty;
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

      if (!IsFieldValid(Note))
      {
        "Please enter add a note of why you want to trade your shift".ToastError();
        return;
      }

      IsBusy = true;

      DialogService.ShowLoading();
      var response = await _tradesEndpoint.CreateAsync<FutureShift>(Note, Shift);
      DialogService.HideLoading();

      if (response.IsSuccess)
      {
        Debug.WriteLine($"Create Trade Succeeded: {response}");
        await NavigationService.Instance.GoToRoot();
      }
      else
      {
        // TODO: BONUS: Report to bugsnag
        Debug.WriteLine($"Create Trade Failed: {response.Exception}");
        DialogService.HideLoading();
        "Create Trade Failed".ToastError();
      }

      IsBusy = false;
    }
  }
}
