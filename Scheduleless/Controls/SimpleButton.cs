using System;
using Xamarin.Forms;

namespace Scheduleless.Controls
{
	public class SimpleButton : Button
	{
		public SimpleButton() : base()
		{
			const int _animationTime = 100;
			Clicked += async (sender, e) =>
			{
				var btn = (SimpleButton)sender;
				await btn.ScaleTo(1.2, _animationTime);
				btn.ScaleTo(1, _animationTime);
			};
		}
	}
}
