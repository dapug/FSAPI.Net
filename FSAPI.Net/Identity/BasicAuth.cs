// ----------------------------------------------------------------------
// <copyright file="BasicAuth.cs" company="nGenesis, LLC">
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
using Schema = FSAPI.Identity.V2.Schema;
using System.Net;

namespace FSAPI.Identity
{
    public class BasicAuth : ServiceBase
    {
        public delegate void LoginCompletedHandler(object sender, BasicAuthEventArgs e);
        public event LoginCompletedHandler LoginCompleted;

        public delegate void LogoutCompletedHandler(object sender, BasicAuthEventArgs e);
        public event LogoutCompletedHandler LogoutCompleted;

        private Net.SuperWebClient client = new Net.SuperWebClient();

        #region Constructors

        /// <summary>
        /// Instantiates the Service
        /// </summary>
        public BasicAuth()
        {
            userAgentComplete = userAgentFSAPI;// +";" + this.contributorRef;

            client.UserAgent = userAgentComplete;
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
        }

        /// <summary>
        /// Instantiates the Service
        /// </summary>
        /// <param name="userAgent">Name of the application and version that is accessing FamilySearch. Use this format:  "MyApp/v1.0"</param>
        /// <param name="server">Indicate which server to use to process all API requests</param>
        public BasicAuth(string userAgent, FSServer server)
        {
            this.UserAgent = userAgent; 
            this.Server = server;
            client.UserAgent = userAgentComplete;
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
        }


        /// <summary>
        /// Instantiates the Service and sets the Cookies to use for Authenticated requests to FamilySearch
        /// </summary>
        /// <param name="cookies">Collection of cookies to use for authenticated requests to FamilySearch.</param>
        /// <param name="userAgent">Name of the application and version that is accessing FamilySearch. Use this format:  "MyApp/v1.0"</param>
        public BasicAuth(CookieContainer cookies, string userAgent)
        {
            this.cookies = cookies;
            this.UserAgent = userAgent; //intentionally using the Public version of this.UserAgent
            client.UserAgent = userAgentComplete;
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
        }

        /// <summary>
        /// Instantiates the Service and sets the Access Token to use for Authenticated requests to FamilySearch
        /// </summary>
        /// <param name="accessToken">Access Token to use for authenticated requests to FamilySearch.</param>
        /// <param name="userAgent">Name of the application and version that is accessing FamilySearch. Use this format:  "MyApp/v1.0"</param>
        public BasicAuth(string accessToken, string userAgent)
        {
            this.AccessToken = accessToken;
            this.UserAgent = userAgent; //intentionally using the Public version of this.UserAgent
            client.UserAgent = userAgentComplete;
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
        }

        //Gets or Sets which server to use to process all API requests


        /// <summary>
        /// Instantiates the Service and sets the Cookies to use for Authenticated requests to FamilySearch
        /// </summary>
        /// <param name="cookies">Collection of cookies to use for authenticated requests to FamilySearch.</param>
        /// <param name="userAgent">Name of the application and version that is accessing FamilySearch. Use this format:  "MyApp/v1.0"</param>
        /// <param name="server">Indicate which server to use to process all API requests</param>
        public BasicAuth(CookieContainer cookies, string userAgent, FSServer server)
        {
            this.cookies = cookies;
            this.UserAgent = userAgent; //intentionally using the Public version of this.UserAgent
            this.Server = server;  
            client.UserAgent = userAgentComplete;
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
        }



