namespace GiveCRM.Web.Services
{
    using System.Web.Security;
    using GiveCRM.BusinessLogic;

    public class MembershipService : IMembershipService
    {
        public bool ValidateUser(string userName, string password)
        {
            return Membership.ValidateUser(userName, password);
        }
        
        public bool CreateUser(string userName, string password, string email,out string error)
        {
            error = string.Empty;
            MembershipCreateStatus createStatus;
            Membership.CreateUser(userName, password, email, null, null, true, null, out createStatus);
            if (createStatus == MembershipCreateStatus.Success)
            {
                return true;
            }

            error = ErrorCodeToString(createStatus);
            return false;
        }
        
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            MembershipUser currentUser = Membership.GetUser(userName, true);
            return currentUser.ChangePassword(oldPassword, newPassword);
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }



    }
}