using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FSAPI.Identity
{
    public class OAuth2AuthorizationEventArgs : AsyncCompletedFullEventArgs
    {
        public AuthInfo Result { get; internal set; }

        public OAuth2AuthorizationEventArgs(Exception error, bool cancelled, object userState, HttpStatusCode statusCode, string statusDescription, AuthInfo result)
            : base(error, cancelled, userState, statusCode, statusDescription)
        {
            this.Result = result;
        }
    }
}
