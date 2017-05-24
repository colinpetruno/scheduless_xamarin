using Plugin.Iconize;

namespace Scheduleless.Droid.Fonts
{
	/// <summary>
	/// Defines the <see cref="FontAwesomeModule" /> icon module.
	/// </summary>
	/// <seealso cref="Plugin.Iconize.IconModule" />
	public sealed class FontAwesomeModule : IconModule
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FontAwesomeModule" /> class.
		/// </summary>
		public FontAwesomeModule()
			: base("FontAwesome", "FontAwesome", "fontawesome.ttf", FontAwesomeCollection.Icons)
		{
			// Intentionally left blank
		}
	}
}