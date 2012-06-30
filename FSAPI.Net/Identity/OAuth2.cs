// ----------------------------------------------------------------------
// <copyright file="OAuth2.cs" company="nGenesis, LLC">
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

namespace FSAPI.Identity
{
    public class OAuth2 : ServiceBase
    {
        public event GetAccessTokenCompletedEventHandler GetAccessTokenCompleted;
        public delegate void GetAccessTokenCompletedEventHandler(object sender, OAuth2AccessTokenEventArgs e);

        Net.SuperWebClient client;
        string contentType = "application/x-www-form-urlencoded";

        #region Constructors

        /// <summary>
        /// Instantiates the Service
        /// </summary>
        public OAuth2()
        {
            userAgentComplete = userAgentFSAPI;// +";" + this.contributorRef;

            client = new Net.SuperWebClient();
            client.Headers = new System.Net.WebHeaderCollection();
            client.Headers.Add("Content-Type", contentType);
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);
        }



        /// <summary>
        /// Instantiates the Service
        /// </summary>
        /// <param name="userAgent">Name of the application and version that is accessing FamilySearch. Use this format:  "MyApp/v1.0"</param>
        /// <param name="server">Indicate which server to use to process all API requests</param>
        public OAuth2(string userAgent, FSServer server)
        {
            this.UserAgent = userAgent; 
            this.Server = server;
            client = new Net.SuperWebClient();
            client.Headers = new System.Net.WebHeaderCollection();
            client.Headers.Add("Content-Type", contentType);
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);
        }

        /// <summary>
        /// Instantiates the Service and sets the SessionId to use for Authenticated requests to FamilySearch
        /// </summary>
        /// <param name="accessToken">AccessToken to use for authenticated requests to FamilySearch.</param>
        /// <param name="userAgent">Name of the application and version that is accessing FamilySearch. Use this format:  "MyApp/v1.0"</param>
        /// <param name="server">Indicate which server to use to process all API requests</param>
        public OAuth2(string accessToken, string userAgent, FSServer server)
        {
            this.AccessToken = accessToken;
            this.userAgent = userAgent;
            this.UserAgent = userAgent; //intentionally using the Public version of this.UserAgent
            this.Server = server;
            client = new Net.SuperWebClient();
            client.Headers = new System.Net.WebHeaderCollection();
            client.Headers.Add("Content-Type", contentType);
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(client_UploadStringCompleted);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the authorization Uri to refer the user to in their Browser to complete the Authentication process
        /// </summary>
        /// <param name="clientID">Client ID (or Dev Key)</param>
        /// <param name="redirectUri">Uri that the OAuth system should return the auth code to</param>
        /// <param name="userState">Arbitrary value for the client app to track this request (optional)</param>
        /// <param name="language">Language that the sign in screen should render in for the user (optional, default "en")</param>
        /// <returns></returns>
        public string GetAuthUrl(string clientID, string redirectUri, string userState, string language)
        {
            string authUrl = FSUri.ActiveDomain + FSUri.OAuth2_AuthPath + "?response_type=code&client_id=" + clientID;

            if (redirectUri != null && redirectUri.Length > 0)
                authUrl += "&redirect_uri=" + redirectUri;

            if (userState != null && userState.Length > 0)
                authUrl += "&state=" + userState;

            if (language != null && language.Length > 0)
                authUrl += "&lng=" + language;

            return authUrl;
        }

        /// <summary>
        /// Gets an Access Token to use for all subsequent calls to the API's in the system
        /// </summary>
        /// <param name="clientID">Client ID (or Dev Key)</param>
        /// <param name="authCode">Authorization code returned to the client after the user signed in</param>
        /// <returns></returns>
        public OAuth2AccessTokenEventArgs GetAccessToken(string clientID, string authCode)
        {
            return GetAccessToken(clientID, authCode, false);
        }

        /// <summary>
        /// Gets an Access Token asynchronously, to use for all subsequent calls to the API's in the system
        /// </summary>
        /// <param name="clientID">Client ID (or Dev Key)</param>
        /// <param name="authCode">Authorization code returned to the client after the user signed in</param>
        public void GetAccessTokenAsync(string clientID, string authCode)
        {
            GetAccessToken(clientID, authCode, true);
        }

        #endregion

        #region Private Methods

        private OAuth2AccessTokenEventArgs GetAccessToken(string clientID, string authCode, bool doAsync)
        {
            string postData = ConstructAccessRequestBody(clientID, authCode);

            string uri = FSUri.ActiveDomain + FSUri.OAuth2_TokenPath;

            string authResult = string.Empty;
            if (doAsync)
                client.UploadStringAsync(new Uri(uri), "POST", postData);
            else
                authResult = client.UploadString(uri, "POST", postData);

            return GetArgsFromAuthResponse(client.StatusCode, client.StatusDescription, authResult);
        }

        private void client_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            var info = ParseAccessTokenResponse(e.Result);

            if (GetAccessTokenCompleted != null)
                GetAccessTokenCompleted(this, new OAuth2AccessTokenEventArgs(client.StatusCode, client.StatusDescription, info));
        }

        private OAuth2AccessTokenEventArgs GetArgsFromAuthResponse(HttpStatusCode statusCode, string statusDescription, string authResult)
        {
            Identity.AccessTokenInfo info = null;

            //parse the result
            info = ParseAccessTokenResponse(authResult);

            //create and return args containing the result
            return new OAuth2AccessTokenEventArgs(statusCode, statusDescription, info);
        }

        private string ConstructAccessRequestBody(string clientID, string authCode)
        {
            if (authCode == null || authCode.Length <= 0 || clientID == null && clientID.Length <= 0)
                throw new Exception("Auth Code and ClientID are both required for an Access Token request");

            string postData =
                        "client_id=" + clientID +
                //"&client_secret=" + clientSecret +
                        "&code=" + authCode +
                        "&grant_type=authorization_code";

            return postData;
        }

        private Identity.AccessTokenInfo ParseAccessTokenResponse(string data)
        {
            if (data != null && data.Length > 0)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Identity.AccessTokenInfo>(data);
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
