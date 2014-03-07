using FSAPI.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Threading;

namespace FSAPI.Test
{
    /// <summary>
    ///This is a test class for BasicTest and is intended
    ///to contain all BasicTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BasicAuthTest
    {
        AutoResetEvent _TestTrigger;
        string appKey = "";  //////////   FamilySearch App Key here /////////////////
        string userAgent = "myTest/v1";
        FSServer server = FSServer.Partner;

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
        ///A test for Login
        ///</summary>
        [TestMethod()]
        public void LoginTest()
        {
            BasicAuth target = new BasicAuth(userAgent, server);
            string username = ""; 
            string password = ""; 
            BasicAuthEventArgs expected = null; // TODO: Initialize to an appropriate value
            BasicAuthEventArgs actual;
            actual = target.Login(username, password, appKey);
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Result);
            Assert.IsNotNull(actual.Result.Session);
            Assert.IsNotNull(actual.Result.Session.Id);
            if( actual.Result.Session.Id != null )
                Debug.WriteLine("AccessToken: " + actual.Result.Session.Id);

            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LoginAsync
        ///</summary>
        [TestMethod()]
        public void LoginAsyncTest()
        {
            this._TestTrigger = new AutoResetEvent(false);

            BasicAuth target = new BasicAuth(userAgent, server);
            string username = "";
            string password = "";
            target.LoginCompleted += new BasicAuth.LoginCompletedHandler(target_LoginCompleted);
            target.LoginAsync(username, password, appKey);

            this._TestTrigger.WaitOne();
        }

        void target_LoginCompleted(object sender, BasicAuthEventArgs e)
        {
            BasicAuthEventArgs actual = e;

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Result);
            Assert.IsNotNull(actual.Result.Session);
            Assert.IsNotNull(actual.Result.Session.Id);
            if (actual.Result.Session.Id != null)
                Debug.WriteLine("AccessToken: " + actual.Result.Session.Id);


            this._TestTrigger.Set();
        }

        /// <summary>
        ///A test for Logout
        ///</summary>
        [TestMethod()]
        public void LogoutTest()
        {
            BasicAuth target = new BasicAuth(userAgent, server); // TODO: Initialize to an appropriate value
            string accessToken = "USYSD1266FEEF8254D936098966E88854CCC_naci-045-033.d.usys.fsglobal.net"; // TODO: Initialize to an appropriate value
            var expected = System.Net.HttpStatusCode.OK; // TODO: Initialize to an appropriate value
            BasicAuthEventArgs actual;
            actual = target.Logout(accessToken);
            Assert.AreEqual(expected, actual.StatusCode);
        }

        /// <summary>
        ///A test for LogoutAsync
        ///</summary>
        [TestMethod()]
        public void LogoutAsyncTest()
        {
            this._TestTrigger = new AutoResetEvent(false);

            BasicAuth target = new BasicAuth(userAgent, server); // TODO: Initialize to an appropriate value
            target.LogoutCompleted += new BasicAuth.LogoutCompletedHandler(target_LogoutCompleted);
            string accessToken = "";
            target.LogoutAsync(accessToken);

            this._TestTrigger.WaitOne();
        }

        void target_LogoutCompleted(object sender, BasicAuthEventArgs e)
        {
            var expected = System.Net.HttpStatusCode.OK;
            var actual = e;

            Assert.AreEqual(expected, actual.StatusCode);

            this._TestTrigger.Set();
        }
    }
}
