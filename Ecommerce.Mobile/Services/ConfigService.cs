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
                return "https://192.168.10.9:7147/api/";
#else
            return "https://localhost:7147/api/";
#endif
        }
    }
}

