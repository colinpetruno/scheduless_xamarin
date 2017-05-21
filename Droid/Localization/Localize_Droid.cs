using System.Globalization;
using System.Threading;
using Scheduleless.Localization;
using Xamarin.Forms;

[assembly: Dependency(typeof(Scheduleless.Droid.Localization.Localize_Droid))]
namespace Scheduleless.Droid.Localization
{
	public class Localize_Droid : ILocalize
	{
		public CultureInfo GetCurrentCultureInfo()
		{
			var androidLocale = Java.Util.Locale.Default;
			var netLanguage = androidLocale.ToString().Replace("_", "-"); // turns pt_BR into pt-BR
			return new CultureInfo(netLanguage);
		}

		public void SetLocale()
		{
			var ci = GetCurrentCultureInfo();

			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;
		}
	}
}
