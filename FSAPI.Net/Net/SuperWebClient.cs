// ----------------------------------------------------------------------
// <copyright file="SuperWebClient.cs" company="nGenesis, LLC">
//     Copyright (c) 2012, nGenesis, LLC. 
//     All rights reserved. This program and the accompanying materials are made available under the terms of the Eclipse Public License v1.0 
//     which accompanies this distribution (Liscense.htm), and is available at http://www.eclipse.org/legal/epl-v10.html 
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

namespace FSAPI.Net
{
    /// <summary>
    /// WebClient with extended functionality not supported in the generic WebClient
    /// </summary>
    public class SuperWebClient : WebClient
    {
        private WebRequest request = null;


        private CookieContainer cookieContainer;
        public CookieContainer CookieContainer
        {
            get { return cookieContainer; }
            set { cookieContainer = value; }
        }

        private string userAgent;
        public string UserAgent
        {
            get { return userAgent; }
            set { userAgent = value; }
        }

        private HttpStatusCode statusCode;
        public HttpStatusCode StatusCode
        {
            get { return statusCode; }
        }

        private string statusDesc;
        public string StatusDescription
        {
            get { return statusDesc; }
        }

        //private int timeout;
        //public int Timeout
        //{
        //    get { return timeout; }
        //    set { timeout = value; }
        //}

        public SuperWebClient()
        {
            cookieContainer = new CookieContainer();
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);

            if (request.GetType() == typeof(HttpWebRequest))
            {
                if (cookieContainer.Count > 0)
                    ((HttpWebRequest)request).CookieContainer = cookieContainer;

                if (!String.IsNullOrEmpty(userAgent))
                    ((HttpWebRequest)request).UserAgent = userAgent;
            }

            return request;
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            WebResponse response;

            try
            {
                response = base.GetWebResponse(request, result);
                if (response != null && response.GetType() == typeof(HttpWebResponse))
                {
                    this.statusCode = ((HttpWebResponse)response).StatusCode;
                    this.statusDesc = ((HttpWebResponse)response).StatusDescription;
                }
            }
            catch (WebException e)
            {
                //WebExceptions close out the original response object and we cant get at the status code, etc
                //but it includes the response in the exception, so lets access that to get the response info
                response = e.Response;

                HttpWebResponse httpResponse = (HttpWebResponse)response;
                this.statusCode = httpResponse.StatusCode;
                this.statusDesc = httpResponse.StatusDescription;
            }

            return response;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response;

            try
            {
                response = base.GetWebResponse(request);
                if (response != null && response.GetType() == typeof(HttpWebResponse))
                {
                    this.statusCode = ((HttpWebResponse)response).StatusCode;
                    this.statusDesc = ((HttpWebResponse)response).StatusDescription;
                }
            }
            catch (WebException e)
            {
                //WebExceptions close out the original response object and we cant get at the status code, etc
                //but it includes the response in the exception, so lets access that to get the response info
                response = e.Response;

                HttpWebResponse httpResponse = (HttpWebResponse)response;
                this.statusCode = httpResponse.StatusCode;
                this.statusDesc = httpResponse.StatusDescription;
            }

            return response;
        }

        //private void GetStatusCode() 
        //{ 
        //    //if (this.request == null) 
        //    //{ 
        //    //    throw (new InvalidOperationException("Unable to retrieve the status code. Request does not appear to be valid")); 
        //    //}

        //    if (this.request != null)
        //    {

        //        HttpWebResponse response = base.GetWebResponse(this.request) as HttpWebResponse;

        //        if (response != null)
        //        {
        //            this.statusCode = response.StatusCode;
        //            this.statusDesc = response.StatusDescription;
        //            this.statusCodeRetrieved = true;
        //        }
        //        //else
        //        //{
        //        //    throw (new InvalidOperationException("Unable to retrieve the status code. Response does not appear to be valid"));
        //        //}
        //    }
        //} 
    }
}
