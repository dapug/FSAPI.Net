using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSAPI.Identity
{
    public class AuthInfo
    {
        public string Code { get; set; }
        public string State { get; set; }
        public string Language { get; set; }

        public string Error { get; set; }
        public string ErrorDescription { get; set; }
    }
}
