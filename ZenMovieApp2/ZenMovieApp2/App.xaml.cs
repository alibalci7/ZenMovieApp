using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMovieApp2.Views;

namespace ZenMovieApp2
{
    public partial class App : Application
    {
        public static string DBName { get; set; } = "DataBase.db3";
        public App()
        {
            InitializeComponent();

            MainPage = new YukleniyorPage(this);
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
