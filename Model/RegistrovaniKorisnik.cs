using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Model
{
    [Serializable]
    public class RegistrovaniKorisnik

    {

        private int _id;

        public int Id
        {
	        get { return _id;}
	        set { _id = value;}
        }

        private string _ime;

        public string Ime
        {
            get { return _ime; }
            set { _ime = value; }
        }

        private string _prezime;

        public string Prezime
        {
            get { return _prezime; }
            set { _prezime = value; }
        }
        private string _lozinka;

        public string Lozinka
        {
            get { return _lozinka; }
            set { _lozinka = value; }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _JMBG;

        public string JMBG
        {
            get { return _JMBG; }
            set { _JMBG = value; }
        }

        private Adresa _adresa;

        public Adresa Adresa
        {
            get { return _adresa; }
            set { _adresa = value; }
        }

        private EPol _pol;

        public EPol Pol
        {
            get { return _pol; }
            set { _pol = value; }
        }

        private ETipKorisnika _tipKorisnika;

        public ETipKorisnika TipKorisnika
        {
            get { return _tipKorisnika; }
            set { _tipKorisnika = value; }
        }

        private bool _aktivan;
        

        public bool Aktivan
        {
            get { return _aktivan; }
            set { _aktivan = value; }
        }
        
        public RegistrovaniKorisnik(string ime, string prezime, string jmbg, string email, string lozinka, ETipKorisnika tip, EPol pol, Adresa adresa) {
            _ime = ime;
            _prezime = prezime;
            _JMBG = jmbg;
            _email = email;
            _tipKorisnika = tip;
            _aktivan = true;
            _lozinka = lozinka;
            _pol = pol;
            _adresa = adresa;
         }
        public RegistrovaniKorisnik() { }
        

        public RegistrovaniKorisnik(int id, string ime, string prezime, ETipKorisnika tip, string jmbg, string email, bool aktivan, string lozinka, Adresa adresa)
        {
            _id = id;
            _ime = ime;
            _prezime = prezime;
            _JMBG = jmbg;
            _email = email;
            _tipKorisnika = tip;
            _aktivan = aktivan;
            _lozinka = lozinka;

            _adresa = adresa;

        }

        public override string ToString()
        {
            return "Ja sam " + Ime + "Prezime " + "tip " + TipKorisnika + "email je " + Adresa;
        }
        public string KorisnikZaUpisUFajl()
        {
            return Ime + ";" + Prezime + ";" + Email + ";" + Lozinka + ";" + JMBG + ";" + Adresa + ";" + Pol + ";" + TipKorisnika + ";" + Aktivan; 
        }
        public RegistrovaniKorisnik Clone()
        {
            RegistrovaniKorisnik kopija = new RegistrovaniKorisnik();
            kopija.Ime = Ime;
            kopija.Prezime = Prezime;
            kopija.Email = Email;
            kopija.Pol = Pol;
            kopija.Lozinka = Lozinka;
            kopija.Adresa = Adresa;
            kopija.JMBG = JMBG;
            kopija.TipKorisnika = TipKorisnika;
            kopija.Aktivan = Aktivan;

            return kopija;
        }












    }
}
