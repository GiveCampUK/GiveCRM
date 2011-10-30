namespace GiveCRM.Web.Services
{
    public interface IAuthenticationService
    {
        void SetAuthorizationCredentials(string username, bool persistCredentials);
        void SignOut();
    }
}