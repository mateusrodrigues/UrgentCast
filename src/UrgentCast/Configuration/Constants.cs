using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrgentCast.Configuration
{
    public static class Constants
    {
        public static string GetIdentityUrl(string environment)
        {
            if (environment.Equals("Development", StringComparison.CurrentCultureIgnoreCase))
            {
                return "https://localhost:5000";
            }
            else if (environment.Equals("Staging", StringComparison.CurrentCultureIgnoreCase))
            {
                return "";
            }
            else if (environment.Equals("Production", StringComparison.CurrentCultureIgnoreCase))
            {
                return "";
            }
            else
            {
                return null;
            }
        }
    }
}