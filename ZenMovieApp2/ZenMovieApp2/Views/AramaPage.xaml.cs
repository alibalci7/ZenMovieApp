using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AramaPage : TabbedPage
    {
        public AramaPage(string arama)
        {
            InitializeComponent();

            Title = "Arama Sonucu";
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(255, 216, 14);
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromRgb(30, 30, 35);

            BarBackgroundColor = Color.FromRgb(255, 216, 14);
            BarTextColor = Color.Black;
            BackgroundColor = Color.FromRgb(30, 30, 35);

            this.Children.Add(new AramaFilmPage(arama));
            this.Children.Add(new AramaDiziPage(arama));
        }
    }
}