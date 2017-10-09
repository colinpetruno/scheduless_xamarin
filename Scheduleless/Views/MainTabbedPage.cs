using System;
using System.Linq;
using FormsPlugin.Iconize;
using Scheduleless.Fonts;
using Scheduleless.Localization;
using Scheduleless.Renderers;
using Scheduleless.Services;
using Xamarin.Forms;

namespace Scheduleless.Views
{
	public class MainTabbedPage : IconTabbedPage
	{
		private FeaturesService _featuresService;

		public MainTabbedPage()
		{
			_featuresService = FeaturesService.Instance;
		}

		/// <summary>
		/// Sets up the initial values for the tabbed pages.
		/// Add additional tabs below as needed.
		/// </summary>
		public void SetupTabbedPages()
		{
			this.Title = "TabbedPage";

			this.Children.Add(AddTabbedPage(
			  new ShiftsPage(),
			  TranslationService.Localize(LocalizationConstants.YourSchedule),
			  FontIcons.CalendarCheck));

			if (_featuresService.Trading)
			{
				this.Children.Add(AddTabbedPage(
				  new AvailableShiftsPage(),
				  TranslationService.Localize(LocalizationConstants.AvailableShifts),
				  FontIcons.CalendarPlus)
				);

				this.Children.Add(AddTabbedPage(
				  new MyTradesPage(),
				  TranslationService.Localize(LocalizationConstants.YourTrades),
				  FontIcons.MapSigns)
				);
			}

			this.Children.Add(AddTabbedPage(
			  new TimeOffIndexPage(),
			  TranslationService.Localize(LocalizationConstants.TimeOff),
			  FontIcons.HourGlass)
			);
		}

		/// <summary>
		/// Convenience method to build up tabbed pages with a navigation page in them.
		/// </summary>
		/// <returns>A tabbed page with a navigation page.</returns>
		/// <param name="page">Page.</param>
		/// <param name="title">Title.</param>
		/// <param name="iconName">Icon Name.</param>
		public static NavigationPage AddTabbedPage(ContentPage page, string title, string iconName)
		{
			var fontModule = Plugin.Iconize.Iconize.Modules.First();
			var icon = fontModule.GetIcon(iconName).Key;

			return new ThemedNavigationPage(page)
			{
				Title = title,
				BindingContext = new ModuleWrapper(fontModule),
				Icon = icon
			};
		}
	}
}
