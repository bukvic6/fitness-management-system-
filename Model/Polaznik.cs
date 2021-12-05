using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Model
{
    class Polaznik
    {
        private RegistrovaniKorisnik _korisnik;

        public RegistrovaniKorisnik Korisnik
        {
            get { return _korisnik; }
            set { _korisnik = value; }
        }
        public override string ToString()
        {
            return "Ja sam Polaznika " + _korisnik.Ime + "a moj mejl je " + _korisnik.Email;
        }
        public string PolaznikZaUpisUFajl()
        {
            return Korisnik.Email;
        }
    }
}
