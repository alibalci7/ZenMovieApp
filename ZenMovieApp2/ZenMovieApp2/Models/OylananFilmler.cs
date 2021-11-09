using SQLite;

namespace ZenMovieApp2.Models
{
    public class OylananFilmler
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int FilmID { get; set; }

        public int Begeni { get; set; }

    }
}
