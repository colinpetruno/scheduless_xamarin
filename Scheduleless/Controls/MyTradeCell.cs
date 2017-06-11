﻿using System;
using Scheduleless.Models;
using Xamarin.Forms;

namespace Scheduleless.Controls
{
    public class MyTradeCell : ViewCell
    {
        Label NameLabel;
        Label MonthLabel;
        Label DayLabel;
        Label FromLabel;
        Label NoteLabel;
        StackLayout MainLayout;

        public MyTradeCell()
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
                FontSize = 17,
                TextColor = Color.FromHex("41454b")
            };

            FromLabel = new Label
            {
                BackgroundColor = Color.Transparent,
                FontAttributes = FontAttributes.None,
                LineBreakMode = LineBreakMode.TailTruncation,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 15,
                WidthRequest = 200,
                Text = "Created by me.",
                TextColor = Color.FromHex("848d98")
            };


            NoteLabel = new Label
            {
                BackgroundColor = Color.Transparent,
                FontAttributes = FontAttributes.None,
                LineBreakMode = LineBreakMode.TailTruncation,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 15,
                WidthRequest = 200,
                HorizontalOptions = LayoutOptions.Start,
                TextColor = Color.FromHex("848d98")
            };

            MonthLabel = new Label
            {
                TextColor = Color.FromHex("5f97ff"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 18
            };

            DayLabel = new Label
            {
                TextColor = Color.FromHex("5f97ff"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 30
            };

            var labelLayout = new StackLayout
            {
                Children = { NameLabel, FromLabel, NoteLabel },
                BackgroundColor = Color.White,
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.CenterAndExpand
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
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = 60,
                WidthRequest = 102
            };

            MainLayout = new StackLayout
            {
                Children = { frame, labelLayout },
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
            NameLabel.SetBinding<Trade>(Label.TextProperty, trade => trade.ShiftLabel);
            MonthLabel.SetBinding<Trade>(Label.TextProperty, trade => trade.ShiftShortMonth);
            DayLabel.SetBinding<Trade>(Label.TextProperty, trade => trade.ShiftDate);
            NoteLabel.SetBinding<Trade>(Label.TextProperty, trade => trade.Note);
        }
    }
}
