using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMovieApp2.Models;
using ZenMovieApp2.Service;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AramaDiziPage : ContentPage
    {
        List<Dizi> aranandiziler;
        VeriCekme veriCekme = new VeriCekme();

        string ad;

        public AramaDiziPage(string arama)
        {
            InitializeComponent();
            BackgroundColor = Color.Black;
            ad = arama;

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(255, 216, 14);
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromRgb(30, 30, 35);

            lstDiziler.ItemAppearing += ListView_ItemAppearing;
            this.Appearing += AramaDiziPage_Appearing;

        }

        private void AramaDiziPage_Appearing(object sender, EventArgs e)
        {
            sayfa.IsVisible = false;
            aranandiziler = new List<Dizi>();
            lstDiziler.BindingContext = aranandiziler;
            FormLoad();
        }

        private async void FormLoad()
        {
            yukleme.IsRunning = true;
            yukleme.IsVisible = true;
            aranandiziler = await veriCekme.DizileriAra(ad);
            lstDiziler.BindingContext = aranandiziler;
            yukleme.IsRunning = false;
            yukleme.IsVisible = false;
            sayfa.IsVisible = true;
            //lstDiziler.BindingContext = VeriCekme.diziler.Where(x => x.DiziBaslik.Contains(a)).OrderBy(x => x.DiziBaslik).ToList();
        }

        async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var item = (Dizi)e.Item;
            if (item == aranandiziler.Last() && aranandiziler.Count >= 10)
            {
                doldur.IsVisible = true;
                doldur.IsRunning = true;
                List<Dizi> gelen = await veriCekme.DizileriAra(ad, item.DiziID);
                if (gelen.Count > 0)
                {
                    aranandiziler.AddRange(gelen);
                    lstDiziler.BindingContext = null;
                    lstDiziler.BindingContext = aranandiziler;
                }
                doldur.IsVisible = false;
                doldur.IsRunning = false;
            }
        }

        private void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            ListView listView = (ListView)sender;
            listView.SelectedItem = null;

        }

        private void OnDiziGit(object sender, EventArgs e)
        {
            ImageButton Ibtn = (ImageButton)sender;
            Navigation.PushAsync(new DiziPage(Convert.ToInt32(Ibtn.CommandParameter.ToString())));
        }
    }
}