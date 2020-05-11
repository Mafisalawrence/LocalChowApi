using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalChow.Api.Settings
{
    public class AuthenticationConfiguration
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string SecreteKey { get; set; }
    }
}
