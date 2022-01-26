using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Services
{
    interface ITreningService
    {
        int SaveTrening(Object obj);
        void ReadTrening();
        void DeleteTrening(int id);
        void RezervisiTrening(int id);

        void IzmeniTrening(Object obj);

        void IzmeniTreningZaRezervaciju(Object obj);
        void OtkaziTrening(int id);
    }
}

