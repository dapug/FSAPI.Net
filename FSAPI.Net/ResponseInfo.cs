using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FSAPI
{
    public class ResponseInfo : AsyncCompletedFullEventArgs
    {
        public string Result { get; internal set; }

        public ResponseInfo(Exception error, bool cancelled, object userState, HttpStatusCode statusCode, string statusDescription, string result)
            : base(error, cancelled, userState, statusCode, statusDescription)
        {
            this.Result = result;
        }

        public ResponseInfo(HttpStatusCode statusCode, string statusDescription, string result)
            : base(null, false, null, statusCode, statusDescription)
        {
            this.Result = result;
        }
    }
}
