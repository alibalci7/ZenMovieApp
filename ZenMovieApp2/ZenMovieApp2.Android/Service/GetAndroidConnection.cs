using SQLite;
using ZenMovieApp2.Droid.Service;
using ZenMovieApp2.Service;

[assembly: Xamarin.Forms.Dependency(typeof(GetAndroidConnection))]
namespace ZenMovieApp2.Droid.Service
{
    public class GetAndroidConnection : ISQLiteConnection
    {
        public SQLiteConnection GetConnection()
        {
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(documentPath, App.DBName);
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}