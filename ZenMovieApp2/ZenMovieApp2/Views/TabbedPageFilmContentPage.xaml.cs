using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMovieApp2.Service;
using ZenMovieApp2.ViewModels;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPageFilmContentPage : ContentPage
    {
        public static bool yuklemebittimi = false;

        TumSiniflar tumSiniflar = new TumSiniflar();
        ServiceManager serviceManager = new ServiceManager();
        VeriCekme veriCekme = new VeriCekme();
        public TabbedPageFilmContentPage()
        {
            InitializeComponent();
            Title = "Filmler";
            BackgroundColor = Color.Black;

            FormLoad();
        }

        private async void FormLoad()
        {
            VeriCekme.izlenenfilmler = await veriCekme.IzlenenFilmleriYukle();
            VeriCekme.begenilenfilmler = await veriCekme.BegenilenFilmleriYukle();
            VeriCekme.yuklenenfilmler = await veriCekme.YuklenenFilmleriYukle();

            yukleme.IsRunning = false;
            yukleme.IsVisible = false;
            sayfa.IsVisible = true;

            lstIzlenen.BindingContext = VeriCekme.izlenenfilmler;
            lstBegenilen.BindingContext = VeriCekme.begenilenfilmler;
            lstYuklenen.BindingContext = VeriCekme.yuklenenfilmler;

            VeriCekme.izlenendiziler = await veriCekme.IzlenenDizileriYukle();
            VeriCekme.begenilendiziler = await veriCekme.BegenilenDizileriYukle();
            VeriCekme.yuklenendiziler = await veriCekme.YuklenenDizileriYukle();

            yuklemebittimi = true;
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

        //    lstIzlenen.BindingContext = tumSiniflar.filmler.OrderByDescending(x => x.FilmIzlenmeSayisi);
        //    lstBegenilen.BindingContext = tumSiniflar.filmler.OrderByDescending(x => x.FilmBegeniOrani);
        //    lstYuklenen.BindingContext = tumSiniflar.filmler.OrderByDescending(x => x.FilmID);

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

        //    BolumlerleriYukle();
        //}

        //private async void BolumlerleriYukle()
        //{
        //    AnaSayfa.bolumler = await serviceManager.GetBolumler();
        //}

        private void OnFilmGit(object sender, EventArgs e)
        {
            ImageButton Ibtn = (ImageButton)sender;

            Navigation.PushAsync(new FilmPage(Convert.ToInt32(Ibtn.CommandParameter.ToString())));
        }
    }
}