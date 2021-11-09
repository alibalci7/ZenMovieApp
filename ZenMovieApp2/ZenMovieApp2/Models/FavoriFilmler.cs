using SQLite;

namespace ZenMovieApp2.Models
{
    public class FavoriFilmler
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int FilmID { get; set; }

    }
}
