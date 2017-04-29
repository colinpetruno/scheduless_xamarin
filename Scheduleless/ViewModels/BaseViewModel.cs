using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		private string _title = string.Empty;
		private string _subTitle = string.Empty;
		private string _icon = null;
		private bool isBusy;

		public const string TitlePropertyName = "Title";
		public const string IconPropertyName = "Icon";
		public const string IsBusyPropertyName = "IsBusy";

		public event PropertyChangingEventHandler PropertyChanging;
		public event PropertyChangedEventHandler PropertyChanged;

		public BaseViewModel() { }

		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public string Subtitle
		{
			get { return _subTitle; }
			set { SetProperty(ref _subTitle, value); }
		}

		public string Icon
		{
			get { return _icon; }
			set { SetProperty(ref _icon, value); }
		}

		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}

		protected void SetProperty<T>(
			ref T backingStore, T value,
			Action onChanged = null,
			Action<T> onChanging = null,
			[CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return;

			if (onChanging != null)
				onChanging(value);

			OnPropertyChanging(propertyName);

			backingStore = value;

			if (onChanged != null)
				onChanged();

			OnPropertyChanged(propertyName);
		}

		public void OnPropertyChanging(string propertyName)
		{
			if (PropertyChanging == null)
				return;

			PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
		}

		public void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
