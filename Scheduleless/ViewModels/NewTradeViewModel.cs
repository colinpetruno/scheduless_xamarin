using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scheduleless.Endpoints;
using Scheduleless.Models;
using Scheduleless.Services;
using Xamarin.Forms;

namespace Scheduleless.ViewModels
{
	public class NewTradeViewModel : BaseViewModel
	{
        public FutureShift Shift { get; set; }	
	}
}
