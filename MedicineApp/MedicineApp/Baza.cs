using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.Text.Core;
using Windows.UI.Xaml.Controls;
using MedicineApp.Classi;
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
                        db.CreateTable<Termin>();
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

        //public static Zdravilo GetLastZdravilo()
        //{
        //    using (var db = DbConnection)
        //    {
        //        return db.Table<Zdravilo>().LastOrDefault();
        //    }
        //}

        //public static Zdravilo GetFirstZdraviloByName(string name)
        //{
        //    using (var db = DbConnection)
        //    {
        //        return db.Table<Zdravilo>().FirstOrDefault(x => x.Naziv.ToLower() == name);
        //    }
        //}

        public static Zdravilo GetFirstZdraviloById(int id)
        {
            using (var db = DbConnection)
            {
                return db.Table<Zdravilo>().FirstOrDefault(x => x.Id == id);
            }
        }

        //public static List<Zdravilo> GetAllDZdravilo()
        //{
        //    using (var db = DbConnection)
        //    {
        //        return db.Table<Zdravilo>().ToList();
        //    }
        //}

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
        public static Skrbnik GetSkrbnik(int pin)
        {
            using (var db = DbConnection)
            {
                return db.Table<Skrbnik>().FirstOrDefault(x => x.Pin == pin);
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
                Skrbnik query = db.Table<Skrbnik>().FirstOrDefault(x => x.TelStevilka == s.TelStevilka);
                if (query!=null)
                {
                    AddSkrbnik(s);
                    db.Delete<Skrbnik>(query.Id);
                }
            }
        }

        //------------------------------------------------------------------
        //Interval
        private static void AddInterval(Interval i)
        {
            foreach (var x in i.SeznamTerminovZaAlarm)
            {
                AddTermin(x);
            }
            using (var db = DbConnection)
            {
                db.Insert(i);
            }
        }

        private async static void AddIntervalAsync(Interval i)
        {
            var conn = DbConnectionAsync;
            await conn.InsertAsync(i).ContinueWith((t) =>
            {
                if (t.IsCompleted)
                {
                    foreach (var x in i.SeznamTerminovZaAlarm)
                    {
                        x.IntervalId = GetLastIntervalkId();
                        AddTermin(x);
                    }
                }

            });
        }
        //------------------------------------------------------------------
        //Termin
        private static void AddTermin(Termin t)
        {
            using (var db = DbConnection)
            {
                db.Insert(t);
            }
        }
        //------------------------------------------------------------------

        //Opomnik
        //------------------------------------------------------------------
        //public static void AddOpomnik(Opomnik o)
        //{
        //    var intervali = o.Intervali;

        //    foreach (var x in intervali)
        //    {
        //        AddInterval(x);
        //    }
        //    using (var db = DbConnection)
        //    {
        //        db.Insert(o);
        //    }
        //}

        //public async static Task<int> AddOpomnikAsync(Opomnik o)
        //{
        //    var conn = DbConnectionAsync;
        //    var id = -1;
        //    await conn.InsertAsync(o).ContinueWith((t) =>
        //    {
        //        if (t.IsCompleted)
        //        {
        //            foreach (var x in o.Intervali)
        //            {
        //                x.OpomnikId = GetLastOpomnikId();
        //                id = x.OpomnikId;
        //                //AddInterval(x);
        //                AddIntervalAsync(x);
        //            }
        //        }
        //    });
        //    return id;

        //}

        public async static Task<Opomnik> GetOpomnikById(int id)
        {
            var conn = DbConnectionAsync;
            var query = conn.Table<Opomnik>().Where(v => v.Id == id);
            Opomnik ok = new Opomnik();

            await query.ToListAsync().ContinueWith((t) =>
            {
                foreach (var x in t.Result)
                {
                    x.Zdravilo1 = GetFirstZdraviloById(x.IdZdravilo);
                    x.Intervali = GetIntervaleByZdraviloId(x.Id);
                    foreach (var y in x.Intervali)
                    {
                        y.SeznamTerminovZaAlarm = GetTermineByIntervalId(y.Id);
                    }
                    ok = x;
                }
            });

            return ok;

        }

        public static int ShraniOpomnik(Opomnik opomnik)
        {
            int idOpomnika = -1;
            using (var db = DbConnection)
            {
                db.Insert(opomnik);

                idOpomnika = GetLastOpomnikId();
                foreach (var x in opomnik.Intervali)
                {
                    x.OpomnikId = idOpomnika;
                    db.Insert(x);

                    int idIntervala = GetLastIntervalkId();

                    foreach (var y in x.SeznamTerminovZaAlarm)
                    {
                        y.IntervalId = idIntervala;
                        db.Insert(y);
                    }
                }
                
            }

            return idOpomnika;
        }
        

        private static List<Termin> GetTermineByIntervalId(int id)
        {
            using (var db = DbConnection)
            {
                return db.Table<Termin>().Where(v => v.IntervalId == id).ToList();
            }
        }

        private static List<Interval> GetIntervaleByZdraviloId(int id)
        {
            using (var db = DbConnection)
            {
                return db.Table<Interval>().Where(v => v.OpomnikId == id).ToList();
            }
        }

        static int GetLastOpomnikId()
        {
            using (var db = DbConnection)
            {
                var x = db.Query<Opomnik>("SELECT * FROM Opomnik ORDER BY id DESC LIMIT 1");

                return x[0].Id;
            }
        }
        static int GetLastIntervalkId()
        {
            using (var db = DbConnection)
            {
                var x = db.Query<Interval>("SELECT * FROM Interval ORDER BY id DESC LIMIT 1");

                return x[0].Id;
            }
        }

    }


}
