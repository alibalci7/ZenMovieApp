using SQLite;

namespace ZenMovieApp2.Models
{
    public class FavoriDiziler
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int DiziID { get; set; }

    }
}
