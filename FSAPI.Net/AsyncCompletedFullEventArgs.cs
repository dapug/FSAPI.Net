using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ComponentModel;

namespace FSAPI
{
    public class AsyncCompletedFullEventArgs : AsyncCompletedEventArgs
    {
        public HttpStatusCode StatusCode { get; internal set; }
        public string  StatusDescription { get; internal set; }

        public AsyncCompletedFullEventArgs(Exception error, bool cancelled, object userState, HttpStatusCode statusCode, string statusDescription) 
            : base(error, cancelled, userState)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }
    }
}
