using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;

namespace MedicineApp
{
    class Baza
    {
        private static SQLiteConnection DbConnection
        {
            get
            {
                return new SQLiteConnection(
                    new SQLitePlatformWinRT(),
                    Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "SQLITEV2.sqlite"));
            }
        }

        public static void AddZdravilo(Zdravilo d)
        {
            using (var db = DbConnection)
            {
                db.CreateTable<Zdravilo>();
                db.Insert(d);
            }
        }

        public static Zdravilo GetLastZdravilo()
        {
            using (var db = DbConnection)
            {
                return db.Table<Zdravilo>().LastOrDefault();
            }
        }

        public static List<Zdravilo> GetAllDZdravilo()
        {
            using (var db = DbConnection)
            {
                return db.Table<Zdravilo>().ToList();
            }
        }

    }

}
