using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FSAPI.Identity
{
    public class OAuth2AccessTokenEventArgs : AsyncCompletedFullEventArgs
    {
        public AccessTokenInfo Result { get; internal set; }

        public OAuth2AccessTokenEventArgs(Exception error, bool cancelled, object userState, HttpStatusCode statusCode, string statusDescription, AccessTokenInfo result)
            : base(error, cancelled, userState, statusCode, statusDescription)
        {
            this.Result = result;
        }

        public OAuth2AccessTokenEventArgs(HttpStatusCode statusCode, string statusDescription, AccessTokenInfo result)
            : base(null, false, null, statusCode, statusDescription)
        {
            this.Result = result;
        }
    }
}
