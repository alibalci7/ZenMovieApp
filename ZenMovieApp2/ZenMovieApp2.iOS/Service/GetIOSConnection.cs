using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SQLite;
using UIKit;
using ZenMovieApp2.iOS.Service;
using ZenMovieApp2.Service;

[assembly: Xamarin.Forms.Dependency(typeof(GetIOSConnection))]
namespace ZenMovieApp2.iOS.Service
{
    public class GetIOSConnection : ISQLiteConnection
    {
        public SQLiteConnection GetConnection()
        {
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(documentPath, App.DBName);
            // platform = new SQLitePlatformAndroid();
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}