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
        public static string CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;
Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static readonly Util instance = new Util();
        private IUserService userService;
        private IInstruktorService instruktorService;
        private IAdresaService adresaService;
        

        private Util()
        {
            userService = new UserService();
            instruktorService = new InstruktorService();
            adresaService = new AdresaService();
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
        public ObservableCollection<Adresa> Adrese { get; set; }
        
        
        public void Initialize()
        {
            Korisnici = new ObservableCollection<RegistrovaniKorisnik>();
            Instruktori = new ObservableCollection<Instruktor>();
            Adrese = new ObservableCollection<Adresa>();
            

        }
        public int SacuvajEntitet(Object obj)
        {
            if (obj is RegistrovaniKorisnik)
            {
               return userService.SaveUsers(obj);
            }
            else if(obj is Adresa)
            {
                return adresaService.SaveAdresa(obj);
            }
            else if (obj is Instruktor)
            {
               return instruktorService.SaveUser(obj);
            }
            return -1;
        }

        public void CitanjeEntiteta(string filename)
        {
            if (filename.Contains("korisnici"))
            {
                userService.ReadUsers();
            }
            else if (filename.Contains("instruktori"))
            {
                instruktorService.ReadUsers();
            }
            else if (filename.Contains("adrese"))
            {
                adresaService.ReadAdresa();
            }
           
            
        }
        public void BrisanjeKorisnika(string email)
        {
            userService.DeleteUser(email);
        }




    }
}
