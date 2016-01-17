using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace MedicineApp
{
    class Skrbnik
    {
        public Skrbnik()
        {}

        public Skrbnik(string ime, string priimek, string telefon, int geslo)
        {
            _ime = ime;
            _priimek = priimek;
            _telStevilka = telefon;
            _pin = geslo;
        }

        private int id;
        private string _ime;
        private string _priimek;
        private string _telStevilka;
        private int _pin;

        //Lastnosti
        #region 

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        public string Priimek
        {
            get { return _priimek; }
            set { _priimek = value; }
        }

        public string TelStevilka
        {
            get { return _telStevilka; }
            set { _telStevilka = value; }
        }

        public int Pin
        {
            get { return _pin; }
            set { _pin = value; }
        }

        #endregion
    }
}
