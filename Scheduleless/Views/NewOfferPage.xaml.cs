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
    public partial class NewOfferPage : NewOfferPageXaml
    {


        public NewOfferPage(AvailableShift availableShift)
        {
            ViewModel.AvailableShift = availableShift;
            ViewModel.FetchShiftsCommand.Execute(null);

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel == null || ViewModel.IsBusy)
            {
                return;
            }
            // ViewModel.FetchShiftsCommand.Execute(null);
        }
    }

    public partial class NewOfferPageXaml : BaseContentPage<NewOfferViewModel> { }
}
