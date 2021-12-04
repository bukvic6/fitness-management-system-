using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Model
{
    [Serializable]
    public class Instruktor
    {
        
        private RegistrovaniKorisnik _korisnik;

        public RegistrovaniKorisnik Korisnik
        {
            get { return _korisnik; }
            set { _korisnik = value; }
        }
        public override string ToString()
        {
            return "Ja sam instruktor " + _korisnik.Ime + "a moj mejl je " + _korisnik.Email;
        }
        public string InstruktorZaUpisUFajl()
        {
            return Korisnik.Email;
        }

    }
}
