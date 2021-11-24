using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.MojiIzuzeci
{
    public class UserNotFoundExeption : Exception
    {
        public UserNotFoundExeption() : base()
        {

        }
        public UserNotFoundExeption(string message): base(message)
        {

        }
        public UserNotFoundExeption(string message, Exception innerExeption): base(message, innerExeption)
        {

        }
    }
}
