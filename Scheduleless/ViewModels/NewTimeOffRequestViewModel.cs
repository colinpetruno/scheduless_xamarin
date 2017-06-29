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
  public class NewTimeOffRequestViewModel : BaseViewModel
  {




    private DateTime _startDate = DateTime.Now;
    public DateTime StartDate
    {
      get { return _startDate; }
      set { SetProperty(ref _startDate, value); }
    }

    private DateTime _endDate = DateTime.Now;
    public DateTime EndDate
    {
      get { return _endDate; }
      set { SetProperty(ref _endDate, value); }
    }

    private TimeSpan _startTime;
    public TimeSpan StartTime
    {
      get { return _startTime; }
      set { SetProperty(ref _startTime, value); }
    }

    private TimeSpan _endTime = new TimeSpan(23, 59, 00);
    public TimeSpan EndTime
    {
      get { return _endTime; }
      set { SetProperty(ref _endTime, value); }
    }


    private TimeOffRequestEndpoint _timeOffRequestEndpoint;

    public NewTimeOffRequestViewModel()
    {
      _timeOffRequestEndpoint = new TimeOffRequestEndpoint();
    }





    Command _requestTimeOffCommand;
    public Command RequestTimeOffCommand
    {
      get { return _requestTimeOffCommand ?? (_requestTimeOffCommand = new Command(async () => await ExecuteRequestTimeOffCommandAsync())); }
    }

    private async Task ExecuteRequestTimeOffCommandAsync()
    {
      if (IsBusy)
      {
        return;
      }

      //if (!IsFieldValid(Note))
      //{
      //  "Please enter add a note of why you want to trade your shift".ToastError();
      //  return;
      //}

      if (!IsFormValid())
      {
        return;
      }

      IsBusy = true;

      DialogService.ShowLoading();
      var response = await _timeOffRequestEndpoint.CreateAsync<TimeOffRequest>(EndDate, EndTime, StartDate, StartTime);
      DialogService.HideLoading();

      if (response.IsSuccess)
      {
        Debug.WriteLine($"Create Time Off Succeeded: {response}");
        await NavigationService.Instance.GoToRoot();
      }
      else
      {
        // TODO: BONUS: Report to bugsnag
        Debug.WriteLine($"Create Time Off Failed: {response.Exception}");
        DialogService.HideLoading();
        "Your Time Off Request Wasn't Processed.".ToastError();
      }

      IsBusy = false;
    }

    private bool IsFormValid()
    {
      if (EndDate < StartDate)
      {
        "The end date must be greater than the start date.".ToastError();
        return false;
      }

      if (EndDate == StartDate && EndTime < StartTime)
      {
        "The start time must be before the end time.".ToastError();
        return false;
      }
      return true;
    }
  }
}
