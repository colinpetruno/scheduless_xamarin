using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Scheduleless.Localization
{
	[ContentProperty("Text")]
	public class TranslateExtension : IMarkupExtension
	{
		public string Text { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (Text == null)
			{
				return "";
			}

			return i18n.Instance.Translate(Text);
		}
	}
}
