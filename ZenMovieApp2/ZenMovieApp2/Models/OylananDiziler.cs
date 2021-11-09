using SQLite;

namespace ZenMovieApp2.Models
{
    public class OylananDiziler
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int DiziID { get; set; }

        public int Begeni { get; set; }

    }
}
