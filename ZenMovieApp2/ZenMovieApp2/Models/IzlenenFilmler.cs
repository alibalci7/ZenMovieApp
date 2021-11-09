using SQLite;

namespace ZenMovieApp2.Models
{
    public class IzlenenFilmler
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int FilmID { get; set; }

        public int Sure { get; set; }

    }
}
