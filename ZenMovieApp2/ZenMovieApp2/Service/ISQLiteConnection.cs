using SQLite;

namespace ZenMovieApp2.Service
{
    public interface ISQLiteConnection
    {
        SQLiteConnection GetConnection();
    }
}
