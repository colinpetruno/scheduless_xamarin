using System;
using Scheduleless.Models;
using Xamarin.Forms;

namespace Scheduleless.Controls
{
	public class FutureShiftCell : ViewCell
	{
		Label NameLabel;
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
				FontSize = 16,
				TextColor = Color.Black,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.Center
			};

			MainLayout = new StackLayout
			{
				Children = { NameLabel },
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(10, 0, 10, 0)
			};

			View = MainLayout;
		}

		private void SetupBindings()
		{
			// FIXME: resolve this warning
			NameLabel.SetBinding<FutureShift>(Label.TextProperty, futureShift => futureShift.Id);
		}
	}
}
