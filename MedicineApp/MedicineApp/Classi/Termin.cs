using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace MedicineApp.Classi
{
    class Termin
    {
        public Termin(int intervalId, DateTime terminZaZvonenje)
        {
            IntervalId = intervalId;
            TerminZazvonenje = terminZaZvonenje;
        }

        public Termin(int intervalId, bool userChecked, DateTime terminZaZvonenje)
        {
            IntervalId = intervalId;
            UserChecked = userChecked;
            TerminZazvonenje = terminZaZvonenje;
        }

        public Termin()
        {
            UserChecked = false;
        }

        private int _Id;
        private DateTime _terminZazvonenje;
        private bool _userChecked;

        private int _IntervalId;

        //Lastnosti
        #region
        public bool UserChecked
        {
            get
            {
                return _userChecked;
            }

            set
            {
                _userChecked = value;
            }
        }

        public DateTime TerminZazvonenje
        {
            get { return _terminZazvonenje; }
            set { _terminZazvonenje = value; }
        }
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int IntervalId
        {
            get
            {
                return _IntervalId;
            }

            set
            {
                _IntervalId = value;
            }
        }

        #endregion
    }
}
