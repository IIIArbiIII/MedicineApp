using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineApp
{
    class Interval
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int dan;

        public int Dan
        {
            get { return dan; }
            set { dan = value; }
        }

        private string ure;

        public string Ure
        {
            get { return ure; }
            set { ure = value; }
        }

        private string doza;

        public string Doza
        {
            get { return doza; }
            set { doza = value; }
        }

        public Interval()
        {

        }

    }
}
