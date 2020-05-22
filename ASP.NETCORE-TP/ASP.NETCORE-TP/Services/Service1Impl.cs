using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCORE_TP.Services
{
    public class Service1Impl : IServices1
    {
        public string GetValueInstance()
        {
            return "Bonjour : " + GetHashCode();
        }
    }
}
