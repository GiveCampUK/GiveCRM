namespace GiveCRM.Web.Services
{
    using System.Web.Security;

    public class AuthenticationService : IAuthenticationService
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