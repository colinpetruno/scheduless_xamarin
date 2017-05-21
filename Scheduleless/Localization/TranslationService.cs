using System.Globalization;

namespace Scheduleless.Localization
{
	public interface ITranslationService
	{
		string Translate(string key, CultureInfo cultureInfo = null);
	}

	public class TranslationService : ITranslationService
	{
		public string Translate(string key, CultureInfo cultureInfo)
		{
			return i18n.Instance.Translate(key, cultureInfo);
		}
	}
}
