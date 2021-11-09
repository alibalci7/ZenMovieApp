using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using ZenMovieApp2.Helper;
using ZenMovieApp2.iOS.Helper;

[assembly: Xamarin.Forms.Dependency(typeof(StatusBarImplementation))]
namespace ZenMovieApp2.iOS.Helper
{
    public class StatusBarImplementation : IStatusBar
    {
        public void HideStatusBar()
        {
            UIApplication.SharedApplication.StatusBarHidden = true;
        }
        public void ShowStatusBar()
        {
            UIApplication.SharedApplication.StatusBarHidden = false;
        }

    }
}