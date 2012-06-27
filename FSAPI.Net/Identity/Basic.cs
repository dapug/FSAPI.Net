using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Schema = FSAPI.Identity.V1.Schema;

namespace FSAPI.Identity
{
    public class Basic
    {
        public delegate void LoginCompletedHandler(object sender, BasicAuthArgs e);

        public event LoginCompletedHandler LoginCompleted;

        public delegate void LogoutCompletedHandler(object sender, BasicAuthArgs e);

        public event LogoutCompletedHandler LogoutCompleted;


        public bool Login(string username, string password, string devKey)
        {
            return false;
        }

        public void LoginAsync(string username, string password, string devKey)
        {
            Schema.Identity authInfo = new Schema.Identity();
            authInfo.Session.Id = "asdasd";

            if (LoginCompleted != null)
                LoginCompleted(this, new BasicAuthArgs(null,false,null, System.Net.HttpStatusCode.OK,null, authInfo));
        }

        public bool Logout() 
        {
            return false;
        }

        public void LogoutAsync()
        {
        }
    }
}
