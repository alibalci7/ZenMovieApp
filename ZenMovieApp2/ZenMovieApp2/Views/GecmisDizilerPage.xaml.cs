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
    public partial class GecmisDizilerPage : ContentPage
    {
        List<Dizi> diziler;
        List<IzlenenDiziler> izlenendiziler;
        SQLiteManager sQLiteManager = new SQLiteManager();
        VeriCekme veriCekme = new VeriCekme();

        int sayac = 10;

        public GecmisDizilerPage()
        {
            InitializeComponent();
            Title = "İzlenen Diziler";
            BackgroundColor = Color.Black;

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(255, 216, 14);
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromRgb(30, 30, 35);

            lstDiziler.ItemAppearing += ListView_ItemAppearing;
            this.Appearing += GecmisDizilerPage_Appearing;

        }

        private void GecmisDizilerPage_Appearing(object sender, EventArgs e)
        {
            sayac = 10;
            sayfa.IsVisible = false;
            diziler = new List<Dizi>();
            lstDiziler.BindingContext = diziler;
            FormLoad();
        }

        private async void FormLoad()
        {
            izlenendiziler = sQLiteManager.GetAllIzlenenDiziler().ToList();

            if (izlenendiziler.Count > 0)
            {
                yukleme.IsRunning = true;
                yukleme.IsVisible = true;
                for (int i = 0; i < 10; i++)
                {
                    if (i == izlenendiziler.Count)
                        break;
                    diziler.Add(await veriCekme.DiziYukle(izlenendiziler[i].DiziID));
                }
            }
            lstDiziler.BindingContext = diziler;
            yukleme.IsRunning = false;
            yukleme.IsVisible = false;
            sayfa.IsVisible = true;

            //lstIzlenen.BindingContext = (from dizi in AnaSayfa.diziler
            //                             join d in izlenendiziler on dizi.DiziID equals d.DiziID
            //                             select dizi).ToList();


            //List<OylananDiziler> oylanan = sQLiteManager.GetAllDizilerBegeni().ToList();
            //lstBegenilen.BindingContext = (from dizi in AnaSayfa.diziler
            //                               join d in oylanan.Where(x => x.Begeni == 1) on dizi.DiziID equals d.DiziID
            //                               select dizi).ToList();

            //lstBegenilmeyen.BindingContext = (from dizi in AnaSayfa.diziler
            //                                  join d in oylanan.Where(x => x.Begeni == 0) on dizi.DiziID equals d.DiziID
            //                                  select dizi).ToList();
        }

        async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var item = (Dizi)e.Item;
            if (item == diziler.Last() && izlenendiziler.Count > 10)
            {
                doldur.IsVisible = true;
                doldur.IsRunning = true;
                sayfa.IsEnabled = false;
                for (int i = sayac; i < sayac + 10; i++)
                {
                    if (sayac == izlenendiziler.Count)
                        break;
                    Dizi gelen = await veriCekme.DiziYukle(izlenendiziler[sayac].DiziID);
                    if (gelen != null)
                    {
                        diziler.Add(gelen);
                        lstDiziler.BindingContext = null;
                        lstDiziler.BindingContext = diziler;
                    }
                    sayac++;
                }
                doldur.IsVisible = false;
                doldur.IsRunning = false;
                sayfa.IsEnabled = true;
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