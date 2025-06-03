using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Mobile.Services
{
    public static class ConfigService
    {
        public static string GetConfigService()
        {
#if ANDROID
                return "https://10.10.1.55:7147/api/";
#else
            return "http://localhost:7147/api/countries";
#endif
        }
    }
}

