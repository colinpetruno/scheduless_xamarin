using System;
using System.Collections.Generic;
using Plugin.Iconize;

namespace Scheduleless.Droid.Fonts
{
	/// <summary>
	/// Defines the <see cref="IconicCollection" /> icon collection.
	/// </summary>
	public static class IconicCollection
	{
		/// <summary>
		/// Gets the icons.
		/// </summary>
		/// <value>
		/// The icons.
		/// </value>
		public static IList<IIcon> Icons { get; } = new List<IIcon>();

		/// <summary>
		/// Initializes the <see cref="IconicCollection" /> class.
		/// </summary>
		static IconicCollection()
		{
			Icons.Add("oi-signpost", '\ue0be');
		}
	}
}
