using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenMovieApp2.Models;
using ZenMovieApp2.ViewModels;

namespace ZenMovieApp2.Service
{
    public class VeriCekme
    {
        public static List<Dizi> diziler = new List<Dizi>();
        public static List<Dizi> izlenendiziler;
        public static List<Dizi> begenilendiziler;
        public static List<Dizi> yuklenendiziler;

        public static List<Film> filmler = new List<Film>();
        public static List<Film> izlenenfilmler;
        public static List<Film> begenilenfilmler;
        public static List<Film> yuklenenfilmler;

        public static List<Kategori> kategoriler;

        TumSiniflar tumSiniflar = new TumSiniflar();
        ServiceManager serviceManager = new ServiceManager();

        public async Task<List<Film>> FilmleriYukle(float imdbmin,float imdbmax,int yilmin,int yilmax,int kategoriid=0, int id = 0,int adet=10)
        {
            if (kategoriid != 0)
            {
                string s= "Film/GetKategoriliFilmleriListele?imdbmin=" + imdbmin.ToString() + "&imdbmax=" + imdbmax.ToString() + "&yilmin=" + yilmin.ToString() + "&yilmax=" + yilmax.ToString() + "&kategoriid=" + kategoriid.ToString() + "&id=" + id.ToString() + "&adet=" + adet.ToString();
                tumSiniflar = await serviceManager.GetAll(s, 1);
            }
            else
            {
                string s = "Film/GetFilmleriListele?imdbmin=" + imdbmin.ToString() + "&imdbmax=" + imdbmax.ToString() + "&yilmin=" + yilmin.ToString() + "&yilmax=" + yilmax.ToString() + "&id=" + id.ToString() + "&adet=" + adet.ToString();
                tumSiniflar = await serviceManager.GetAll(s, 1);
            }          

            foreach (var item in tumSiniflar.filmler)
            {
                item.FilmIMDB = item.FilmIMDB.Replace(',', '.');
                item.IMDB = Convert.ToSingle(item.FilmIMDB.Replace(',', '.'));
                if (item.IMDB > 10)
                {
                    item.IMDB = item.IMDB / 10;
                }
            }
            return tumSiniflar.filmler;
        }

        public async Task<Film> FilmYukle(int id)
        {
            tumSiniflar = await serviceManager.GetAll("Film/GetFilmListele?id=" + id.ToString(), 1);

            foreach (var item in tumSiniflar.filmler)
            {
                item.FilmIMDB = item.FilmIMDB.Replace(',', '.');
                item.IMDB = Convert.ToSingle(item.FilmIMDB.Replace(',', '.'));
                if (item.IMDB > 10)
                {
                    item.IMDB = item.IMDB / 10;
                }
            }
            return tumSiniflar.filmler.FirstOrDefault();
        }

        public async Task<List<Film>> FilmleriAra(string ad, int id=0, int adet=10)
        {
            tumSiniflar = await serviceManager.GetAll("Film/GetFilmleriAra?ad="+ ad.ToString() +"&id=" + id.ToString() + "&adet="+adet.ToString(), 1);

            foreach (var item in tumSiniflar.filmler)
            {
                item.FilmIMDB = item.FilmIMDB.Replace(',', '.');
                item.IMDB = Convert.ToSingle(item.FilmIMDB.Replace(',', '.'));
                if (item.IMDB > 10)
                {
                    item.IMDB = item.IMDB / 10;
                }
            }
            return tumSiniflar.filmler;
        }

        public async Task<List<Dizi>> DizileriYukle(float imdbmin, float imdbmax, int yilmin, int yilmax, int id=0, int adet=10)
        {
            string s= "Dizi/GetDizileriListele?imdbmin=" + imdbmin.ToString() + "&imdbmax=" + imdbmax.ToString() + "&yilmin=" + yilmin.ToString() + "&yilmax=" + yilmax.ToString() + "&id=" + id.ToString() + "&adet=" + adet.ToString();
            tumSiniflar = await serviceManager.GetAll(s, 2);
            foreach (var item in tumSiniflar.diziler)
            {
                item.DiziIMDB = item.DiziIMDB.Replace(',', '.');
                item.IMDB = Convert.ToSingle(item.DiziIMDB.Replace(',', '.'));
                if (item.IMDB > 10)
                {
                    item.IMDB = item.IMDB / 10;
                }
            }
            return tumSiniflar.diziler;
        }

        public async Task<Dizi> DiziYukle(int id)
        {
            tumSiniflar = await serviceManager.GetAll("Dizi/GetDiziListele?id=" + id.ToString(), 2);
            foreach (var item in tumSiniflar.diziler)
            {
                item.DiziIMDB = item.DiziIMDB.Replace(',', '.');
                item.IMDB = Convert.ToSingle(item.DiziIMDB.Replace(',', '.'));
                if (item.IMDB > 10)
                {
                    item.IMDB = item.IMDB / 10;
                }
            }
            return tumSiniflar.diziler.FirstOrDefault();
        }

