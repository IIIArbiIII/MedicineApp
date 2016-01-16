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
                        db.CreateTable<Opomnik>();
                        db.CreateTable<Operacije>();
                    }
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool CheckIfBaseExists()
        {
            if (File.Exists(DbPath))
                return true;

            return false;
        }

        private static SQLiteConnection DbConnection
        {
            get
            {
                return new SQLiteConnection(
                    new SQLitePlatformWinRT(), DbPath);
            }
        }

        public static void AddZdravilo(Zdravilo d)
        {
            using (var db = DbConnection)
            {
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
