namespace GiveCRM.BusinessLogic
{
    public interface IMembershipService
    {
        bool ValidateUser(string userName, string password);
        bool CreateUser(string userName, string password,string email,out string error);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}