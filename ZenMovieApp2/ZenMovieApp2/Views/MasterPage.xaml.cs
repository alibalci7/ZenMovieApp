using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public MasterPage()
        {
            InitializeComponent();
            Title = "Menü";
            BackgroundColor = Color.Black;
        }

        private void FilmlereGit(object sender, EventArgs e)
        {
            if (TabbedPageFilmContentPage.yuklemebittimi)
            {
                Navigation.PushAsync(new FilmlerPage());
            }
            else
            {
                DisplayAlert("UYARI", "Yükleme işlemi devam ediyor. Lütfen biraz bekleyiniz.", "TAMAM");
            }            
        }

        private void DizilereGit(object sender, EventArgs e)
        {
            if (TabbedPageFilmContentPage.yuklemebittimi)
            {
                Navigation.PushAsync(new DizilerPage());
            }
            else
            {
                DisplayAlert("UYARI", "Yükleme işlemi devam ediyor. Lütfen biraz bekleyiniz.", "TAMAM");
            }          
        }

        private void ListemeGit(object sender, EventArgs e)
        {
            if (TabbedPageFilmContentPage.yuklemebittimi)
            {
                Navigation.PushAsync(new ListemPage());
            }
            else
            {
                DisplayAlert("UYARI", "Yükleme işlemi devam ediyor. Lütfen biraz bekleyiniz.", "TAMAM");
            }           
        }

        private void GecmiseGit(object sender, EventArgs e)
        {
            if (TabbedPageFilmContentPage.yuklemebittimi)
            {
                Navigation.PushAsync(new GecmisPage());
            }
            else
            {
                DisplayAlert("UYARI", "Yükleme işlemi devam ediyor. Lütfen biraz bekleyiniz.", "TAMAM");
            }            
        }

        private void OnArama(object sender, EventArgs e)
        {
            if (TabbedPageFilmContentPage.yuklemebittimi)
            {
                string arama = sbArama.Text;
                Navigation.PushAsync(new AramaPage(arama));
            }
            else
            {
                DisplayAlert("UYARI", "Yükleme işlemi devam ediyor. Lütfen biraz bekleyiniz.", "TAMAM");
            }           
        }

    }
}