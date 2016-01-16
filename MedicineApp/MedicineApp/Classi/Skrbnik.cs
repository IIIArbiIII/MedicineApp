using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineApp
{
    class Skrbnik
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string ime;

        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }

        private string priimek;

        public string Priimek
        {
            get { return priimek; }
            set { priimek = value; }
        }

        private string telStevilka;

        public string TelStevilka
        {
            get { return telStevilka; }
            set { telStevilka = value; }
        }

        private int pin;

        public int Pin
        {
            get { return pin; }
            set { pin = value; }
        }

        public Skrbnik()
        {

        }
    }
}
