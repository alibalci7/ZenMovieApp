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
    public partial class AramaFilmPage : ContentPage
    {
        List<Film> arananfilmler;
        VeriCekme veriCekme = new VeriCekme();

        string ad;

        public AramaFilmPage(string arama)
        {
            InitializeComponent();
            BackgroundColor = Color.Black;
            ad = arama;

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(255, 216, 14);
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromRgb(30, 30, 35);

            lstFilmler.ItemAppearing += ListView_ItemAppearing;
            this.Appearing += AramaFilmPage_Appearing;

        }

        private void AramaFilmPage_Appearing(object sender, EventArgs e)
        {
            sayfa.IsVisible = false;
            arananfilmler = new List<Film>();
            lstFilmler.BindingContext = arananfilmler;
            FormLoad();
        }

        private async void FormLoad()
        {
            yukleme.IsRunning = true;
            yukleme.IsVisible = true;
            arananfilmler = await veriCekme.FilmleriAra(ad);
            lstFilmler.BindingContext = arananfilmler;
            yukleme.IsRunning = false;
            yukleme.IsVisible = false;
            sayfa.IsVisible = true;
            //lstFilmler.BindingContext = VeriCekme.filmler.Where(x => x.FilmBaslik.Contains(a)).OrderBy(x => x.FilmBaslik).ToList();
        }

        async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var item = (Film)e.Item;
            if (item == arananfilmler.Last() && arananfilmler.Count >= 10)
            {
                doldur.IsVisible = true;
                doldur.IsRunning = true;
                List<Film> gelen = await veriCekme.FilmleriAra(ad,item.FilmID);
                if (gelen.Count > 0)
                {
                    arananfilmler.AddRange(gelen);
                    lstFilmler.BindingContext = null;
                    lstFilmler.BindingContext = arananfilmler;
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

        private void OnFilmGit(object sender, EventArgs e)
        {
            ImageButton Ibtn = (ImageButton)sender;
            Navigation.PushAsync(new FilmPage(Convert.ToInt32(Ibtn.CommandParameter.ToString())));
        }
    }
}