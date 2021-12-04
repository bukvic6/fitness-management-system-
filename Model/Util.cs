using SR22_2020_POP2021.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Model
{
    public sealed class Util
    {
        private static readonly Util instance = new Util();
        private IUserService userService;
        private IInstruktorService instruktorService;

        private Util()
        {
            userService = new UserService();
            instruktorService = new InstruktorService();
        }

        static Util() { }
        public static Util Instance
        {
            get
            {
                return instance; 
            }

        }
        public ObservableCollection<RegistrovaniKorisnik> Korisnici { get; set; }
        public ObservableCollection<Instruktor> Instruktori { get; set; }
        public void Initialize()
        {
            Korisnici = new ObservableCollection<RegistrovaniKorisnik>();
            Instruktori = new ObservableCollection<Instruktor>();

            Adresa adresa = new Adresa
            {
                Grad = "Novi Sad",
                Drzava = "Srbija",
                Ulica = "ulica1",
                Broj = "22",
                ID = "1"
            };
            RegistrovaniKorisnik korisnik1 = new RegistrovaniKorisnik();
            korisnik1.Ime = "Pera";
            korisnik1.Prezime = "Peric";
            korisnik1.Email = "pera@gmail.com";
            korisnik1.JMBG = "121344";
            korisnik1.Lozinka = "peki";
            korisnik1.Pol = EPol.MUSKI;
            korisnik1.TipKorisnika = ETipKorisnika.ADMINISTRATOR;
            korisnik1.Aktivan = true;

            RegistrovaniKorisnik korisnik2 = new RegistrovaniKorisnik
            {
                Email = "mika@gmail.com",
                Ime = "mika",
                Prezime = "Mikic",
                JMBG = "132323",
                Lozinka = "zika",
                Pol = EPol.MUSKI,
                TipKorisnika = ETipKorisnika.INSTRUKTOR,
                Aktivan = true
            };
            Instruktor korisnik4 = new Instruktor
            {
                Korisnik = korisnik2,
            };
            Korisnici.Add(korisnik1);
            Korisnici.Add(korisnik2);
            Instruktori.Add(korisnik4);
        }
        public void SacuvajEntitet(string filename)
        {
            if (filename.Contains("korisnici"))
            {
                userService.SaveUsers(filename);
            }
            else if (filename.Contains("instruktori"))
            {
                instruktorService.SaveUsers(filename);
            }
        }
        public void CitanjeEntiteta(string filename)
        {
            if (filename.Contains("korisnici"))
            {
                userService.ReadUsers(filename);
            }
            else if (filename.Contains("instruktori"))
            {
                instruktorService.ReadUsers(filename);
            }
            
        }
        public void BrisanjeKorisnika(string email)
        {
            userService.DeleteUser(email);
        }




    }
}
