using Rg.Plugins.Popup.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMovieApp2.Models;
using ZenMovieApp2.Service;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmlerPage : ContentPage
    {
        List<Film> secilifilmler = new List<Film>();
        VeriCekme veriCekme = new VeriCekme();

        public int yilmin = 1900, yilmax = DateTime.Now.Year, kategoriID = 0;
        public float imdbmin = 1, imdbmax = 10;
        public bool altyazimi = false;

        public FilmlerPage()
        {
            InitializeComponent();
            Title = "FİLMLER";
            BackgroundColor = Color.Black;
            //NavigationPage.SetHasBackButton(this, false);
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(255, 216, 14);
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromRgb(30, 30, 35);

            lstFilmler.ItemAppearing += ListView_ItemAppearing;
            this.Appearing += FilmlerPage_Appearing;
        }

        private void FilmlerPage_Appearing(object sender, EventArgs e)
        {
            VeriCekme.filmler = null;
            doldur.IsVisible = false;
            doldur.IsRunning = false;
            FormLoad(yilmin, yilmax, imdbmin, imdbmax);
        }

        public async void FormLoad(int yilmin, int yilmax, float imdbmin, float imdbmax)
        {
            yukleme.IsRunning = true;
            yukleme.IsVisible = true;
            VeriCekme.filmler = await veriCekme.FilmleriYukle(imdbmin, imdbmax, yilmin, yilmax);
            secilifilmler = VeriCekme.filmler;
            lstFilmler.BindingContext = secilifilmler;
            yukleme.IsRunning = false;
            yukleme.IsVisible = false;
            sayfa.IsVisible = true;
        }

        public async void Filtrele(int yilmin, int yilmax, int kategoriID, float imdbmin, float imdbmax, bool altyazimi = false)
        {
            this.yilmin = yilmin;
            this.yilmax = yilmax;
            this.imdbmax = imdbmax;
            this.imdbmin = imdbmin;
            this.kategoriID = kategoriID;
            this.altyazimi = altyazimi;
            if (kategoriID != 0)
            {
                VeriCekme.filmler = await veriCekme.FilmleriYukle(imdbmin, imdbmax, yilmin, yilmax,kategoriID);                
                //List<FilmKategori> secilenkategoriler = AnaSayfa.filmKategoriler.Where(x => x.KategoriID == Convert.ToInt32(kategoriID)).ToList();
                //List<Film> sfilmler = AnaSayfa.filmler.Where(x => x.FilmYapimYili >= Convert.ToInt32(yilmin) && x.FilmYapimYili <= Convert.ToInt32(yilmax) && x.IMDB >= Convert.ToSingle(imdbmin) && x.IMDB <= Convert.ToSingle(imdbmax) && x.FilmAltYazi == altyazimi).ToList();
                //secilifilmler = (from film in sfilmler
                //                 join secilen in secilenkategoriler on film.FilmID equals secilen.FilmID
                //                 select film).ToList();
            }
            else
            {
                VeriCekme.filmler = await veriCekme.FilmleriYukle(imdbmin, imdbmax, yilmin, yilmax, 0);
                //secilifilmler = AnaSayfa.filmler.Where(x => x.IMDB >= Convert.ToSingle(imdbmin) && x.IMDB <= Convert.ToSingle(imdbmax) && x.FilmYapimYili >= Convert.ToInt32(yilmin) && x.FilmYapimYili <= Convert.ToInt32(yilmax) && x.FilmAltYazi == altyazimi).ToList();
            }
            secilifilmler = VeriCekme.filmler.Where(x => x.FilmAltYazi == altyazimi).ToList();
            lstFilmler.BindingContext = null;
            lstFilmler.BindingContext = secilifilmler;
        }

        async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var item = (Film)e.Item;
            if (item == VeriCekme.filmler.Last() && VeriCekme.filmler.Count >=10)
            {
                doldur.IsVisible = true;
                doldur.IsRunning = true;
                List<Film> gelen = await veriCekme.FilmleriYukle(imdbmin, imdbmax, yilmin, yilmax, kategoriID, item.FilmID, 10);
                if(gelen.Count > 0)
                {
                    VeriCekme.filmler.AddRange(gelen);
                    secilifilmler = VeriCekme.filmler.Where(x => x.FilmAltYazi == altyazimi).ToList();
                    lstFilmler.BindingContext = null;
                    lstFilmler.BindingContext = secilifilmler;
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

        private void OnRefreshed(object sender, EventArgs e)
        {
            FormLoad(yilmin, yilmax, imdbmin, imdbmax);
            lstFilmler.IsRefreshing = false;
        }

        private async void OnFiltreGit(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new FiltrePage(true, this, null));
        }

        private async void OnSiralamayaGit(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new SiralamaPage(true, this, null));
        }

        public void IsmeGoreBKSirala()
        {
            lstFilmler.BindingContext = secilifilmler.OrderByDescending(x => x.FilmBaslik).ToList();
        }

        public void IsmeGoreKBSirala()
        {
            lstFilmler.BindingContext = secilifilmler.OrderBy(x => x.FilmBaslik).ToList();
        }

        public void YilaGoreBKSirala()
        {
            lstFilmler.BindingContext = secilifilmler.OrderByDescending(x => x.FilmYapimYili).ToList();
        }

        public void YilaGoreKBSirala()
        {
            lstFilmler.BindingContext = secilifilmler.OrderBy(x => x.FilmYapimYili).ToList();
        }

        public void IMDBGoreBKSirala()
        {
            lstFilmler.BindingContext = secilifilmler.OrderByDescending(x => x.IMDB).ToList();
        }

        public void IMDBGoreKBSirala()
        {
            lstFilmler.BindingContext = secilifilmler.OrderBy(x => x.IMDB).ToList();
        }
    }
}