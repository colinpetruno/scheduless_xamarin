using System;
using System.Collections.Generic;
using Scheduleless.Models;
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
            AvailableShiftsListView.ItemSelected += (s, e) =>
            {

                AvailableShiftsListView.SelectedItem = null;
                if (e.SelectedItem == null)
                {
                    return; // ItemSelected is called on deselection, which results in SelectedItem being set to null
                }

                var availableShift = e.SelectedItem as AvailableShift;
                if (availableShift != null)
                {
                    var page = new NewOfferPage(availableShift);
                    Navigation.PushAsync(page);
                }
            };
        }
    }

    public partial class AvailableShiftsPageXaml : BaseContentPage<AvailableShiftsViewModel> { }
}
