using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Model
{
    public class Trening
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }


        private DateTime _datum;

        public DateTime Datum
        {
            get { return _datum; }
            set { _datum = value; }
        }

        private DateTime _vremeTreninga;

        public DateTime VremeTreninga
        {
            get { return _vremeTreninga; }
            set { _vremeTreninga = value; }
        }

        private int _trajanjeTreninga;

        public int TrajanjeTreninga
        {
            get { return _trajanjeTreninga; }
            set { _trajanjeTreninga = value; }
        }

        private Boolean _statusTreninga;

        public Boolean MyProperty
        {
            get { return _statusTreninga; }
            set { _statusTreninga = value; }
        }




    }
}