        /// <summary>
        /// Instantiates the Service and sets the SessionId to use for Authenticated requests to FamilySearch
        /// </summary>
        /// <param name="accessToken">AccessToken to use for authenticated requests to FamilySearch.</param>
        /// <param name="userAgent">Name of the application and version that is accessing FamilySearch. Use this format:  "MyApp/v1.0"</param>
        /// <param name="server">Indicate which server to use to process all API requests</param>
        public BasicAuth(string accessToken, string userAgent, FSServer server)
        {
            this.AccessToken = accessToken;
            this.userAgent = userAgent;
            this.UserAgent = userAgent; //intentionally using the Public version of this.UserAgent
            this.Server = server;  //intentionally using the Public version of this.Mode
            client.UserAgent = userAgentComplete;
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Authenticate with FamilySearch using Basic Authentication
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="appKey">App Key (or Client ID)</param>
        /// <returns></returns>
        public BasicAuthEventArgs Login(string username, string password, string appKey)
        {
            bool valid = ValidateLogin(username, password, appKey);

            BasicAuthEventArgs ir = null;

            if (valid)
            {
                //Attempt Login to the system, get the response
                ir = AuthRequest(username, password, appKey, false);

                if (ir != null && ir.Result != null && ir.Result.Session != null)
                {
                    this.AccessToken = ir.Result.Session.Id;
                }
            }

            return ir;
        }

        /// <summary>
        /// Authenticate with FamilySearch using Basic Authentication asynchronously
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="appKey">App Key (or Client ID)</param>
        public void LoginAsync(string username, string password, string appKey)
        {
            bool valid = ValidateLogin(username, password, appKey);


            if (valid)
            {
                //Attempt Login to the system, get the response using Async, wherein the remaining steps of login are done by the DownloadStringCompleted handler
                AuthRequest(username, password, appKey, true);
            }
        }

        /// <summary>
        /// Log the given AccessToken (or SessionID) out of FamilySearch 
        /// </summary>
        /// <param name="accessToken">Access Token (or Session ID)</param>
        /// <returns></returns>
        public BasicAuthEventArgs Logout(string accessToken) 
        {
            return LogoutRequest(accessToken, false);
        }

        /// <summary>
        /// Log the given AccessToken (or SessionID) out of FamilySearch asynchronously
        /// </summary>
        /// <param name="accessToken">Access Token (or Session ID)</param>
        public void LogoutAsync(string accessToken)
        {
            LogoutRequest(accessToken, true);
        }

        #endregion

        #region Private Methods

        private BasicAuthEventArgs LogoutRequest(string accessToken, bool doAsync)
        {
            BasicAuthEventArgs result = null;

            string uri = FSUri.ActiveDomain + FSUri.IdentityV2Path + "/logout?sessionId=" + accessToken;

            string authResult = string.Empty;
            if (doAsync)
                client.DownloadStringAsync(new Uri(uri), false); //we are using a SINGLE event handler for both login and logout.  Setting state as "true" will indicate to the Completed handler to know what the DownloadString was for.
            else
                authResult = client.DownloadString(uri);

            return GetArgsFromAuthResponse(client.StatusCode, client.StatusDescription, authResult);
        }

        private BasicAuthEventArgs AuthRequest(string username, string password, string devKey, bool doAsync)
        {
            BasicAuthEventArgs result = null;
            client.Credentials = new NetworkCredential(username, password);
     
            string uri = FSUri.ActiveDomain + FSUri.IdentityV2Path + "/login?key=" + devKey;

            string authResult = string.Empty;
            if (doAsync)
                client.DownloadStringAsync(new Uri(uri), true); //we are using a SINGLE event handler for both login and logout.  Setting state as "true" will indicate to the Completed handler to know what the DownloadString was for.
            else
                authResult = client.DownloadString(uri);

            return GetArgsFromAuthResponse(client.StatusCode, client.StatusDescription, authResult);
        }

        private void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var identity = ParseIdentityV1Response(e.Result);
            if (identity != null && identity.Session != null)
            {
                this.AccessToken = identity.Session.Id;
            }

            //we stored a bool as a state object to know whether this is for Login.  Else its for Logout
            //this is because a) the only methods in this class using this web client are login and logout and
            //                b) both login and logout use the same args and return object type
            if (e.UserState != null && (bool)e.UserState == true) 
            {
                if (LoginCompleted != null)
                    LoginCompleted(this, new BasicAuthEventArgs(client.StatusCode, client.StatusDescription, identity));
            }
            else
            {
                if (LogoutCompleted != null)
                    LogoutCompleted(this, new BasicAuthEventArgs(client.StatusCode, client.StatusDescription, identity));
            }
        }

        private BasicAuthEventArgs GetArgsFromAuthResponse(HttpStatusCode statusCode, string statusDescription, string authResult)
        {
            Schema.Identity id = null;

            //parse the result
            id = ParseIdentityV1Response(authResult);

            //create and return args containing the result
            return new BasicAuthEventArgs(statusCode, statusDescription, id);
        }

        private Schema.Identity ParseIdentityV1Response(string xmlData)
        {
            Schema.Identity identity = null;

            if (xmlData != null && xmlData.Length > 0)
            {
                try
                {
                    //deserialize the xml into our FamilySearch object
                    identity = new Schema.Identity();
                    identity = (Schema.Identity)XmlSerlializeHelper.Load(identity, new System.IO.StringReader(xmlData));
                }
                catch (System.Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return identity;
        }

        private bool ValidateLogin(string username, string password, string devKey)
        {
            if (username == null || username.Length < 2 || password == null || password.Length < 2)
                throw new Exception("Username or Password is missing.");

            if (devKey == null || devKey.Length < 2)
                throw new Exception("Developer Key is missing or invalid.");

            if (this.userAgent == null || this.userAgent.Length < 3)
                throw new Exception("UserAgent is a required property that is missing.");

            this.userAgentComplete = userAgentFSAPI + userAgent;

            return true;
        }

        #endregion
    }
}
