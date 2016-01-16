using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.Text.Core;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;

namespace MedicineApp
{
    class Baza
    {
        static Baza()
        {
            DbPath= Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "SQLITEV2.sqlite");
        }

        private static readonly string DbPath;

        /// <summary>
        /// Metoda ustvari bazo če ta ne obstaja. V nasprotnem primeru se metoda ne izvrši
        /// </summary>
        /// <returns>
        ///     true:baza je bila uspešno ustvarjena
        ///     false: -neznana napaka
        ///            -baza obstaja
        /// </returns>
        public bool CreateDB()
        {
            try
            {
                if (!CheckIfBaseExists())
                {
                    using (var db = DbConnection)
                    {
                        //dopolni po potrebi
                        db.CreateTable<Zdravilo>();
                        db.CreateTable<Interval>();
                        db.CreateTable<Opomnik>();
                        
                    }
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                DeleteDB();
                return false;
            }
        }
        private bool CheckIfBaseExists()
        {
            if (File.Exists(DbPath))
                return true;

            return false;
        }

        private void DeleteDB()
        {
            File.Delete(DbPath);
        }

        private static SQLiteConnection DbConnection
        {
            get
            {
                return new SQLiteConnection(
                    new SQLitePlatformWinRT(), DbPath);
            }
        }

        //ZDRAVILA
        //------------------------------------------------------------------
        public static void AddZdravilo(Zdravilo z)
        {
            using (var db = DbConnection)
            {
                db.Insert(z);
            }
        }

        public static bool DeleteZdravilo(Zdravilo z)
        {
            try
            {
                using (var db = DbConnection)
                {
                    db.Delete<Zdravilo>(z);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Zdravilo GetLastZdravilo()
        {
            using (var db = DbConnection)
            {
                return db.Table<Zdravilo>().LastOrDefault();
            }
        }

        public static Zdravilo GetLastZdraviloByName(string name)
        {
            using (var db = DbConnection)
            {
                return db.Table<Zdravilo>().FirstOrDefault(x => x.Naziv.ToLower() == name);
            }
        }

        public static List<Zdravilo> GetAllDZdravilo()
        {
            using (var db = DbConnection)
            {
                return db.Table<Zdravilo>().ToList();
            }
        }
        //------------------------------------------------------------------

    }

}
