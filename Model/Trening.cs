using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Model
{
    [Serializable]

    public class Trening
    {
        private int _id;

        public int Id
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

        private string _vremeTreninga;

        public string VremeTreninga
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

        private EStatusTreninga _statusTreninga;

        public EStatusTreninga StatusTreninga
        {
            get { return _statusTreninga; }
            set { _statusTreninga = value; }
        }

        private string _polaznikJmbg;

        public string PolaznikJmbg
        {
	        get { return _polaznikJmbg;}
	        set { _polaznikJmbg = value;}
        }

        private string _instruktorJmbg;

        
        public string InstruktorJmbg
        {
	        get { return _instruktorJmbg;}
	        set { _instruktorJmbg = value;}
        }
        private bool _aktivan;

        public Trening() { }

        public Trening(DateTime date, string text1, string text2, string text3, EStatusTreninga tip)
        {
            _datum = date;
            _vremeTreninga = text1;
            _statusTreninga = tip;
            _trajanjeTreninga = int.Parse(text2);
            _instruktorJmbg = text3;
        }

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }







    }
}
