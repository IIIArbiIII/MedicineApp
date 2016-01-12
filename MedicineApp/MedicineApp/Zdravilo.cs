using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineApp
{
    class Zdravilo
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string naziv;

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        private DateTime rokTrajanja;

        public DateTime RokTrajanja
        {
            get { return rokTrajanja; }
            set { rokTrajanja = value; }
        }

        private double kolicina;

        public double Kolicina
        {
            get { return kolicina; }
            set { kolicina = value; }
        }

        public Zdravilo()
        {

        }
    }
}