        public async Task<List<Dizi>> DizileriAra(string ad,int id=0,int adet=10)
        {
            tumSiniflar = await serviceManager.GetAll("Dizi/GetDizileriAra?ad=" + ad.ToString() + "&id=" + id.ToString() + "&adet=" + adet.ToString(), 2);
            foreach (var item in tumSiniflar.diziler)
            {
                item.DiziIMDB = item.DiziIMDB.Replace(',', '.');
                item.IMDB = Convert.ToSingle(item.DiziIMDB.Replace(',', '.'));
                if (item.IMDB > 10)
                {
                    item.IMDB = item.IMDB / 10;
                }
            }
            return tumSiniflar.diziler;
        }

        public async Task<List<Film>> IzlenenFilmleriYukle()
        {
            if (izlenenfilmler==null)
            {
                tumSiniflar = await serviceManager.GetAll("Film/GetCokIzlenenFilmleriListele", 1);
                izlenenfilmler = tumSiniflar.filmler;

                foreach (var item in izlenenfilmler)
                {
                    item.FilmIMDB = item.FilmIMDB.Replace(',', '.');
                    item.IMDB = Convert.ToSingle(item.FilmIMDB.Replace(',', '.'));
                    if (item.IMDB > 10)
                    {
                        item.IMDB = item.IMDB / 10;
                    }
                }
            }
            return izlenenfilmler;
        }

        public async Task<List<Film>> BegenilenFilmleriYukle()
        {
            if (begenilenfilmler == null)
            {
                tumSiniflar = await serviceManager.GetAll("Film/GetCokBegenilenFilmleriListele", 1);
                begenilenfilmler = tumSiniflar.filmler;

                foreach (var item in begenilenfilmler)
                {
                    item.FilmIMDB = item.FilmIMDB.Replace(',', '.');
                    item.IMDB = Convert.ToSingle(item.FilmIMDB.Replace(',', '.'));
                    if (item.IMDB > 10)
                    {
                        item.IMDB = item.IMDB / 10;
                    }
                }
            }
            return begenilenfilmler;
        }

        public async Task<List<Film>> YuklenenFilmleriYukle()
        {
            if (yuklenenfilmler == null)
            {
                tumSiniflar = await serviceManager.GetAll("Film/GetSonEklenenFilmleriListele", 1);
                yuklenenfilmler = tumSiniflar.filmler;

                foreach (var item in yuklenenfilmler)
                {
                    item.FilmIMDB = item.FilmIMDB.Replace(',', '.');
                    item.IMDB = Convert.ToSingle(item.FilmIMDB.Replace(',', '.'));
                    if (item.IMDB > 10)
                    {
                        item.IMDB = item.IMDB / 10;
                    }
                }
            }
            return yuklenenfilmler;
        }

        public async Task<List<Dizi>> IzlenenDizileriYukle()
        {
            if (izlenendiziler == null)
            {
                tumSiniflar = await serviceManager.GetAll("Dizi/GetCokIzlenenDizileriListele", 2);
                izlenendiziler = tumSiniflar.diziler;
                foreach (var item in izlenendiziler)
                {
                    item.DiziIMDB = item.DiziIMDB.Replace(',', '.');
                    item.IMDB = Convert.ToSingle(item.DiziIMDB.Replace(',', '.'));
                    if (item.IMDB > 10)
                    {
                        item.IMDB = item.IMDB / 10;
                    }
                }
            }
            return izlenendiziler;
        }

        public async Task<List<Dizi>> BegenilenDizileriYukle()
        {
            if (begenilendiziler == null)
            {
                tumSiniflar = await serviceManager.GetAll("Dizi/GetCokBegenilenDizileriListele", 2);
                begenilendiziler = tumSiniflar.diziler;
                foreach (var item in begenilendiziler)
                {
                    item.DiziIMDB = item.DiziIMDB.Replace(',', '.');
                    item.IMDB = Convert.ToSingle(item.DiziIMDB.Replace(',', '.'));
                    if (item.IMDB > 10)
                    {
                        item.IMDB = item.IMDB / 10;
                    }
                }
            }
            return begenilendiziler;
        }

        public async Task<List<Dizi>> YuklenenDizileriYukle()
        {
            if (yuklenendiziler == null)
            {
                tumSiniflar = await serviceManager.GetAll("Dizi/GetSonEklenenDizileriListele", 2);
                yuklenendiziler = tumSiniflar.diziler;
                foreach (var item in yuklenendiziler)
                {
                    item.DiziIMDB = item.DiziIMDB.Replace(',', '.');
                    item.IMDB = Convert.ToSingle(item.DiziIMDB.Replace(',', '.'));
                    if (item.IMDB > 10)
                    {
                        item.IMDB = item.IMDB / 10;
                    }
                }
            }
            return yuklenendiziler;
        }

    }
}
