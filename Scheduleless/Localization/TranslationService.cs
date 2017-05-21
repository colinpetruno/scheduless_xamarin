using System;
using System.Globalization;

namespace Scheduleless.Localization
{
	public interface ITranslationService
	{
		string Translate(string key, CultureInfo cultureInfo = null);
	}

	public class TranslationService : ITranslationService
	{
		private static readonly Lazy<TranslationService> lazy = new Lazy<TranslationService>(() => new TranslationService());
		public static TranslationService Instance
		{
			get
			{
				return lazy.Value;
			}
		}

		public string Translate(string key, CultureInfo cultureInfo = null)
		{
			return i18n.Instance.Translate(key, cultureInfo);
		}

		/// <summary>
		/// Convenience method
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="cultureInfo">Culture info.</param>
		public static string Localize(string key, CultureInfo cultureInfo = null)
		{
			return Instance.Translate(key, cultureInfo);
		}
	}
}
