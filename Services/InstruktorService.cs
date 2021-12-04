using SR22_2020_POP2021.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Services
{
    class InstruktorService : IInstruktorService
    {

        public void ReadUsers(string filename)
        {
            Util.Instance.Instruktori = new ObservableCollection<Instruktor>();
            using(StreamReader file = new StreamReader(@"../../Resources/" + filename))
            {
                string line;

                while ((line = file.ReadLine())!= null)
                {
                    string[] instruktorIzFajla = line.Split(';');
                    RegistrovaniKorisnik registrovaniKorisnik = Util.Instance.Korisnici.ToList().Find(korisnik => korisnik.Email.Equals(instruktorIzFajla[0]));

                    Instruktor instruktor = new Instruktor
                    {
                        Korisnik = registrovaniKorisnik,
                    };
                    Util.Instance.Instruktori.Add(instruktor);
                }
            }
        }

        public void SaveUsers(string filename)
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/" + filename))
            {
                foreach (Instruktor instruktor in Util.Instance.Instruktori)
                {
                    file.WriteLine(instruktor.InstruktorZaUpisUFajl());
                }
            }
        }
    }
}
