using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomCulturesRegistrator.Enums
{
    public enum RegistrationResults
    {
        JustRegistered = 0,

        JustUnregistered = 1,

        AlreadyRegistered = 2,

        AlreadyUnregistered = 3,

        WrongCultureFromat = 4
    }
}
