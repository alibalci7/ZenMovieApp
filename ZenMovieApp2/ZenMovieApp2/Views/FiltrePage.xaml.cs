using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMovieApp2.Models;
using ZenMovieApp2.Service;
using ZenMovieApp2.ViewModels;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FiltrePage : PopupPage
    {
        FilmlerPage filmlerPage;
        DizilerPage dizilerPage;
        KategoriViewModel kategoriviewModel = new KategoriViewModel();

        public List<Kategori> listkat;
        public List<string> listyil;
        public List<string> listimdb;
        public List<string> listanakat;
        public List<string> listyabancikat;
        public Kategori PickerSelectedItem { get; set; }

        bool filmpagemi, yabancifilmmi;
        int yilmin = 1990, yilmax = DateTime.Now.Year, kategoriID = 0;
        bool altyazimi = false;
        float imdbmin = 1, imdbmax = 10;
        string kategori = "";

        public FiltrePage(bool b, FilmlerPage f, DizilerPage d)
        {
            InitializeComponent();

            BackgroundColor = Color.Transparent;
            cerceve.BackgroundColor = Color.FromRgb(30, 30, 35);

            filmpagemi = b;
            filmlerPage = f;
            dizilerPage = d;

            FormLoad();
        }

        private void FormLoad()
        {
            listyil = new List<string>();
            listimdb = new List<string>()
            {
                "1","2","3","4","5","6","7","8","9","10"
            };
            listanakat = new List<string>()
            {
                "Yerli Filmler","Yabancı Filmler"
            };
            listyabancikat = new List<string>()
            {
                "Alt Yazılı","Dublaj"
            };

            int yil = Convert.ToInt32(DateTime.Now.Year.ToString());
            for (int i = yil; i >= 1900; i--)
            {
                listyil.Add(i.ToString());
            }

            pickerKat.ItemsSource = listanakat;



            pickerIMDBMin.ItemsSource = listimdb;
            pickerIMDBMin.Title = "1";
            pickerIMDBMax.ItemsSource = listimdb;
            pickerIMDBMax.Title = "10";

            pickerYilMin.ItemsSource = listyil;
            pickerYilMin.Title = "1900";
            pickerYilMax.ItemsSource = listyil;
            pickerYilMax.Title = DateTime.Now.Year.ToString();

            if (filmpagemi)
            {
                listkat = VeriCekme.kategoriler;
            }
            else
            {
                cerKategori.IsVisible = false;
            }

        }

        private void pickerYabanciKat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Xamarin.Forms.Picker pck = (Xamarin.Forms.Picker)sender;
            YabanciKategoriDoldur(pck.SelectedIndex);
        }

        private void pickerKat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Xamarin.Forms.Picker pck = (Xamarin.Forms.Picker)sender;
            kategori = "";
            KategoriDoldur(pck.SelectedIndex);
        }

        private void pickerAltKat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Xamarin.Forms.Picker pck = (Xamarin.Forms.Picker)sender;
            var aa = (Kategori)pck.SelectedItem;
            kategoriID = aa.KategoriID;
            kategori += aa.KategoriAd;
            lblKat.Text = kategori;
            Kategori();
            try
            {
                
            }
            catch
            {

            }

        }

        private void pickerIMDBMin_SelectedIndexChanged(object sender, EventArgs e)
        {
            Xamarin.Forms.Picker pck = (Xamarin.Forms.Picker)sender;
            imdbmin = Convert.ToSingle(pck.SelectedItem.ToString());
        }

        private void pickerIMDBMax_SelectedIndexChanged(object sender, EventArgs e)
        {
            Xamarin.Forms.Picker pck = (Xamarin.Forms.Picker)sender;
            imdbmax = Convert.ToSingle(pck.SelectedItem.ToString());
        }

        private void pickerYilMax_SelectedIndexChanged(object sender, EventArgs e)
        {
            Xamarin.Forms.Picker pck = (Xamarin.Forms.Picker)sender;
            yilmax = Convert.ToInt32(pck.SelectedItem.ToString());
        }

        private void pickerYilMin_SelectedIndexChanged(object sender, EventArgs e)
        {
            Xamarin.Forms.Picker pck = (Xamarin.Forms.Picker)sender;
            yilmin = Convert.ToInt32(pck.SelectedItem.ToString());
        }    

        private async void ButtonClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OnFiltreyiUygula(object sender, EventArgs e)
        {
            if (yilmin > yilmax || imdbmin > imdbmax)
            {
                DisplayAlert("UYARI", "Alt Sınır Üst Sınırdan Büyük Olamaz. Lütfen Kontrol Ediniz!", "TAMAM");
            }
            else
            {
                if (filmpagemi)
                {
                    filmlerPage.Filtrele(yilmin, yilmax, kategoriID, imdbmin, imdbmax, altyazimi);

                }
                else
                {
                    dizilerPage.Filtrele(yilmin, yilmax, imdbmin, imdbmax);
                }

                await PopupNavigation.Instance.PopAsync(true);
            }
        }

        private void KategoriDoldur(int b)
        {
            if (b == 0)
            {
                kategoriviewModel.Kategoriler = listkat.Where(x => x.KategoriUstID == 1).ToList();
                BindingContext = kategoriviewModel;
                pickerAltKat.SetBinding(Xamarin.Forms.Picker.ItemsSourceProperty, "Kategoriler");
                pickerAltKat.SetBinding(Xamarin.Forms.Picker.SelectedItemProperty, "SelectedKategori");
                pickerAltKat.ItemDisplayBinding = new Binding("KategoriAd");

                pickerKat.IsVisible = false;
                pickerAltKat.IsVisible = true;
                pickerYabanciKat.IsVisible = false;
                kategori = "Yerli Filmler/";
                pickerAltKat.Focus();
            }
            if (b == 1)
            {
                pickerYabanciKat.ItemsSource = listyabancikat;
                pickerKat.IsVisible = false;
                pickerAltKat.IsVisible = false;
                pickerYabanciKat.IsVisible = true;
                kategori = "Yabancı Filmler/";
                pickerYabanciKat.Focus();
            }

        }

        private void YabanciKategoriDoldur(int b)
        {
            if (b == 0)
            {
                altyazimi = true;
                kategori += "Alt Yazılı/";
            }
            if (b == 1)
            {
                altyazimi = false;
                kategori += "Dublaj/";
            }

            kategoriviewModel.Kategoriler = listkat.Where(x => x.KategoriUstID == 2).ToList();
            BindingContext = kategoriviewModel;
            pickerAltKat.SetBinding(Xamarin.Forms.Picker.ItemsSourceProperty, "Kategoriler");
            pickerAltKat.SetBinding(Xamarin.Forms.Picker.SelectedItemProperty, "SelectedKategori");
            pickerAltKat.ItemDisplayBinding = new Binding("KategoriAd");

            pickerKat.IsVisible = false;
            pickerAltKat.IsVisible = true;
            pickerYabanciKat.IsVisible = false;
            pickerAltKat.Focus();

        }

        private void Kategori()
        {
            pickerKat.Title = "Kategori";
            pickerKat.IsVisible = true;
            pickerAltKat.IsVisible = false;
            pickerYabanciKat.IsVisible = false;
        }

    }
}