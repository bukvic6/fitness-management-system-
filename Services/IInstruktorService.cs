﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR22_2020_POP2021.Services
{
    interface IInstruktorService
    {
        int SaveUser(Object obj);
        void ReadUsers();

    }
}
