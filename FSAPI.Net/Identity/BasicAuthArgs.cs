using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Schema = FSAPI.Identity.V1.Schema;

namespace FSAPI.Identity
{
    public class BasicAuthArgs : AsyncCompletedFullEventArgs
    {
        public Schema.Identity Result { get; internal set; }

        public BasicAuthArgs(Exception error, bool cancelled, object userState, HttpStatusCode statusCode, string statusDescription, Schema.Identity result)
            : base(error, cancelled, userState, statusCode, statusDescription)
        {
            this.Result = result;
        }
    }
}
