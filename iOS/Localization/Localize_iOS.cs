using System.Diagnostics;
using System.Globalization;
using Scheduleless.Localization;
using Foundation;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency(typeof(Scheduleless.iOS.Localization.Localize_iOS))]
namespace Scheduleless.iOS.Localization
{
	public class Localize_iOS : ILocalize
	{
		public CultureInfo GetCurrentCultureInfo()
		{
			var netLanguage = "en";
			var prefLanguageOnly = "en";
			if (NSLocale.PreferredLanguages.Length > 0)
			{
				var pref = NSLocale.PreferredLanguages[0];
				prefLanguageOnly = pref.Substring(0, 2);
				if (prefLanguageOnly == "pt")
				{
					if (pref == "pt")
						pref = "pt-BR"; // get the correct Brazilian language strings from the PCL RESX (note the local iOS folder is still "pt")
					else
						pref = "pt-PT"; // Portugal
				}
				netLanguage = pref.Replace("_", "-");
				Debug.WriteLine("preferred language:" + netLanguage);
			}
			CultureInfo ci = null;
			try
			{
				ci = new CultureInfo(netLanguage);
			}
			catch
			{
				// iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
				// fallback to first characters, in this case "en"
				ci = new CultureInfo(prefLanguageOnly);
			}

			return ci;
		}

		public void SetLocale()
		{
			var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
			var netLocale = iosLocaleAuto.Replace("_", "-");

			CultureInfo ci;

			try
			{
				ci = new CultureInfo(netLocale);
			}
			catch
			{
				ci = GetCurrentCultureInfo();
			}

			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;
		}
	}
}
