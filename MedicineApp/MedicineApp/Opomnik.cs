﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Attributes;

namespace MedicineApp
{
    class Opomnik
    {
        private int id;
        private string _naziv;
        private DateTime _zacetekJemanja;
        private DateTime _konecJemanja;
        private int _dozaZdravila;
        private bool _vibracija;
        private string _melodija;
        private Interval _interval;



        //Lastnosti
        #region 

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        [NotNull]
        public int Interval_ID { get; set; }

        public string Naziv
        {
            get { return _naziv; }
            set { _naziv = value; }
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

        public int DozaZdravila
        {
            get { return _dozaZdravila; }
            set { _dozaZdravila = value; }
        }

        public bool Vibracija
        {
            get { return _vibracija; }
            set { _vibracija = value; }
        }

        public string Melodija
        {
            get { return _melodija; }
            set { _melodija = value; }
        }


        #endregion

    }
}
