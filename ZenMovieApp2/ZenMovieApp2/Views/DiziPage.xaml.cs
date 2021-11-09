using MarcTron.Plugin;
using Plugin.Share;
using System;
using System.Collections.Generic;
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
    public partial class DiziPage : ContentPage
    {
        List<string> lstSezonlar;
        List<Bolum> bolumler = new List<Bolum>();

        ServiceManager serviceManager = new ServiceManager();
        SQLiteManager sQLiteManager = new SQLiteManager();
        OylananDiziler oylananDiziler = new OylananDiziler();
        FavoriDiziler favoriDizi = new FavoriDiziler();
        VeriCekme veriCekme = new VeriCekme();
        Dizi dizi = new Dizi();

        int diziid;
        bool oylanandizi, favoridizi;
        string url = "";
        public DiziPage(int id)
        {
            InitializeComponent();

            Reklam.ReklamDoldur();

            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(255, 216, 14);
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;

            DependencyService.Get<IStatusBar>().HideStatusBar();

            BackgroundColor = Color.Black;

            diziid = id;
            oylanandizi = sQLiteManager.GetDiziBegeniKontrol(diziid);

            if (oylanandizi == true)
            {
                oylananDiziler = sQLiteManager.GetDiziBegeni(diziid);
                if (oylananDiziler.Begeni == 1)
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

            favoridizi = sQLiteManager.GetFavoriDizilerKontrol(diziid);
            if (favoridizi == true)
            {
                ımgBtnFavRed.IsVisible = true;
                ımgBtnFavWhite.IsVisible = false;
            }

            this.Disappearing += DiziPage_Disappearing;
            CrossMTAdmob.Current.OnInterstitialClosed += Current_OnInterstitialClosed;

            FormLoad(id);
        }

        private void DiziPage_Disappearing(object sender, EventArgs e)
        {
            DependencyService.Get<IStatusBar>().ShowStatusBar();
        }

        private async void BolumleriYukle()
        {
            bolumler = await serviceManager.GetBolumler(diziid);

            foreach (Bolum item in bolumler)
            {
                item.DiziResim = dizi.DiziKapakFoto;
            }

            lstSezonlar = bolumler.Select(x => x.DiziBolumSezonNo).Distinct().ToList();
            pickerSezonlar.ItemsSource = lstSezonlar;

            lstBolumler.BindingContext = bolumler.Where(x => x.DiziBolumSezonNo == "1").ToList();
        }
        private async void FormLoad(int id)
        {
            ımgKapakFoto.WidthRequest = Application.Current.MainPage.Width;
            ımgKapakFoto.HeightRequest = (Application.Current.MainPage.Height / 3) * 2;

            if (VeriCekme.diziler.Any(x => x.DiziID == id))
            {
                dizi = VeriCekme.diziler.FirstOrDefault(X => X.DiziID == id);
            }
            else
            {
                dizi = await veriCekme.DiziYukle(id);
            }           

            ımgKapakFoto.BindingContext = dizi;
            Title = dizi.DiziBaslik;
            lbIMDB.Text = dizi.DiziIMDB;
            lbIzlenme.Text = dizi.DiziIzlenmeSayisi.ToString();
            lbKonu.Text = dizi.DiziKonu;
            lbOyuncu.Text = dizi.DiziOyuncular;
            lbYonetmen.Text = dizi.DiziYonetmenler;
            lbYıl.Text = dizi.DiziBaslangicYili.ToString();

            BolumleriYukle();
        }

        private async void OnLikeGonder(object sender, EventArgs e)
        {
            oylanandizi = sQLiteManager.GetDiziBegeniKontrol(diziid);
            if (oylanandizi == true)
            {                
                try
                {
                    string yol = "Dizi/GetDiziBegeniGuncelle?id=" + diziid + "&begeni=" + 1;
                    bool b = await serviceManager.BegeniInsert(yol);
                    Vibration.Vibrate();
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Feature not supported on device
                }
                oylananDiziler.DiziID = diziid;
                oylananDiziler.Begeni = 1;
                sQLiteManager.DiziBegeniInsert(oylananDiziler);
            }
            else
            {                
                try
                {
                    string yol = "Dizi/GetDiziBegeniGuncelle?id=" + diziid + "&begeni=" + 1;
                    bool b = await serviceManager.BegeniInsert(yol);
                    Vibration.Vibrate();
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Feature not supported on device
                }
                oylananDiziler.DiziID = diziid;
                oylananDiziler.Begeni = 1;
                sQLiteManager.DiziBegeniUpdate(oylananDiziler);
            }

            ımgBtnLikeGreen.IsVisible = true;
            ımgBtnLikeGray.IsVisible = false;
            ımgBtnDislikeRed.IsVisible = false;
            ımgBtnDislikeGray.IsVisible = true;
        }

        private async void OnDislikeGonder(object sender, EventArgs e)
        {
            oylanandizi = sQLiteManager.GetDiziBegeniKontrol(diziid);
            if (oylanandizi == true)
            {               
                try
                {
                    string yol = "Dizi/GetDiziBegeniGuncelle?id=" + diziid + "&begeni=" + 0;
                    bool b = await serviceManager.BegeniInsert(yol);
                    Vibration.Vibrate();
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Feature not supported on device
                }
                oylananDiziler.DiziID = diziid;
                oylananDiziler.Begeni = 0;
                sQLiteManager.DiziBegeniInsert(oylananDiziler);
            }
            else
            {              
                try
                {
                    string yol = "Dizi/GetDiziBegeniGuncelle?id=" + diziid + "&begeni=" + 0;
                    bool b = await serviceManager.BegeniInsert(yol);
                    Vibration.Vibrate();
                }
                catch (FeatureNotSupportedException ex)
                {
                    // Feature not supported on device
                }
                oylananDiziler.DiziID = diziid;
                oylananDiziler.Begeni = 0;
                sQLiteManager.DiziBegeniUpdate(oylananDiziler);
            }

            ımgBtnLikeGreen.IsVisible = false;
            ımgBtnLikeGray.IsVisible = true;
            ımgBtnDislikeRed.IsVisible = true;
            ımgBtnDislikeGray.IsVisible = false;

        }

        private void OnFavFilmSec(object sender, EventArgs e)
        {
            favoriDizi = new FavoriDiziler();
            favoriDizi.DiziID = diziid;
            sQLiteManager.FavoriDizilerInsert(favoriDizi);

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
            favoriDizi = sQLiteManager.GetFavoriDizi(diziid);
            sQLiteManager.FavoriDiziDelete(favoriDizi.ID);

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
                Text = dizi.DiziBaslik + " dizisini izlemek için TIKLA",
                Title = "ZenMovie Film ve Dizi İzleme",
                Url = ""
            });
        }

        private void pickerSezonlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker pck = (Picker)sender;
            lstBolumler.BindingContext = bolumler.Where(x => x.DiziBolumSezonNo == pck.SelectedItem.ToString());
        }

        private void OnIzle(object sender, EventArgs e)
        {
            ImageButton ımgbtn = (ImageButton)sender;
            url = ımgbtn.CommandParameter.ToString();
            if (CrossMTAdmob.Current.IsInterstitialLoaded())
            {
                Reklam.ReklamGoster();
            }
            else
            {
                Navigation.PushModalAsync(new VideoPage(url, "dizi", diziid));
            }
        }

        private void Current_OnInterstitialClosed(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new VideoPage(url, "dizi", diziid));
        }

    }
}