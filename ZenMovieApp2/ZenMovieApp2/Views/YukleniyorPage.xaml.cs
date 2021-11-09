using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMovieApp2.Service;
using ZenMovieApp2.ViewModels;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YukleniyorPage : ContentPage
    {
        App anaSayfa;
        TumSiniflar tumSiniflar = new TumSiniflar();
        ServiceManager serviceManager = new ServiceManager();
        VeriCekme veriCekme = new VeriCekme();
        public YukleniyorPage(App a)
        {
            InitializeComponent();

            Title = " ";
            BackgroundColor = Color.Black;
            anaSayfa = a;

            KategorileriYukle();
        }

        private async void KategorileriYukle()
        {
            VeriCekme.kategoriler = await serviceManager.GetKategoriler();
            //Navigation.PushAsync(new DetailPage());
            anaSayfa.MainPage = new NavigationPage(new AnaSayfa());
        }

        //private async void FilmleriYukle()
        //{
        //    tumSiniflar = await serviceManager.GetAll("Film/GetFilmleriListele", 1);
        //    AnaSayfa.filmler = tumSiniflar.filmler;

        //    foreach (var item in AnaSayfa.filmler)
        //    {
        //        item.FilmIMDB = item.FilmIMDB.Replace(',', '.');
        //        item.IMDB = Convert.ToSingle(item.FilmIMDB.Replace(',', '.'));
        //        if (item.IMDB > 10)
        //        {
        //            item.IMDB = item.IMDB / 10;
        //        }
        //    }

        //    DizileriYukle();
        //}

        //private async void DizileriYukle()
        //{
        //    tumSiniflar = await serviceManager.GetAll("Dizi/GetDizileriListele", 2);
        //    AnaSayfa.diziler = tumSiniflar.diziler;
        //    foreach (var item in AnaSayfa.diziler)
        //    {
        //        item.DiziIMDB = item.DiziIMDB.Replace(',', '.');
        //        item.IMDB = Convert.ToSingle(item.DiziIMDB.Replace(',', '.'));
        //        if (item.IMDB > 10)
        //        {
        //            item.IMDB = item.IMDB / 10;
        //        }
        //    }

        //    KategorileriYukle();
        //}

        //private async void BolumlerlerveKategorilerYukle()
        //{
        //    AnaSayfa.bolumler = await serviceManager.GetBolumler();
        //    AnaSayfa.kategoriler = await serviceManager.GetKategoriler();
        //    AnaSayfa.filmKategoriler = await serviceManager.GetFilmKategoriler();
        //    //Navigation.PushAsync(new DetailPage());
        //    anaSayfa.MainPage = new NavigationPage(new AnaSayfa());
        //}
    }
}