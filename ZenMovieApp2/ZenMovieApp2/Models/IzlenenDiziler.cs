using SQLite;

namespace ZenMovieApp2.Models
{
    public class IzlenenDiziler
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int DiziID { get; set; }

        public int Sure { get; set; }

    }
}
