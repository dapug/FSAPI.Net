using FSAPI.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace FSAPI.Test
{
    
    
    /// <summary>
    ///This is a test class for OAuth2Test and is intended
    ///to contain all OAuth2Test Unit Tests
    ///</summary>
    [TestClass()]
    public class OAuth2Test
    {
        AutoResetEvent _TestTrigger;
        string appKey = "";   //////////   FamilySearch App Key here /////////////////
        string userAgent = "myTest/v1";
        FSServer server = FSServer.Sandbox;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion



        /// <summary>
        ///A test for GetAuthUrl
        ///</summary>
        [TestMethod()]
        public void GetAuthUrlTest()
        {
            OAuth2 target = new OAuth2(userAgent, server);
            string clientID = appKey; // App Key
            string redirectUrl = ""; // TODO: Initialize to an appropriate value
            string userState = string.Empty; // TODO: Initialize to an appropriate value
            string language = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetAuthUrl(clientID, redirectUrl, userState, language);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAccessToken
        ///</summary>
        [TestMethod()]
        public void GetAccessTokenTest()
        {
            OAuth2 target = new OAuth2(userAgent, server);
            string clientID = appKey; // Dev Key
            string authCode = "88-36-13-9982-57-78-1373-105-119-121-56-153915-9656-73102-32-47-91-104-11890-118-37-1811281-123";
            OAuth2AccessTokenEventArgs expected = null; // TODO: Initialize to an appropriate value
            OAuth2AccessTokenEventArgs actual;
            actual = target.GetAccessToken(clientID, authCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAccessTokenAsync
        ///</summary>
        [TestMethod()]
        public void GetAccessTokenAsyncTest()
        {
            this._TestTrigger = new AutoResetEvent(false);

            OAuth2 target = new OAuth2(userAgent, server);
            string clientID = appKey; // Dev Key
            string authCode = string.Empty; // TODO: Initialize to an appropriate value
            target.GetAccessTokenCompleted += new OAuth2.GetAccessTokenCompletedEventHandler(target_GetAccessTokenCompleted);
            target.GetAccessTokenAsync(clientID, authCode);
            
            this._TestTrigger.WaitOne();
        }

        void target_GetAccessTokenCompleted(object sender, OAuth2AccessTokenEventArgs e)
        {
            var expected = System.Net.HttpStatusCode.OK;
            var actual = e;

            Assert.AreEqual(expected, actual.StatusCode);

            this._TestTrigger.Set();
        }
    }
}
