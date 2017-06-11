using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Localization;
using System.Collections.Generic;
using Scheduleless.Models;
using Scheduleless.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.BindablePicker;

namespace Scheduleless.Views
{
    public partial class NewOfferPage : NewOfferPageXaml
    {


        public NewOfferPage()
        {
            // ViewModel.Shift = shift;
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel == null || ViewModel.IsBusy)
            {
                return;
            }

            ViewModel.FetchShiftsCommand.Execute(null);
        }
    }

    public partial class NewOfferPageXaml : BaseContentPage<NewOfferViewModel> { }
}
