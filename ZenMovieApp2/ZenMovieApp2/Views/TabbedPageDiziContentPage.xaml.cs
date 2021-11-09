using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMovieApp2.Service;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPageDiziContentPage : ContentPage
    {
        VeriCekme veriCekme = new VeriCekme();
        public TabbedPageDiziContentPage()
        {
            InitializeComponent();
            Title = "Diziler";
            BackgroundColor = Color.Black;
            FormLoad();
        }

        void FormLoad()
        {
            this.Appearing += TabbedPageDiziContentPage_Appearing;
        }

        private async void TabbedPageDiziContentPage_Appearing(object sender, EventArgs e)
        {
            if (VeriCekme.izlenendiziler != null)
            {
                yukleme.IsRunning = false;
                yukleme.IsVisible = false;
                sayfa.IsVisible = true;
            }
            lstIzlenen.BindingContext = VeriCekme.izlenendiziler;
            lstBegenilen.BindingContext = VeriCekme.begenilendiziler;
            lstYuklenen.BindingContext = VeriCekme.yuklenendiziler;
        }

        private void OnDiziGit(object sender, EventArgs e)
        {
            ImageButton Ibtn = (ImageButton)sender;

            Navigation.PushAsync(new DiziPage(Convert.ToInt32(Ibtn.CommandParameter.ToString())));
        }
    }
}