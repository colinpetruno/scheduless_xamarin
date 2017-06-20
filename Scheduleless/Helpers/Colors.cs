using System;
using Xamarin.Forms;

namespace Scheduleless.Helpers
{
	public static class Colors
	{
		// General
		public static readonly Color BackgroundColor = Color.FromHex("EBF0F8");
		public static readonly Color LightGray = Color.FromHex("C7C7CD");
		public static readonly Color DarkGray = Color.FromHex("3d3d3d");

		// Navigation #5f97ff;
		public static readonly Color NavigationBarColor = (Device.OS == TargetPlatform.iOS) ? Color.FromHex("5f97ff") : BackgroundColor;
		public static readonly Color NavigationBarTextColor = (Device.OS == TargetPlatform.iOS) ? Color.White : Color.FromHex("747c85");

		// Buttons and Input Fields
		public static readonly Color ButtonBackgroundColor = Color.FromHex("A4C7F0");
		public static readonly Color ButtonTextColor = Color.White;
		public static readonly Color EntryBackgroundColor = Color.Transparent;
		public static readonly Color EntryTextColor = Color.White;
		public static readonly Color EntryPlaceholderTextColor = Color.White;
		public static readonly Color SeparatorColor = Color.White;

	}
}
