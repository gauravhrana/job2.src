using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Library.CommonServices.Utils
{

    public static class Misc
    {
#if(DEBUG)
        private static readonly string intranetPath = ConfigurationManager.AppSettings["DevIntranetPath"];
#else
        private static readonly string intranetPath = ConfigurationManager.AppSettings["IntranetPath"];
#endif

        public static string IntranetPath { get { return intranetPath; } }
    }
}
