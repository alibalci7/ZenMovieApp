using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : TabbedPage
    {
        public DetailPage()
        {
            InitializeComponent();
            Title = "Ana Sayfa";
            BarBackgroundColor = Color.FromRgb(255, 216, 14);
            BarTextColor = Color.Black;
            BackgroundColor = Color.Black;
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(255, 216, 14);
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromRgb(30, 30, 35);
        }
    }
}