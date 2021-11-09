namespace ZenMovieApp2.Models
{
    public class Bolum
    {
        public int DiziBolumID { get; set; }

        public int DiziID { get; set; }

        public string DiziBolumLink { get; set; }

        public string DiziBolumBaslik { get; set; }

        public string DiziBolumSezonNo { get; set; }

        public int DiziIzlenmeSayisi { get; set; }

        public int DiziBolumEkleyenID { get; set; }

        public byte[] DiziResim { get; set; }

    }
}
