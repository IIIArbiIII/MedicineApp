using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineApp
{
    class Zdravilo
    {
        public Zdravilo()
        { }

        public Zdravilo(string naziv, DateTime roktrajanja, double kolicina)
        {
            _naziv = naziv;
            _rokTrajanja = roktrajanja;
            _kolicina = kolicina;
        }

        int _id;
        string _naziv;
        DateTime _rokTrajanja;
        double _kolicina;
        string _enota;

        //Lastnosti
        #region 

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Naziv
        {
            get { return _naziv; }
            set { _naziv = value; }
        }

        public DateTime RokTrajanja
        {
            get { return _rokTrajanja; }
            set { _rokTrajanja = value; }
        }

        public double Kolicina
        {
            get { return _kolicina; }
            set { _kolicina = value; }
        }

        public string Enota
        {
            get { return _enota; }
            set { _enota = value; }
        }

        #endregion

    }
}
