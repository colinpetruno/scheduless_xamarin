using System;
using System.Collections.Generic;
using System.Diagnostics;
using Scheduleless.Models;
using Scheduleless.Services;
using Scheduleless.ViewModels;
using Xamarin.Forms;

namespace Scheduleless.Views
{
  public partial class TimeOffIndexPage : TimeOffIndexPageXaml
  {
    public TimeOffIndexPage()
    {
      Initialize();
    }

    protected override void Initialize()
    {
      InitializeComponent();

      SetupEventHandlers();
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();

      if (ViewModel == null || ViewModel.IsBusy)
      {
        return;
      }

      ViewModel.FetchTimeOffRequestsCommand.Execute(null);
    }

    private void SetupEventHandlers()
    {

    }
  }

  public partial class TimeOffIndexPageXaml : BaseContentPage<TimeOffIndexViewModel> { }
}
