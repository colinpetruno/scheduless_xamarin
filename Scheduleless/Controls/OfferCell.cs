using System;
using Scheduleless.Models;
using Xamarin.Forms;

namespace Scheduleless.Controls
{
    public class OfferCell : ViewCell
    {
        Label NameLabel;
        Label MonthLabel;
        Label DayLabel;
        StackLayout MainLayout;

        public OfferCell()
        {
            SetupUserInterface();
            SetupBindings();
        }

        private void SetupUserInterface()
        {
            NameLabel = new Label
            {
                BackgroundColor = Color.Transparent,
                FontAttributes = FontAttributes.None,
                LineBreakMode = LineBreakMode.TailTruncation,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 16,
                TextColor = Color.Black
            };

            MonthLabel = new Label
            {
                Text = "Jul",
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18
            };

            DayLabel = new Label
            {
                Text = "10",
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18
            };

            var frameLayout = new StackLayout
            {
                Children = { MonthLabel, DayLabel },
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = 0
            };

            var frame = new Frame
            {
                Content = frameLayout,
                OutlineColor = Color.Silver,
                HasShadow = false,
                CornerRadius = 0,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HeightRequest = 60,
                WidthRequest = 60
            };



            MainLayout = new StackLayout
            {
                Children = { frame, NameLabel },
                Orientation = StackOrientation.Horizontal,
                HeightRequest = 120,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10, 10, 10, 10)
            };

            View = MainLayout;
        }

        private void SetupBindings()
        {
            // FIXME: resolve this warning
            NameLabel.SetBinding<Offer>(Label.TextProperty, offer => offer.Label);
            MonthLabel.SetBinding<Offer>(Label.TextProperty, offer => offer.Month);
            DayLabel.SetBinding<Offer>(Label.TextProperty, offer => offer.Day);
        }
    }
}
