using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class AuthenticationSettings
    {
        public string Secret { get; set; }
        public string JWTExpiredDays { get; set; }
        public string JWTIssuer { get; set; }
    }
}
