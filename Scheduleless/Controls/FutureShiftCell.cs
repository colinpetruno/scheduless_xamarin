using System;
using Scheduleless.Models;
using Xamarin.Forms;

namespace Scheduleless.Controls
{
    public class FutureShiftCell : ViewCell
    {
        Label NameLabel;
        Label MonthLabel;
        Label DayLabel;
        StackLayout MainLayout;

        public FutureShiftCell()
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
                TextColor = Color.FromHex("5f97ff"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 18
            };

            DayLabel = new Label
            {
                Text = "10",
                TextColor = Color.FromHex("5f97ff"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 30
            };

            var frameLayout = new StackLayout
            {
                Children = { MonthLabel, DayLabel },
                BackgroundColor = Color.White,
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = 80,
                HeightRequest = 76,
                Padding = new Thickness(0, 12, 0, 0),
            };

            var frame = new Frame
            {
                Content = frameLayout,
                OutlineColor = Color.Silver,
                HasShadow = false,
                BackgroundColor = Color.FromHex("5f97ff"),
                CornerRadius = 0,
                Padding = -5,
                HeightRequest = 60,
                WidthRequest = 102
            };

            MainLayout = new StackLayout
            {
                Children = { frame, NameLabel },
                Orientation = StackOrientation.Horizontal,
                HeightRequest = 120,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0, 0, 0, 0),
                Margin = new Thickness(10, 10, 10, 10),
                BackgroundColor = Color.White
            };

            var outerFrame = new Frame
            {
                Content = MainLayout,
                OutlineColor = Color.Silver,
                HasShadow = false,
                BackgroundColor = Color.White,
                CornerRadius = 0,
                Padding = 0,
                Margin = new Thickness(15, 10, 15, 10),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            View = outerFrame;
        }

        private void SetupBindings()
        {
            // FIXME: resolve this warning
            NameLabel.SetBinding<FutureShift>(Label.TextProperty, futureShift => futureShift.Label);
            MonthLabel.SetBinding<FutureShift>(Label.TextProperty, futureShift => futureShift.ShortMonth);
            DayLabel.SetBinding<FutureShift>(Label.TextProperty, futureShift => futureShift.Day);
        }
    }
}
