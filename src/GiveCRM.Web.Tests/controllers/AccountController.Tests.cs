using GiveCRM.Web.Controllers;
using GiveCRM.Web.Models;
using GiveCRM.Web.Services;
using MvcContrib.TestHelper;
using NSubstitute;
using NUnit.Framework;

namespace GiveCRM.Web.Tests.controllers
{
    [TestFixture]
    public class AccountControllerTests:AssertionHelper
    {
        private IMembershipService membershipService;
        private IAuthenticationService authenticationService;
        private IUrlValidationService urlValidationService;

        [SetUp]
        public void SetUp()
        {
            membershipService = Substitute.For<IMembershipService>();
            authenticationService = Substitute.For<IAuthenticationService>();
            urlValidationService = Substitute.For<IUrlValidationService>();
        }

        private AccountController CreateController()
        {
            return new AccountController(membershipService,
                authenticationService,
                urlValidationService);
        }

        [Test]
        public void ShouldLogOnUserAndRedirectToHome()
        {
            var controller = CreateController();
membershipService.ValidateUser("test","password").Returns(true);
            urlValidationService.IsRedirectable(controller,string.Empty).Returns(false);

            var model = new LogOnModel();
            model.UserName = "test";
            model.Password = "password";
            var url = string.Empty;

            var actionResult = controller.LogOn(model, url);
            Expect(controller.ModelState.IsValid, Is.True);
            actionResult.AssertActionRedirect();
        }

        [Test]
        public void ShouldLogOnUserAndRedirectToUrl()
        {
            var controller = CreateController();

           membershipService.ValidateUser("test", "password").Returns(true);
            urlValidationService.IsRedirectable(controller, "testurl").Returns(true);
           
            var model = new LogOnModel();
            model.UserName = "test";
            model.Password = "password";
            var url = "testurl";

            var actionResult = controller.LogOn(model, url);
            Expect(controller.ModelState.IsValid, Is.True);
            Expect(actionResult.AssertHttpRedirect().Url, Is.EqualTo(url));
            actionResult.AssertHttpRedirect();
        }

        [Test]
        public void ShouldNotLogOnForIncorrectCredentials()
        {
            var controller = CreateController();

            membershipService.ValidateUser("test", "password").Returns(false);
            
            var model = new LogOnModel();
            model.UserName = "test";
            model.Password = "password";
            var url = string.Empty;

            var actionResult = controller.LogOn(model, url);
            Expect(controller.ModelState.IsValid, Is.False);
            Expect(controller.ModelState[string.Empty].Errors.Count, Is.EqualTo(1));
            Expect(controller.ModelState[string.Empty].Errors[0].ErrorMessage,Is.EqualTo("The user name or password provided is incorrect."));
            actionResult.AssertViewRendered().WithViewData<LogOnModel>();
        }

        [Test]
        public void ShouldLogOff()
        {
            authenticationService.SignOut();
            var controller = CreateController();

			var actionResult = controller.LogOff();
            authenticationService.Received();
            actionResult.AssertActionRedirect();
        }

        [Test]
        public void ShouldRegister()
        {
            string error;
            membershipService.CreateUser("test", "password", "a@a.a", out error).Returns(true);
            var controller = CreateController();
            var model = new RegisterModel
                            {
                                UserName = "test",
                                Password ="password",
                                Email = "a@a.a"
                            };
            var actionResult = controller.Register(model);
            actionResult.AssertActionRedirect();
        }

        [Test]
        public void ShouldFailToRegister()
        {
            var error = string.Empty;
            membershipService.CreateUser("test", "password", "a@a.a", out error).Returns(false);
            var controller = CreateController();
            var model = new RegisterModel
            {
                UserName = "test",
                Password = "password",
                Email = "a@a.a"
            };
            var actionResult = controller.Register(model);
            actionResult.AssertViewRendered().WithViewData<RegisterModel>();
        }

        [Test]
        [Ignore]
        public void ShouldChangePassword()
        {
            var model = new ChangePasswordModel
                            {
                                NewPassword = "Slartibartfast",
                                OldPassword = "password",
                                ConfirmPassword = "Slartibartfast"
                            };
            
membershipService.ChangePassword("userName", "password","Slartibartfast").Returns(true);
            
            var controller = CreateController();
            
            var actionResult = controller.ChangePassword(model);
            Expect(controller.ModelState.IsValid,Is.True);
            actionResult.AssertActionRedirect();
        }

        [Test]
        public void ShouldNotChangePasswordInvalidModel()
        {
            var model = new ChangePasswordModel
            {
                NewPassword = "Slartibartfast",
                OldPassword = "password",
                ConfirmPassword = "ZaphodBeeblebrox"
            };

            var controller = CreateController();
            controller.ModelState.AddModelError("NewPassword","The new password and confirmation password do not match.");
            
            var actionResult = controller.ChangePassword(model);
            Expect(controller.ViewData.ModelState.IsValid, Is.False);
            actionResult.AssertViewRendered().WithViewData<ChangePasswordModel>();
        }

        [Test]
        public void ShouldNotChangePasswordIFailForChangePassword()
        {
            var model = new ChangePasswordModel
            {
                NewPassword = "Slartibartfast",
                OldPassword = "password",
                ConfirmPassword = "Slartibartfast"
            };

membershipService.ChangePassword("userName", "password", "Slartibartfast").Returns(false);
            var controller = CreateController();
            
            var actionResult = controller.ChangePassword(model);
            Expect(controller.ViewData.ModelState.IsValid, Is.False);
            Expect(controller.ModelState[string.Empty].Errors.Count,Is.EqualTo(1));
            Expect(controller.ModelState[string.Empty].Errors[0].ErrorMessage, Is.EqualTo("The current password is incorrect or the new password is invalid."));
            actionResult.AssertViewRendered().WithViewData<ChangePasswordModel>();
        }
    }
}
