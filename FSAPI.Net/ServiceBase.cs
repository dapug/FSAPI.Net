// ----------------------------------------------------------------------
// <copyright file="ServiceBase.cs" company="nGenesis, LLC">
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

namespace FSAPI
{
    public abstract partial class ServiceBase
    {
        //user agent consists of  API version AND the App version.   user inputs their app info.  we append our API info
        internal const string userAgentFSAPI = "nGenesis FSAPINet2.0/v2.99;";

        internal string userAgentComplete;

        //internal string contributorRef = "";


        #region properties

        protected CookieContainer cookies;
        /// <summary>
        /// Gets or sets the collection of cookies to use for authenticated requests to FamilySearch.
        /// </summary>
        public CookieContainer Cookies
        {
            get { return cookies; }
            set { cookies = value; }
        }

        protected string accessToken;
        /// <summary>
        /// Gets or sets the Access Token to use for authenticated requests to FamilySearch.
        /// </summary>
        public string AccessToken
        {
            get { return accessToken; }
            set { accessToken = value; }
        }

        protected string userAgent;
        /// <summary>
        /// Gets or sets the name of the application and version that is accessing FamilySearch. Use this format:  "MyApp/v1.0"
        /// </summary>
        public string UserAgent
        {
            get { return userAgent; }
            set
            {
                if (value != null && value.Length > 0)
                {
                    userAgent = value;
                    this.userAgentComplete = userAgentFSAPI + userAgent;// +this.contributorRef;
                }
                else
                    throw new Exception("A required property \"UserAgent\" was not found");
            }
        }

        private FSServer fsMode = FSServer.Sandbox;
        /// <summary>
        /// Gets or Sets which server to use to process all API requests
        /// </summary>
        public FSServer Server
        {
            get { return fsMode; }
            set
            {
                fsMode = value;
                if (fsMode == FSServer.Partner)
                    FSUri.ActiveDomain = FSUri.PartnerDomain;
                else if (fsMode == FSServer.Sandbox)
                    FSUri.ActiveDomain = FSUri.SandboxDomain;
                else if (fsMode == FSServer.Staging)
                    FSUri.ActiveDomain = FSUri.StagingDomain;
                else if (fsMode == FSServer.Production)
                    FSUri.ActiveDomain = FSUri.ProductionDomain;

                //FSUri.Reset();
            }
        }

        #endregion

        protected string GetRequest(string uri)
        {
            WebClient client = new WebClient();
            //client.

            return null;
        }

        protected ResponseInfo PostRequest(string uri, string data, string contentType)
        {
            Net.SuperWebClient wc = new Net.SuperWebClient();
            wc.Headers = new System.Net.WebHeaderCollection();
            wc.Headers.Add("Content-Type", contentType);

            string result = wc.UploadString(new Uri(uri), "POST", data);

            return new ResponseInfo(wc.StatusCode, wc.StatusDescription, result);
        }

        
    }
}
