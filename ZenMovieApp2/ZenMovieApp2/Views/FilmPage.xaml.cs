using MarcTron.Plugin;
using Plugin.Share;
using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMovieApp2.Helper;
using ZenMovieApp2.Models;
using ZenMovieApp2.Service;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmPage : ContentPage
    {
        ServiceManager serviceManager = new ServiceManager();
        SQLiteManager sQLiteManager = new SQLiteManager();
        OylananFilmler oylananFilmler = new OylananFilmler();
        FavoriFilmler favoriFilm;
        Film film = new Film();
        VeriCekme veriCekme = new VeriCekme();

        int filmid;
        bool oylananfilm;
        bool favorifilm;
        string url = "";
        public FilmPage(int id)
        {
            InitializeComponent();

            Reklam.ReklamDoldur();

            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(255, 216, 14);
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;

            DependencyService.Get<IStatusBar>().HideStatusBar();

            filmid = id;
            oylananfilm = sQLiteManager.GetFilmBegeniKontrol(filmid);
            if (oylananfilm == true)
            {
                oylananFilmler = sQLiteManager.GetFilmBegeni(filmid);
                if (oylananFilmler.Begeni == 1)
                {
                    ımgBtnLikeGreen.IsVisible = true;
                    ımgBtnLikeGray.IsVisible = false;
                    ımgBtnDislikeRed.IsVisible = false;
                    ımgBtnDislikeGray.IsVisible = true;
                }
                else
                {
                    ımgBtnLikeGreen.IsVisible = false;
                    ımgBtnLikeGray.IsVisible = true;
                    ımgBtnDislikeRed.IsVisible = true;
                    ımgBtnDislikeGray.IsVisible = false;
                }
            }

            favorifilm = sQLiteManager.GetFavoriFilmlerKontrol(filmid);
            if (favorifilm == true)
            {
                ımgBtnFavRed.IsVisible = true;
                ımgBtnFavWhite.IsVisible = false;
            }

            BackgroundColor = Color.Black;

            FormLoad(filmid);

            this.Disappearing += FilmPage_Disappearing;
            CrossMTAdmob.Current.OnInterstitialClosed += Current_OnInterstitialClosed;
            
        }

        private void FilmPage_Disappearing(object sender, EventArgs e)
        {
            DependencyService.Get<IStatusBar>().ShowStatusBar();
        }

        private async void FormLoad(int id)
        {
            ımgKapakFoto.WidthRequest = Application.Current.MainPage.Width;
            ımgKapakFoto.HeightRequest = (Application.Current.MainPage.Height / 3) * 2;

            if (VeriCekme.filmler.Any(x => x.FilmID == id))
            {
                film = VeriCekme.filmler.FirstOrDefault(x => x.FilmID == id);
            }
            else
            {
                film = await veriCekme.FilmYukle(id);
            }                

            Title = film.FilmBaslik;
            ımgKapakFoto.BindingContext = film;
            //lbAd.Text = film.FilmBaslik;
            lbIMDB.Text = film.FilmIMDB;
            lbIzlenme.Text = film.FilmIzlenmeSayisi.ToString();
            lbKat.Text = film.FilmKategoriler;
            lbKonu.Text = film.FilmKonu;
            lbOyuncu.Text = film.FilmOyuncular;
            lbSure.Text = film.FilmSure;
            lbYonetmen.Text = film.FilmYonetmenler;
            lbYıl.Text = film.FilmYapimYili.ToString();

            ımgbtn.BindingContext = film;
        }

        private async void OnLikeGonder(object sender, EventArgs e)
        {
            oylananfilm = sQLiteManager.GetFilmBegeniKontrol(filmid);
            if (oylananfilm == false)
            {              
                try
                {
                    string yol = "Film/GetFilmBegeniGonder?id=" + filmid + "&begeni=" + 1;
                    bool b = await serviceManager.BegeniInsert(yol);
                    Vibration.Vibrate();
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Feature not supported on device
                }
                oylananFilmler.FilmID = filmid;
                oylananFilmler.Begeni = 1;
                sQLiteManager.FilmBegeniInsert(oylananFilmler);
            }
            else
            {               
                try
                {
                    string yol = "Film/GetFilmBegeniGuncelle?id=" + filmid + "&begeni=" + 1;
                    bool b = await serviceManager.BegeniInsert(yol);
                    Vibration.Vibrate();
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Feature not supported on device
                }
                oylananFilmler.FilmID = filmid;
                oylananFilmler.Begeni = 1;
                sQLiteManager.FilmBegeniUpdate(oylananFilmler);
            }

            ımgBtnLikeGreen.IsVisible = true;
            ımgBtnLikeGray.IsVisible = false;
            ımgBtnDislikeRed.IsVisible = false;
            ımgBtnDislikeGray.IsVisible = true;

        }

        private async void OnDislikeGonder(object sender, EventArgs e)
        {
            oylananfilm = sQLiteManager.GetFilmBegeniKontrol(filmid);
            if (oylananfilm == false)
            {                
                try
                {
                    string yol = "Film/GetFilmBegeniGonder?id=" + filmid + "&begeni=" + 0;
                    bool b = await serviceManager.BegeniInsert(yol);
                    Vibration.Vibrate();
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Feature not supported on device
                }
                oylananFilmler.FilmID = filmid;
                oylananFilmler.Begeni = 0;
                sQLiteManager.FilmBegeniInsert(oylananFilmler);
            }
            else
            {               
                try
                {
                    string yol = "Film/GetFilmBegeniGuncelle?id=" + filmid + "&begeni=" + 0;
                    bool b = await serviceManager.BegeniInsert(yol);
                    Vibration.Vibrate();
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Feature not supported on device
                }
                oylananFilmler.FilmID = filmid;
                oylananFilmler.Begeni = 0;
                sQLiteManager.FilmBegeniUpdate(oylananFilmler);
            }

            ımgBtnLikeGreen.IsVisible = false;
            ımgBtnLikeGray.IsVisible = true;
            ımgBtnDislikeRed.IsVisible = true;
            ımgBtnDislikeGray.IsVisible = false;
        }

        private void OnFavFilmSec(object sender, EventArgs e)
        {
            favoriFilm = new FavoriFilmler();
            favoriFilm.FilmID = filmid;
            sQLiteManager.FavoriFilmlerInsert(favoriFilm);

            ımgBtnFavRed.IsVisible = true;
            ımgBtnFavWhite.IsVisible = false;

            try
            {
                Vibration.Vibrate();
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
            }
        }

        private void OnFavFilmCikar(object sender, EventArgs e)
        {
            favoriFilm = sQLiteManager.GetFavoriFilm(filmid);
            sQLiteManager.FavoriFilmDelete(favoriFilm.ID);

            ımgBtnFavRed.IsVisible = false;
            ımgBtnFavWhite.IsVisible = true;

            try
            {
                Vibration.Vibrate();
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
            }
        }

        private void OnShare(object sender, EventArgs e)
        {
            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                Text = film.FilmBaslik + " filmini izlemek için TIKLA",
                Title = "ZenMovie Film ve Dizi İzleme",
                Url = ""
            });
        }

        private void OnIzle(object sender, EventArgs e)
        {
            Button ımgbtn = (Button)sender;
            url = ımgbtn.CommandParameter.ToString();
            if (CrossMTAdmob.Current.IsInterstitialLoaded())
            {
                Reklam.ReklamGoster();
            }
            else
            {
                Navigation.PushModalAsync(new VideoPage(url, "film", filmid));
            }           
        }
        
        private void Current_OnInterstitialClosed(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new VideoPage(url, "film", filmid));
        }
    }
}