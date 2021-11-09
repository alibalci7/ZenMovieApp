using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ZenMovieApp2.Models;
using ZenMovieApp2.ViewModels;

namespace ZenMovieApp2.Service
{
    public class ServiceManager
    {
        private string url = "http://192.168.1.8:8085/api/";
        //private string url = "http://213.14.133.208:8085/api/";
        
        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/JSON");

            return client;
        }

        public async Task<TumSiniflar> GetAll(string yol, int tip)
        {
            HttpClient client = await GetClient();
            var jsonResult = await client.GetStringAsync(url + yol);
            TumSiniflar tumsiniflar = new TumSiniflar();
            switch (tip)
            {
                case 1:
                    tumsiniflar.filmler = JsonConvert.DeserializeObject<List<Film>>(jsonResult);
                    break;
                case 2:
                    tumsiniflar.diziler = JsonConvert.DeserializeObject<List<Dizi>>(jsonResult);
                    break;
            }

            return tumsiniflar;
        }

        public async Task<List<Kategori>> GetKategoriler()
        {
            HttpClient client = await GetClient();
            var jsonResult = await client.GetStringAsync(url + "Film/GetKategorileriListele");
            List<Kategori> kategoriler = JsonConvert.DeserializeObject<List<Kategori>>(jsonResult);

            return kategoriler;
        }

        public async Task<List<FilmKategori>> GetFilmKategoriler()
        {
            HttpClient client = await GetClient();
            var jsonResult = await client.GetStringAsync(url + "Film/GetFilmKategorileriListele");
            List<FilmKategori> filmkategoriler = JsonConvert.DeserializeObject<List<FilmKategori>>(jsonResult);

            return filmkategoriler;
        }

        public async Task<List<Bolum>> GetBolumler(int id)
        {
            HttpClient client = await GetClient();
            var jsonResult = await client.GetStringAsync(url + "Dizi/GetBolumleriListele?id="+id.ToString());
            List<Bolum> bolumler = JsonConvert.DeserializeObject<List<Bolum>>(jsonResult);

            return bolumler;
        }

        public async Task<bool> BegeniInsert(string yol)
        {
            try
            {
                HttpClient client = await GetClient();
                var jsonResult = await client.GetStringAsync(url + yol);
                string sonuc = JsonConvert.DeserializeObject<string>(jsonResult);

                if (jsonResult == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }

            //insert ten gelen cevabı alıyoruz
            //var result = JsonConvert.DeserializeObject<Film>(jsonResult);

            //return result;
        }

        public async Task IzlemeInsert(string yol)
        {
            try
            {
                HttpClient client = await GetClient();
                var jsonResult = await client.GetStringAsync(url + yol);
            }
            catch
            {
            }

            //insert ten gelen cevabı alıyoruz
            //var result = JsonConvert.DeserializeObject<Film>(jsonResult);

            //return result;
        }
    }
}
