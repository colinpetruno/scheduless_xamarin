using System;
using System.Collections.Generic;

using Scheduleless.ViewModels;

using Xamarin.Forms;

namespace Scheduleless.Views
{
    public partial class AvailableShiftsPage : AvailableShiftsPageXaml
    {
        public AvailableShiftsPage()
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

            ViewModel.FetchShiftsCommand.Execute(null);
        }

        private void SetupEventHandlers()
        {
           // FIXME this needs to have the handler setup to go into the trade
           // view
        }
    }
    
    public partial class AvailableShiftsPageXaml : BaseContentPage<AvailableShiftsViewModel> { }
}
