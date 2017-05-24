using System;
using Plugin.Iconize;

namespace Scheduleless.iOS.Fonts
{
	/// <summary>
	/// Defines the <see cref="IconicModule" /> icon module.
	/// </summary>
	/// <seealso cref="Plugin.Iconize.IconModule" />
	public sealed class IconicModule : IconModule
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IconicModule" /> class.
		/// </summary>
		public IconicModule()
			: base("Iconic", "Iconic", "open-iconic.ttf", IconicCollection.Icons)
		{
			// Intentionally left blank
		}
	}
}
