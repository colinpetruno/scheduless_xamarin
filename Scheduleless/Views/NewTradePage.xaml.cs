using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Scheduleless.Localization;
using System.Collections.Generic;
using Scheduleless.Models;
using Scheduleless.ViewModels;
using Xamarin.Forms;

namespace Scheduleless.Views
{
    public partial class NewTradePage : NewTradePageXaml
    {


        public NewTradePage(FutureShift shift)
        {
            ViewModel.Shift = shift;
            InitializeComponent();
        }
    }

    public partial class NewTradePageXaml : BaseContentPage<NewTradeViewModel> { }
}
