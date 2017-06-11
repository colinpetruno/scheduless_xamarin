using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Endpoints;
using Scheduleless.Models;
using Scheduleless.Services;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
    public class FutureShiftDetailViewModel : BaseViewModel
    {

        private FutureShift _futureShift;
        public FutureShift FutureShift
        {
            get { return _futureShift; }
            set { SetProperty(ref _futureShift, value); }
        }

        public string Day
        {
            get { return FutureShift.Day; }
        }

        public string Month
        {
            get { return FutureShift.ShortMonth; }
        }

        public string Label
        {
            get { return FutureShift.Label; }
        }

        public string LocationName
        {
            get { return FutureShift.LocationName; }
        }

        public string LocationLine1
        {
            get { return FutureShift.LocationLine1; }
        }

        public string LocationLine2
        {
            get { return FutureShift.LocationLine2; }
        }

        public string LocationCityStateZip
        {
            get { return FutureShift.CityStateZip; }
        }


        public string Address
        {
            get
            {
                string[] addressLines = new string[4];
                addressLines[0] = FutureShift.LocationName;
                addressLines[1] = FutureShift.LocationLine1;
                addressLines[2] = FutureShift.LocationLine2;
                addressLines[3] = FutureShift.CityStateZip;

                // TODO: There should be a better way than this
                var temp = new List<string>();
                foreach (var s in addressLines)
                {
                    if (!string.IsNullOrEmpty(s))
                        temp.Add(s);
                }
                addressLines = temp.ToArray();

                return String.Join(System.Environment.NewLine, addressLines);
            }
        }


        // FIXME HALP
        //        Command _TapTradeShiftButton;
        //        public Command TapTradeShiftButton
        //        {
        //            get { 
        //                var page = new FutureShiftDetailPage(futureShift);
        //                Navigation.PushAsync(page);
        //            }
        //        }
    }
}
