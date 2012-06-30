// ----------------------------------------------------------------------
// <copyright file="BasicAuthArgs.cs" company="nGenesis, LLC">
//     Copyright (c) 2012, nGenesis, LLC. 
//     All rights reserved. This program and the accompanying materials 
//     are made available under the terms of the Eclipse Public License v1.0 
//     which accompanies this distribution (Liscense.htm), and is available at 
//     http://www.eclipse.org/legal/epl-v10.html 
//     
//     Contributors: 
//         dapug - Initial author, core functionality
// </copyright>
//
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Schema = FSAPI.Identity.V2.Schema;

namespace FSAPI.Identity
{
    public class BasicAuthEventArgs : AsyncCompletedFullEventArgs
    {
        public Schema.Identity Result { get; internal set; }

        public BasicAuthEventArgs(Exception error, bool cancelled, object userState, HttpStatusCode statusCode, string statusDescription, Schema.Identity result)
            : base(error, cancelled, userState, statusCode, statusDescription)
        {
            this.Result = result;
        }

        public BasicAuthEventArgs(HttpStatusCode statusCode, string statusDescription, Schema.Identity result)
            : base(null, false, null, statusCode, statusDescription)
        {
            this.Result = result;
        }
    }
}
