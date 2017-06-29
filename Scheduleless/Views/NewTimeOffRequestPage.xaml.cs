using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Localization;
using System.Collections.Generic;
using Scheduleless.Models;
using Scheduleless.ViewModels;
using Xamarin.Forms;

namespace Scheduleless.Views
{
  public partial class NewTimeOffRequestPage : NewTimeOffRequestPageXaml
  {


    public NewTimeOffRequestPage()
    {
      InitializeComponent();
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();

      if (ViewModel == null || ViewModel.IsBusy)
      {
        return;
      }
    }
  }

  public partial class NewTimeOffRequestPageXaml : BaseContentPage<NewTimeOffRequestViewModel> { }
}
