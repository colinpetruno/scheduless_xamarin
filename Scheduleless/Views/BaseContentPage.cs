using System;
using System.Collections.ObjectModel;
using Scheduleless.Helpers;
using Scheduleless.Interfaces;
using Scheduleless.Renderers;
using Scheduleless.Services;
using Scheduleless.ViewModels;
using Xamarin.Forms;

namespace Scheduleless.Views
{
    /// <summary>
    /// Each ContentPage is required to align with a corresponding ViewModel
    /// ViewModels will be the BindingContext by default
    /// </summary>
    public class BaseContentPage<T> : MainBaseContentPage where T : BaseViewModel, new()
    {
        public BaseContentPage()
        {
            try
            {
                BindingContext = ViewModel;
            }
            catch (Exception)
            {
                // FIXME: address this
                //NavigationService.Instance.Logout();
                //FirebaseService.Instance.Revoke();
                //"Sorry, there was an issue fetching your account. Please sign back in.".ToToast(ToastNotificationType.Error);
            }
        }

        ~BaseContentPage()
        {
            _viewModel = null;
        }

        protected T _viewModel;
        public T ViewModel
        {
            get
            {
                return _viewModel ?? (_viewModel = new T());
            }
        }
    }

    public class MainBaseContentPage : ContentPage
    {
        public MainBaseContentPage()
        {
            BarBackgroundColor = Colors.NavigationBarColor;
            BarTextColor = Colors.NavigationBarTextColor;
            BackgroundColor = Color.White;
        }

        public void HandleAddToolbarItem(string imageName, string buttonText, Command action)
        {
            if (Device.OS == TargetPlatform.iOS)
            {
                DependencyService.Get<IBaseContentPage>().UpdateToolbarItems();
            }
            else
            {
                var item = new ToolbarItem
                {
                    Text = buttonText,
                    Icon = imageName,
                    Command = action
                };
                ToolbarItems.Add(item);
            }
        }

        public Color BarTextColor
        {
            get;
            set;
        }

        public Color BarBackgroundColor
        {
            get;
            set;
        }

        public bool HasInitialized
        {
            get;
            private set;
        }

        protected virtual void OnLoaded()
        {
            // TODO: add tracking?
            //TrackPage(new Dictionary<string, string>());
        }

        protected virtual void Initialize()
        {
            SetBinding(Page.TitleProperty, new Binding(BaseViewModel.TitlePropertyName));
            SetBinding(Page.IconProperty, new Binding(BaseViewModel.IconPropertyName));
            SetBinding(Page.IsBusyProperty, new Binding(BaseViewModel.IsBusyPropertyName));
        }

        protected override void OnAppearing()
        {
            var nav = Parent as NavigationPage;
            if (nav != null)
            {
                nav.BarBackgroundColor = Colors.NavigationBarColor;
                nav.BarTextColor = Colors.NavigationBarTextColor;
            }

            if (!HasInitialized)
            {
                HasInitialized = true;
                OnLoaded();
            }

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            //MessagingCenter.Unsubscribe<AuthenticationViewModel>(this, Messages.UserAuthenticated);
            //_hasSubscribed = false;

            base.OnDisappearing();
            //EvaluateNavigationStack();
        }

        /// <summary>
        /// Wraps the ContentPage within a NavigationPage
        /// </summary>
        /// <returns>The navigation page.</returns>
        public NavigationPage WithinNavigationPage()
        {
            var nav = new ThemedNavigationPage(this);
            ApplyTheme(nav);
            return nav;
        }

        protected void SetTheme()
        {
            BarBackgroundColor = Colors.NavigationBarColor;
            BarTextColor = Colors.NavigationBarTextColor;
        }

        public void ApplyTheme(NavigationPage nav)
        {
            nav.BarBackgroundColor = BarBackgroundColor;
            nav.BarTextColor = BarTextColor;
        }
    }
}
