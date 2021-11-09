using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnaSayfa : MasterDetailPage
    {
        //public static List<Dizi> diziler;
        //public static List<Film> filmler;
        //public static List<FilmKategori> filmKategoriler;
        //public static List<Bolum> bolumler;
        //public static List<Kategori> kategoriler;

        public AnaSayfa()
        {
            InitializeComponent();
            Title = "MasterDetail";
            this.Master = new MasterPage();
            this.Detail = new NavigationPage(new DetailPage())
            {
                BarBackgroundColor = Color.FromRgb(255, 216, 14),
                BarTextColor = Color.Black
            };

            FormLoad();
        }

        private async void FormLoad()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                bool b = await DisplayAlert("UYARI", "Lütfen internet ayarlarınızı kontrol ediniz!", "TAMAM", "TEKRAR DENE");
                if (b)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
                else
                {
                    FormLoad();
                }

            }
        }

    }
}