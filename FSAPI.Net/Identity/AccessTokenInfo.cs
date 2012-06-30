using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSAPI.Identity
{
    public class AccessTokenInfo
    {
        public string access_token { get; set; }
        //public string RefreshToken { get; set; }
        //public DateTime Expiration { get; set; }

        public string error { get; set; }
        public string error_description { get; set; }
    }
}
