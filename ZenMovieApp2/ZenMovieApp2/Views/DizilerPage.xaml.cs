using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMovieApp2.Models;
using ZenMovieApp2.Service;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DizilerPage : ContentPage
    {
        List<Dizi> secilidiziler = new List<Dizi>();
        VeriCekme veriCekme = new VeriCekme();

        public int yilmin = 1900, yilmax = DateTime.Now.Year;
        public float imdbmin = 1, imdbmax = 10;
        public DizilerPage()
        {
            InitializeComponent();

            Title = "DİZİLER";
            BackgroundColor = Color.Black;

            //NavigationPage.SetHasBackButton(this, false);
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(255, 216, 14);
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromRgb(30, 30, 35);

            lstDiziler.ItemAppearing += ListView_ItemAppearing;
            this.Appearing += DizilerPage_Appearing;

        }

        private void DizilerPage_Appearing(object sender, EventArgs e)
        {
            VeriCekme.diziler = null;
            doldur.IsVisible = false;
            doldur.IsRunning = false;
            FormLoad(yilmin, yilmax, imdbmin, imdbmax);
        }

        public async void FormLoad(int yilmin, int yilmax, float imdbmin, float imdbmax)
        {
            //foreach (var item in AnaSayfa.diziler)
            //{
            //    if (item.IMDB >= imdbmin && item.IMDB <= imdbmax)
            //    {
            //        if (item.DiziBaslangicYili >= yilmin && item.DiziBaslangicYili <= yilmax)
            //        {
            //            secilidiziler.Add(item);
            //        }
            //    }
            //}
            //secilidiziler = AnaSayfa.diziler.Where(x => x.IMDB >= Convert.ToSingle(imdbmin) && x.IMDB <= Convert.ToSingle(imdbmax) && x.DiziBaslangicYili >= Convert.ToInt32(yilmin) && x.DiziBaslangicYili <= Convert.ToInt32(yilmax)).ToList();
            yukleme.IsRunning = true;
            yukleme.IsVisible = true;
            VeriCekme.diziler = await veriCekme.DizileriYukle(imdbmin, imdbmax, yilmin, yilmax);
            secilidiziler = VeriCekme.diziler;
            lstDiziler.BindingContext = secilidiziler;
            yukleme.IsRunning = false;
            yukleme.IsVisible = false;
            sayfa.IsVisible = true;
        }

        async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var item = (Dizi)e.Item;
            if (item == VeriCekme.diziler.Last() && VeriCekme.diziler.Count >=10 )
            {
                doldur.IsVisible = true;
                doldur.IsRunning = true;
                List<Dizi> gelen = await veriCekme.DizileriYukle(imdbmin, imdbmax, yilmin, yilmax, item.DiziID, 10);
                if(gelen.Count > 0)
                {
                    VeriCekme.diziler.AddRange(gelen);
                    secilidiziler = VeriCekme.diziler;
                    lstDiziler.BindingContext = null;
                    lstDiziler.BindingContext = secilidiziler;
                }
                doldur.IsVisible = false;
                doldur.IsRunning = false;
            }
        }

        public async void Filtrele(int yilmin, int yilmax, float imdbmin, float imdbmax)
        {
            this.yilmin = yilmin;
            this.yilmax = yilmax;
            this.imdbmax = imdbmax;
            this.imdbmin = imdbmin;

            VeriCekme.diziler = await veriCekme.DizileriYukle(imdbmin, imdbmax, yilmin, yilmax);
            secilidiziler = VeriCekme.diziler;
            lstDiziler.BindingContext = null;
            lstDiziler.BindingContext = secilidiziler;
        }

        private void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            ListView listView = (ListView)sender;
            listView.SelectedItem = null;
        }

        private void OnDiziGit(object sender, EventArgs e)
        {
            ImageButton Ibtn = (ImageButton)sender;
            Navigation.PushAsync(new DiziPage(Convert.ToInt32(Ibtn.CommandParameter)));
        }

        private void OnRefreshed(object sender, EventArgs e)
        {
            FormLoad(yilmin, yilmax, imdbmin, imdbmax);
            lstDiziler.IsRefreshing = false;
        }

        private async void OnFiltreGit(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new FiltrePage(false, null, this));
        }

        private async void OnSiralamayaGit(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new SiralamaPage(false, null, this));
        }

        public void IsmeGoreBKSirala()
        {
            lstDiziler.BindingContext = secilidiziler.OrderByDescending(x => x.DiziBaslik).ToList();
        }

        public void IsmeGoreKBSirala()
        {
            lstDiziler.BindingContext = secilidiziler.OrderBy(x => x.DiziBaslik).ToList();
        }

        public void YilaGoreBKSirala()
        {
            lstDiziler.BindingContext = secilidiziler.OrderByDescending(x => x.DiziBaslangicYili).ToList();
        }

        public void YilaGoreKBSirala()
        {
            lstDiziler.BindingContext = secilidiziler.OrderBy(x => x.DiziBaslangicYili).ToList();
        }

        public void IMDBGoreBKSirala()
        {
            lstDiziler.BindingContext = secilidiziler.OrderByDescending(x => x.IMDB).ToList();
        }

        public void IMDBGoreKBSirala()
        {
            lstDiziler.BindingContext = secilidiziler.OrderBy(x => x.IMDB).ToList();
        }
    }
}