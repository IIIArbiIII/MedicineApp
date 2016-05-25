﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.Text.Core;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;
using SQLite.Net.Platform.WinRT;

namespace MedicineApp
{
    class Baza
    {
        private static readonly string DbPath;
        static Baza()
        {
           DbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "SQLITEV2.sqlite");
        }

        /// <summary>
        /// Metoda ustvari bazo ob vsakem ponovnem zagonu aplikacije :D. 
        /// </summary>
        /// <returns>
        ///     true: baza je bila uspešno ustvarjena
        ///     false: -neznana napaka
        /// </returns>
        public bool CreateDb()
        {
            try
            {
                if (CheckIfBaseExists())
                    DeleteDB();
                

                if (!CheckIfBaseExists())
                {
                    using (var db = DbConnection)
                    {
                        //dopolni po potrebi
                        db.CreateTable<Zdravilo>();
                        db.CreateTable<Interval>();
                        db.CreateTable<Opomnik>();
                        db.CreateTable<Skrbnik>();
                    }
                    return true;
                }
                return false;
            }

            catch (Exception)
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
                    new SQLitePlatformWinRT(), DbPath, false);
            }
        }

        private static SQLiteAsyncConnection DbConnectionAsync
        {
            get
            {
                return 
                    new SQLiteAsyncConnection(
                        () =>
                            new SQLiteConnectionWithLock(new SQLitePlatformWinRT(),
                                new SQLiteConnectionString(DbPath, storeDateTimeAsTicks: false)));

                var connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(DbPath, storeDateTimeAsTicks: false)));
                //var asyncConnection = new SQLiteAsyncConnection(connectionFactory);

                //return asyncConnection;
                return new SQLiteAsyncConnection(connectionFactory);
            }
        }



        //ZDRAVILA
        //------------------------------------------------------------------

        //Prevero
        public static void AddZdravilo(Zdravilo z)
        {
            using (var db = DbConnection)
            {
                db.Insert(z);
            }
        }

        //Prevero
        public static bool DeleteZdravilo(Zdravilo z)
        {
            try
            {
                using (var db = DbConnection)
                {
                    var query = db.Table<Zdravilo>().FirstOrDefault(x => x.Id == z.Id);
                    if (query!=null)
                    {
                        db.Delete<Zdravilo>(z.Id);
                    }
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

        public static Zdravilo GetFirstZdraviloByName(string name)
        {
            using (var db = DbConnection)
            {
                return db.Table<Zdravilo>().FirstOrDefault(x => x.Naziv.ToLower() == name);
            }
        }

        public static Zdravilo GetFirstZdraviloById(Zdravilo z)
        {
            using (var db = DbConnection)
            {
                return db.Table<Zdravilo>().FirstOrDefault(x => x.Id == z.Id);
            }
        }

        public static List<Zdravilo> GetAllDZdravilo()
        {
            using (var db = DbConnection)
            {
                return db.Table<Zdravilo>().ToList();
            }
        }

        public static async Task<List<Zdravilo>> GetAllDZdraviloAsync()
        {
            var conn = new SQLiteAsyncConnection(
                        () =>
                            new SQLiteConnectionWithLock(new SQLitePlatformWinRT(),
                                new SQLiteConnectionString(DbPath, storeDateTimeAsTicks: false)));

            List<Zdravilo> seznamZdravil = await conn.Table<Zdravilo>().ToListAsync();
                return seznamZdravil;
            
            
        }
        //------------------------------------------------------------------
        //Skrbnik
        public static Skrbnik GetSkrbnik(Skrbnik s)
        {
            using (var db = DbConnection)
            {
                return db.Table<Skrbnik>().FirstOrDefault();
            }
        }
        public static void AddSkrbnik(Skrbnik s)
        {
            using (var db = DbConnection)
            {
                db.Insert(s);
            }
        }
        public static void UpdateSkrbnik(Skrbnik s)
        {
            using (var db = DbConnection)
            {
                Skrbnik query = db.Table<Skrbnik>().FirstOrDefault();
                if (query!=null)
                {
                    AddSkrbnik(s);
                    db.Delete<Skrbnik>(query.Id);
                }
            }
        }

        //------------------------------------------------------------------
        //Interval
        public static void AddInterval(Interval i)
        {
            using (var db = DbConnection)
            {
                db.Insert(i);
            }
        }
        //------------------------------------------------------------------


        //Opomnik
        //------------------------------------------------------------------
        public static void AddOpomnik(Opomnik o)
        {
            using (var db = DbConnection)
            {
                db.Insert(o);
            }
        }

    }

}
