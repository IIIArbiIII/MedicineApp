﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace MedicineApp
{
    class Interval
    {
        private int id;
        private int _dan;
        private int _ure;
        private int _doza;

        //Lastnosti
        #region 

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Dan
        {
            get { return _dan; }
            set { _dan = value; }
        }

        public int Ure
        {
            get { return _ure; }
            set { _ure = value; }
        }

        public int Doza
        {
            get { return _doza; }
            set { _doza = value; }
        }

        #endregion

    }
}
