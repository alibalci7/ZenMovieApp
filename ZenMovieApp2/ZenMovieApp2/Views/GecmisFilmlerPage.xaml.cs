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
    public partial class GecmisFilmlerPage : ContentPage
    {
        List<Film> filmler;
        List<IzlenenFilmler> izlenenfilmler;
        SQLiteManager sQLiteManager = new SQLiteManager();
        VeriCekme veriCekme = new VeriCekme();

        int sayac = 10;

        public GecmisFilmlerPage()
        {
            InitializeComponent();
            Title = "İzlenen Filmler";
            BackgroundColor = Color.Black;

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(255, 216, 14);
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromRgb(30, 30, 35);

            lstFilmler.ItemAppearing += ListView_ItemAppearing;
            this.Appearing += GecmisFilmlerPage_Appearing;

        }

        private void GecmisFilmlerPage_Appearing(object sender, EventArgs e)
        {
            sayac = 10;
            sayfa.IsVisible = false;
            filmler = new List<Film>();
            lstFilmler.BindingContext = filmler;
            FormLoad();
        }

        private async void FormLoad()
        {
            izlenenfilmler = sQLiteManager.GetAllIzlenenFilmler().ToList();

            if (izlenenfilmler.Count > 0)
            {
                yukleme.IsRunning = true;
                yukleme.IsVisible = true;
                for (int i = 0; i < 10; i++)
                {
                    if (i == izlenenfilmler.Count)
                        break;
                    filmler.Add(await veriCekme.FilmYukle(izlenenfilmler[i].FilmID));
                }
            }
            lstFilmler.BindingContext = filmler;
            yukleme.IsRunning = false;
            yukleme.IsVisible = false;
            sayfa.IsVisible = true;

            //lstIzlenen.BindingContext = (from film in AnaSayfa.filmler
            //                             join f in izlenenfilmler on film.FilmID equals f.FilmID
            //                             select film).ToList();


            //List<OylananFilmler> oylanan = sQLiteManager.GetAllFilmlerBegeni().ToList();
            //lstBegenilen.BindingContext = (from film in AnaSayfa.filmler
            //                               join f in oylanan.Where(x => x.Begeni == 1) on film.FilmID equals f.FilmID
            //                               select film).ToList();

            //lstBegenilmeyen.BindingContext = (from film in AnaSayfa.filmler
            //                                  join f in oylanan.Where(x => x.Begeni == 0) on film.FilmID equals f.FilmID
            //                                  select film).ToList();
        }

        async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var item = (Film)e.Item;
            if (item == filmler.Last() && izlenenfilmler.Count > 10)
            {
                doldur.IsVisible = true;
                doldur.IsRunning = true;
                sayfa.IsEnabled = false;
                for (int i = sayac; i < sayac + 10; i++)
                {
                    if (sayac == izlenenfilmler.Count)
                        break;
                    Film gelen = await veriCekme.FilmYukle(izlenenfilmler[sayac].FilmID);
                    if (gelen != null)
                    {
                        filmler.Add(gelen);
                        lstFilmler.BindingContext = null;
                        lstFilmler.BindingContext = filmler;
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

        private void OnFilmGit(object sender, EventArgs e)
        {
            ImageButton Ibtn = (ImageButton)sender;

            Navigation.PushAsync(new FilmPage(Convert.ToInt32(Ibtn.CommandParameter.ToString())));
        }
    }
}