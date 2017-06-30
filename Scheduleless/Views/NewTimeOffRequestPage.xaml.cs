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

      endDatepicker.MinimumDate = System.DateTime.Now;
      endDatepicker.MaximumDate = System.DateTime.Now.AddMonths(6);

      startDatepicker.MinimumDate = System.DateTime.Now;
      startDatepicker.MaximumDate = System.DateTime.Now.AddMonths(7);
    }
  }

  public partial class NewTimeOffRequestPageXaml : BaseContentPage<NewTimeOffRequestViewModel> { }
}
