using SR22_2020_POP2021.Model;
using SR22_2020_POP2021.MojiIzuzeci;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Services
{
    public class UserService : IUserService
    {
        public void DeleteUser(string email)
        {
            RegistrovaniKorisnik registrovaniKorisnik = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.Email.Equals(email));
            if (registrovaniKorisnik == null)
            {
                throw new UserNotFoundExeption($"Ne postoji korisnik sa tim emailom");
            }
            registrovaniKorisnik.Aktivan = false;
            Console.WriteLine("Usmesno obrisan korisnik sa emailom: " + email);
        }

        public void SaveUsers(string filename)
        {
            using(StreamWriter file = new StreamWriter(@"../../Resources/" + filename))
            {
                foreach(RegistrovaniKorisnik registrovaniKorisnik in Util.Instance.Korisnici)
                {
                    file.WriteLine(registrovaniKorisnik.KorisnikZaUpisUFajl());
                }
            }
        }

        public void ReadUsers(string filename)
        {
            Util.Instance.Korisnici = new ObservableCollection<RegistrovaniKorisnik>();
            using (StreamReader file = new StreamReader(@"../../Resources/" + filename))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    string[] korisnikIzFajla = line.Split(';');

                    Enum.TryParse(korisnikIzFajla[5], out EPol pol);
                    Enum.TryParse(korisnikIzFajla[6], out ETipKorisnika tip);
                    Boolean.TryParse(korisnikIzFajla[7], out Boolean status);
                    RegistrovaniKorisnik registrovaniKorisnik = new RegistrovaniKorisnik
                    {
                        Ime = korisnikIzFajla[0],
                        Prezime = korisnikIzFajla[1],
                        Email = korisnikIzFajla[2],
                        Lozinka = korisnikIzFajla[3],
                        JMBG = korisnikIzFajla[4],
                        Pol = pol,
                        TipKorisnika = tip,
                        Aktivan = status
                     
                    };
                    Util.Instance.Korisnici.Add(registrovaniKorisnik);
                }
            }

        }
    }
}
