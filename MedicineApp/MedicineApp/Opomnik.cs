using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineApp
{
    class Opomnik
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private Zdravilo naziv;

        public Zdravilo Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        private DateTime zacetekJemanja;

        public DateTime ZacetekJemanja
        {
            get { return zacetekJemanja; }
            set { zacetekJemanja = value; }
        }

        private DateTime konecJemanja;

        public DateTime KonecJemanja
        {
            get { return konecJemanja; }
            set { konecJemanja = value; }
        }

        private int dozaZdravila;

        public int DozaZdravila
        {
            get { return dozaZdravila; }
            set { dozaZdravila = value; }
        }


        private bool vibracija;

        public bool Vibracija
        {
            get { return vibracija; }
            set { vibracija = value; }
        }
        private string melodija;

        public string Melodija
        {
            get { return melodija; }
            set { melodija = value; }
        }

        public Opomnik()
        {

        }
    }
}
