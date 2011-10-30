using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace GiveCRM.Web.Services
{
    public class AuthenticationService :IAuthenticationService
    {
        public void SetAuthorizationCredentials(string username, bool persistCredentials)
        {
            FormsAuthentication.SetAuthCookie(username, persistCredentials);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}