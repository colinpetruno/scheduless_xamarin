using System;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using System.Globalization;

namespace Scheduleless.Localization
{
	/// <summary>
	/// Used to provide access to localization in <see cref="TranslateExtension"/>.
	/// </summary>
	public class i18n
	{
		private struct Constants
		{
			public const string ResourceId = "Scheduleless.Localization.LanguageResources";
		}

		private Lazy<ResourceManager> _resourceManager = new Lazy<ResourceManager>(
			() => new ResourceManager(Constants.ResourceId, typeof(i18n).GetTypeInfo().Assembly));

		// Currently, we don't have a way to inject anything into TranslateExtension, so we must
		// implement the i18n Singleton ourselves and reference it statically
		#region Singleton Implementation
		private static readonly Lazy<i18n> lazy = new Lazy<i18n>(() => new i18n());
		public static i18n Instance { get { return lazy.Value; } }

		private i18n() { }
		#endregion // Singleton Implementation

		public string Translate(string key, CultureInfo cultureInfo = null)
		{
			Debug.WriteLine("Localize " + key);

			// TODO: Figure out how to inject into TranslateExtension and then remove the DependencyService static reference
			cultureInfo = cultureInfo ?? DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

			string translation = _resourceManager.Value.GetString(key, cultureInfo);

			if (translation == null)
			{
#if DEBUG
				throw new ArgumentException($"Key '{key}' was not found in resources '{Constants.ResourceId}' for culture '{cultureInfo.Name}'.");
#else
				translation = key;
#endif
			}

			return translation;
		}
	}
}
