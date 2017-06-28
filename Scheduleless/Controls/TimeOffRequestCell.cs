using System;
using Scheduleless.Models;
using Xamarin.Forms;

namespace Scheduleless.Controls
{
  public class TimeOffRequestCell : ViewCell
  {
    Label Label;
    Label StatusLabel;
    Frame StatusFrame;
    StackLayout MainLayout;

    public TimeOffRequestCell()
    {
      SetupUserInterface();
      SetupBindings();
    }

    private void SetupUserInterface()
    {
      Label = new Label
      {
        BackgroundColor = Color.Transparent,
        FontAttributes = FontAttributes.None,
        LineBreakMode = LineBreakMode.WordWrap,
        VerticalOptions = LayoutOptions.CenterAndExpand,
        HorizontalOptions = LayoutOptions.CenterAndExpand,
        HorizontalTextAlignment = TextAlignment.Center,
        FontSize = 17,
        TextColor = Color.FromHex("41454b")
      };

      StatusLabel = new Label
      {
        FontAttributes = FontAttributes.None,
        LineBreakMode = LineBreakMode.WordWrap,
        VerticalOptions = LayoutOptions.CenterAndExpand,
        HorizontalOptions = LayoutOptions.CenterAndExpand,
        HorizontalTextAlignment = TextAlignment.Center,
        FontSize = 17,
        TextColor = Color.FromHex("41454b")
      };


      StatusFrame = new Frame
      {
        Content = StatusLabel,
        HasShadow = false,
        BackgroundColor = Color.FromHex("5f97ff"),
        CornerRadius = 0,
        Padding = -5,
        HeightRequest = 60,
        WidthRequest = 102
      };




      //var frameLayout = new StackLayout
      //{
      //  Children = { Label, StatusFrame },
      //  BackgroundColor = Color.White,
      //  Orientation = StackOrientation.Vertical,
      //  HorizontalOptions = LayoutOptions.Center,
      //  VerticalOptions = LayoutOptions.CenterAndExpand,
      //  HeightRequest = 76,
      //  Padding = new Thickness(0, 12, 0, 0),
      //};

      //var frame = new Frame
      //{
      //  Content = frameLayout,
      //  OutlineColor = Color.Silver,
      //  HasShadow = false,
      //  BackgroundColor = Color.FromHex("5f97ff"),
      //  CornerRadius = 0,
      //  Padding = -5,
      //  HorizontalOptions = LayoutOptions.Start,
      //  HeightRequest = 60,
      //  WidthRequest = 102
      //};

      MainLayout = new StackLayout
      {
        Children = { Label, StatusFrame },
        Orientation = StackOrientation.Vertical,
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
      Label.SetBinding<TimeOffRequest>(Label.TextProperty, timeOffRequest => timeOffRequest.Label);
      StatusLabel.SetBinding<TimeOffRequest>(Label.TextProperty, timeOffRequest => timeOffRequest.Status);
      StatusFrame.SetBinding<TimeOffRequest>(Frame.BackgroundColorProperty, timeOffRequest => timeOffRequest.BackgroundColor);
      StatusFrame.SetBinding<TimeOffRequest>(Frame.OutlineColorProperty, timeOffRequest => timeOffRequest.BorderColor);
      StatusLabel.SetBinding<TimeOffRequest>(Label.TextColorProperty, timeOffRequest => timeOffRequest.TextColor);
    }
  }
}
