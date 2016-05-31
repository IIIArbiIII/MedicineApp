using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using MedicineApp.Classi;
using SQLite.Net;
using SQLite.Net.Attributes;

namespace MedicineApp
{
    class Opomnik
    {
        private int id;
        private int _idZdravilo;
        private DateTime _zacetekJemanja;
        private DateTime _konecJemanja;
        private string _melodija;


        private List<Interval> _interval;
        private Zdravilo _zdravilo;

        //Lastnosti
        #region 

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime ZacetekJemanja
        {
            get { return _zacetekJemanja; }
            set { _zacetekJemanja = value; }
        }

        public DateTime KonecJemanja
        {
            get { return _konecJemanja; }
            set { _konecJemanja = value; }
        }

        //public int DozaZdravila
        //{
        //    get { return _dozaZdravila; }
        //    set { _dozaZdravila = value; }
        //}

        //public bool Vibracija
        //{
        //    get { return _vibracija; }
        //    set { _vibracija = value; }
        //}

        public string Melodija
        {
            get { return _melodija; }
            set { _melodija = value; }
        }

        [Ignore]
        public List<Interval> Intervali
        {
            get { return _interval; }
            set { _interval = value; }
        }

        public int IdZdravilo
        {
            get { return _idZdravilo; }
            set { _idZdravilo = value; }
        }

        [Ignore]
        public Zdravilo Zdravilo1
        {
            get { return _zdravilo; }
            set { _zdravilo = value; }
        }

        #endregion
        public void IzracunajCasovneTermine()
        {
            var x = IzracunjaTermineZaIntervale();

            KonecJemanja = x[x.Count() - 1];
        }

        List<DateTime> IzracunjaTermineZaIntervale()
        {
            int steviloAlarmov = 0;
            DateTime dt = ZacetekJemanja;

            var seznamUrZaToastNotificatione = new List<DateTime>();

            foreach (var x in Intervali)
            {
                steviloAlarmov = x.Dan * (24 / x.Ure);
                for (int i = 0; i < steviloAlarmov; i++)
                {
                    Termin t = new Termin();

                    dt = dt.AddHours(double.Parse(x.Ure.ToString()));
                    t.TerminZazvonenje = dt;
                    t.UserChecked = false;

                    x.SeznamTerminovZaAlarm.Add(t);
                    seznamUrZaToastNotificatione.Add(dt);
                }
            }

            return seznamUrZaToastNotificatione;
        } 
    }

   
}
