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
        public string jmbgPrijavljen = "";
        public static string CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;
Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
            


        }
        public int SacuvajEntitet(Object obj)
        {
            if (obj is RegistrovaniKorisnik)
            {
                return userService.SaveUsers(obj);
            }
            else if (obj is Instruktor)
            {
                return instruktorService.SaveUser(obj);
            }
            
            return -1;
        }
  


        public void CitanjeEntiteta()
        {
            
            instruktorService.ReadUsers();
            userService.ReadUsers();
            
            
        }
        public void BrisanjeKorisnika(string email)
        {
            userService.DeleteUser(email);
        }
        //public void izmena(Object obj)
        //{
        //    userService.Izmena(obj);
        //}






    }
}
