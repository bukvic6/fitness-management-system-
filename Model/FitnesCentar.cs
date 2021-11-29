using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Model
{
    class FitnesCentar
    {
        private string _ID;

        public string Id
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string _NazivCentra;

        public string NazivCentra
        {
            get { return _NazivCentra; }
            set { _NazivCentra = value; }
        }

        public string _Adresa;

        public string Adresa
        {
            get { return _Adresa; }
            set { _Adresa = value; }
        }
        public override string ToString()
        {
            return Id + ";" + NazivCentra + ";" + Adresa;
        }

    }
}
