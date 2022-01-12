using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Services
{
    public interface IUserService
    {
        int SaveUsers(Object obj);
        void ReadUsers();
        void DeleteUser(string email);

        void IzmeniKorisnika(Object obj);
    }
}
