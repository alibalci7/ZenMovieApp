using Octane.Xamarin.Forms.VideoPlayer;
using System;
using System.Linq;
using System.Threading.Tasks;
using VideoLibrary;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZenMovieApp2.Helper;
using ZenMovieApp2.Models;
using ZenMovieApp2.Service;

namespace ZenMovieApp2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPage : ContentPage
    {
        ServiceManager serviceManager = new ServiceManager();
        SQLiteManager sQLiteManager = new SQLiteManager();
        //LibVLC libvlc;
        //Media media;
        IzlenenFilmler izlenenfilm = new IzlenenFilmler();
        IzlenenDiziler izlenendizi = new IzlenenDiziler();
        VideoPlayer videoPlayer = new VideoPlayer();

        string link = "", tip = "";
        int videoid, sure = 1, videouzunluk = 0;
        bool basladimi;
        public VideoPage(string url, string t, int id)
        {
            InitializeComponent();

            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromRgb(255, 216, 14);
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.FromRgb(30, 30, 35);
            BackgroundColor = Color.Black;
            videoid = id;
            tip = t;

            DeviceDisplay.KeepScreenOn = true;
            this.Disappearing += VideoPage_Disappearing;
            DependencyService.Get<IStatusBar>().HideStatusBar();

            FormLoad(url);
            bekle(15000);
        }

        private void VideoPage_Disappearing(object sender, EventArgs e)
        {
            videoPlayer.Pause();
            switch (tip)
            {
                case "film":
                    if (izlenenfilm.ID == 0)
                    {
                        izlenenfilm.FilmID = videoid;
                        izlenenfilm.Sure = 1;
                        sQLiteManager.IzlenenFilmlerInsert(izlenenfilm);
                    }
                    else
                    {
                        izlenenfilm.ID = sQLiteManager.GetIzlenenFilm(videoid).ID;
                        izlenenfilm.FilmID = videoid;
                        izlenenfilm.Sure = 1;
                        sQLiteManager.IzlenenFilmlerUpdate(izlenenfilm);
                    }
                    break;
                case "dizi":
                    if (izlenendizi.ID == 0)
                    {
                        izlenendizi.DiziID = videoid;
                        izlenendizi.Sure = 1;
                        sQLiteManager.IzlenenDizilerInsert(izlenendizi);
                    }
                    else
                    {
                        izlenendizi.ID = sQLiteManager.GetIzlenenDizi(videoid).ID;
                        izlenendizi.DiziID = videoid;
                        izlenendizi.Sure = 1;
                        sQLiteManager.IzlenenDizilerUpdate(izlenendizi);
                    }
                    break;
            }

            switch (tip)
            {
                case "film":
                    Navigation.PopModalAsync();
                    break;
                case "dizi":
                    Navigation.PopModalAsync();
                    break;
            }
        }

        private async void FormLoad(string url)
        {
            try
            {
                string yol = "";
                switch (tip)
                {
                    case "film":
                        yol = "Film/GetFilmIzlenmeGuncelle?id=" + videoid.ToString();
                        if (sQLiteManager.GetIzlenenFilmlerKontrol(videoid))
                        {
                            izlenenfilm = sQLiteManager.GetIzlenenFilm(videoid);
                            sure = izlenenfilm.Sure;
                        }
                        break;
                    case "dizi":
                        yol = "Film/GetDiziIzlenmeGuncelle?id=" + videoid.ToString();
                        if (sQLiteManager.GetIzlenenDizilerKontrol(videoid))
                        {
                            izlenendizi = sQLiteManager.GetIzlenenDizi(videoid);
                            sure = izlenendizi.Sure;
                        }
                        break;
                }
                await serviceManager.IzlemeInsert(yol);

                var youtube = YouTube.Default;
				//videonun kendi url si kapalı. Sıkıntı yaşamamak için şimdilik Burçlopedi videosunu çekip izletiyoruz.
				//var video = await youtube.GetVideoAsync(url);
                var video = await youtube.GetVideoAsync("https://www.youtube.com/watch?v=el8tw5UerDg");
                link = video.Uri;

                //videos = videos.Where(x => x.Format == VideoFormat.Mp4 && x.AudioFormat == AudioFormat.Aac);
                //if (videos.Where(x => x.Resolution == 720) != null)
                //{
                //    link = videos.FirstOrDefault(x => x.Resolution == 720).Uri;
                //}
                //else if (videos.Where(x => x.Resolution == 480) != null)
                //{
                //    link = videos.FirstOrDefault(x => x.Resolution == 480).Uri;
                //}
                //else if (videos.Where(x => x.Resolution == 360) != null)
                //{
                //    link = videos.FirstOrDefault(x => x.Resolution == 360).Uri;
                //}
                //else
                //{
                //    link = videos.FirstOrDefault(x => x.Resolution == 240).Uri;
                //}

                videoPlayer.Source = link;
                videoPlayer.AutoPlay = true;
                videoPlayer.Play();
                videoCerceve.Children.Add(videoPlayer);

                actindicator.IsRunning = false;
                cerceve.IsVisible = false;
                
                basladimi = true;
            }
            catch
            {
                DisplayAlert("HATA", "Beklenmedik Bir Hata Oluştu. Lütfen Tekrar Deneyiniz.", "TAMAM");
                switch (tip)
                {
                    case "film":
                        Navigation.PopModalAsync();
                        break;
                    case "dizi":
                        Navigation.PopModalAsync();
                        break;
                }

            }
        }

        private async void bekle(int sure)
        {
            await Task.Delay(sure);
            if (!basladimi)
            {
                videoPlayer.Pause();
                DisplayAlert("HATA", "Beklenmedik Bir Hata Oluştu. Lütfen Tekrar Deneyiniz.", "TAMAM");
                switch (tip)
                {
                    case "film":
                        Navigation.PopModalAsync();
                        break;
                    case "dizi":
                        Navigation.PopModalAsync();
                        break;
                }
            }
        }

    }
}