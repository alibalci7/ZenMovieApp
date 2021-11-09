using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using ZenMovieApp2.Models;

namespace ZenMovieApp2.Service
{
    public class SQLiteManager
    {
        SQLiteConnection sqliteConnection;

        public SQLiteManager()
        {
            sqliteConnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            //sqliteConnection.DeleteAll<OylananFilmler>();
            //sqliteConnection.DeleteAll<OylananDiziler>();
            //sqliteConnection.DeleteAll<IzlenenFilmler>();
            //sqliteConnection.DeleteAll<IzlenenDiziler>();
            //sqliteConnection.DeleteAll<FavoriFilmler>();
            //sqliteConnection.DeleteAll<FavoriDiziler>();

            sqliteConnection.CreateTable<OylananFilmler>();
            sqliteConnection.CreateTable<OylananDiziler>();
            sqliteConnection.CreateTable<IzlenenFilmler>();
            sqliteConnection.CreateTable<IzlenenDiziler>();
            sqliteConnection.CreateTable<FavoriFilmler>();
            sqliteConnection.CreateTable<FavoriDiziler>();
        }

        public int FilmBegeniInsert(OylananFilmler o)
        {
            return sqliteConnection.Insert(o);
        }

        public int FilmBegeniUpdate(OylananFilmler o)
        {
            return sqliteConnection.Update(o);
        }

        public int FilmBegeniDelete(int id)
        {
            return sqliteConnection.Delete<OylananFilmler>(id);
        }

        public IEnumerable<OylananFilmler> GetAllFilmlerBegeni()
        {
            return sqliteConnection.Table<OylananFilmler>();
        }

        public bool GetFilmBegeniKontrol(int id)
        {
            return sqliteConnection.Table<OylananFilmler>().Any(x => x.FilmID == id);
        }

        public OylananFilmler GetFilmBegeni(int id)
        {
            return sqliteConnection.Table<OylananFilmler>().FirstOrDefault(x => x.FilmID == id);
        }


        public int DiziBegeniInsert(OylananDiziler o)
        {
            return sqliteConnection.Insert(o);
        }

        public int DiziBegeniUpdate(OylananDiziler o)
        {
            return sqliteConnection.Update(o);
        }

        public int DiziBegeniDelete(int id)
        {
            return sqliteConnection.Delete<OylananDiziler>(id);
        }

        public IEnumerable<OylananDiziler> GetAllDizilerBegeni()
        {
            return sqliteConnection.Table<OylananDiziler>();
        }

        public bool GetDiziBegeniKontrol(int id)
        {
            return sqliteConnection.Table<OylananDiziler>().Any(x => x.DiziID == id);
        }

        public OylananDiziler GetDiziBegeni(int id)
        {
            return sqliteConnection.Table<OylananDiziler>().FirstOrDefault(x => x.DiziID == id);
        }



        public int IzlenenFilmlerInsert(IzlenenFilmler i)
        {
            return sqliteConnection.Insert(i);
        }

        public int IzlenenFilmlerUpdate(IzlenenFilmler i)
        {
            return sqliteConnection.Update(i);
        }

        public IEnumerable<IzlenenFilmler> GetAllIzlenenFilmler()
        {
            return sqliteConnection.Table<IzlenenFilmler>();
        }

        public bool GetIzlenenFilmlerKontrol(int id)
        {
            return sqliteConnection.Table<IzlenenFilmler>().Any(x => x.FilmID == id);
        }

        public IzlenenFilmler GetIzlenenFilm(int id)
        {
            return sqliteConnection.Table<IzlenenFilmler>().FirstOrDefault(x => x.FilmID == id);
        }



        public int IzlenenDizilerInsert(IzlenenDiziler i)
        {
            return sqliteConnection.Insert(i);
        }

        public int IzlenenDizilerUpdate(IzlenenDiziler i)
        {
            return sqliteConnection.Update(i);
        }

        public IEnumerable<IzlenenDiziler> GetAllIzlenenDiziler()
        {
            return sqliteConnection.Table<IzlenenDiziler>();
        }

        public bool GetIzlenenDizilerKontrol(int id)
        {
            return sqliteConnection.Table<IzlenenDiziler>().Any(x => x.DiziID == id);
        }

        public IzlenenDiziler GetIzlenenDizi(int id)
        {
            return sqliteConnection.Table<IzlenenDiziler>().FirstOrDefault(x => x.DiziID == id);
        }



        public int FavoriFilmlerInsert(FavoriFilmler f)
        {
            return sqliteConnection.Insert(f);
        }

        public int FavoriFilmDelete(int id)
        {
            return sqliteConnection.Delete<FavoriFilmler>(id);
        }

        public IEnumerable<FavoriFilmler> GetAllFavoriFilmler()
        {
            return sqliteConnection.Table<FavoriFilmler>();
        }

        public bool GetFavoriFilmlerKontrol(int id)
        {
            return sqliteConnection.Table<FavoriFilmler>().Any(x => x.FilmID == id);
        }

        public FavoriFilmler GetFavoriFilm(int id)
        {
            return sqliteConnection.Table<FavoriFilmler>().FirstOrDefault(x => x.FilmID == id);
        }



        public int FavoriDizilerInsert(FavoriDiziler f)
        {
            return sqliteConnection.Insert(f);
        }

        public int FavoriDiziDelete(int id)
        {
            return sqliteConnection.Delete<FavoriDiziler>(id);
        }

        public IEnumerable<FavoriDiziler> GetAllFavoriDiziler()
        {
            return sqliteConnection.Table<FavoriDiziler>();
        }

        public bool GetFavoriDizilerKontrol(int id)
        {
            return sqliteConnection.Table<FavoriDiziler>().Any(x => x.DiziID == id);
        }

        public FavoriDiziler GetFavoriDizi(int id)
        {
            return sqliteConnection.Table<FavoriDiziler>().FirstOrDefault(x => x.DiziID == id);
        }

    }
}
