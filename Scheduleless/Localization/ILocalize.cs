using System.Globalization;

namespace Scheduleless.Localization
{
	public interface ILocalize
	{
		CultureInfo GetCurrentCultureInfo();

		void SetLocale();
	}
}
