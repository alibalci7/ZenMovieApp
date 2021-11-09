using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SiralamaPage : PopupPage
    {
        FilmlerPage filmlerPage;
        DizilerPage dizilerPage;

        bool filmpagemi;
        public SiralamaPage(bool b, FilmlerPage f, DizilerPage d)
        {
            InitializeComponent();

            BackgroundColor = Color.Transparent;
            cerceve.BackgroundColor = Color.FromRgb(30, 30, 35);

            filmlerPage = f;
            dizilerPage = d;
            filmpagemi = b;
        }

        private async void OnIsmeGoreBK_Clicked(object sender, EventArgs e)
        {
            if (filmpagemi)
            {
                filmlerPage.IsmeGoreBKSirala();
            }
            else
            {
                dizilerPage.IsmeGoreBKSirala();
            }

            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OnIsmeGoreKB_Clicked(object sender, EventArgs e)
        {
            if (filmpagemi)
            {
                filmlerPage.IsmeGoreKBSirala();
            }
            else
            {
                dizilerPage.IsmeGoreKBSirala();
            }

            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OnYilaGoreBK_Clicked(object sender, EventArgs e)
        {
            if (filmpagemi)
            {
                filmlerPage.YilaGoreBKSirala();
            }
            else
            {
                dizilerPage.YilaGoreBKSirala();
            }

            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OnYilaGoreKB_Clicked(object sender, EventArgs e)
        {
            if (filmpagemi)
            {
                filmlerPage.YilaGoreKBSirala();
            }
            else
            {
                dizilerPage.YilaGoreKBSirala();
            }

            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OnIMDBGoreBK_Clicked(object sender, EventArgs e)
        {
            if (filmpagemi)
            {
                filmlerPage.IMDBGoreBKSirala();
            }
            else
            {
                dizilerPage.IMDBGoreBKSirala();
            }

            await PopupNavigation.Instance.PopAsync(true);
        }

        private async void OnIMDBGoreKB_Clicked(object sender, EventArgs e)
        {
            if (filmpagemi)
            {
                filmlerPage.IMDBGoreKBSirala();
            }
            else
            {
                dizilerPage.IMDBGoreKBSirala();
            }

            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}