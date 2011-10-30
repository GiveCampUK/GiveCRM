using System;
using System.Web.Mvc;
using System.Web.Security;
using GiveCRM.Web.Models;
using GiveCRM.Web.Services;

namespace GiveCRM.Web.Controllers
{
    public class AccountController : Controller
    {
        private IMembershipService membershipService;
        private IAuthenticationService authenticationService;
        private IUrlValidationService urlValidationService;

        public AccountController(IMembershipService membershipService, IAuthenticationService authenticationService, IUrlValidationService urlValidationService)
        {
            this.membershipService = membershipService;
            this.authenticationService = authenticationService;
            this.urlValidationService = urlValidationService;
        }


        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                
                if(membershipService.ValidateUser(model.UserName,model.Password))
                {
                    authenticationService.SetAuthorizationCredentials(model.UserName,model.RememberMe);
                    
                    if(urlValidationService.IsRedirectable(this,returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            authenticationService.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var error = string.Empty;
                if (membershipService.CreateUser(model.UserName,model.Password,model.Email,out error))
                {
                    authenticationService.SetAuthorizationCredentials(model.UserName,false);
                    return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError("", error);
                
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    string username = User == null ? string.Empty : User.Identity.Name;
                    changePasswordSucceeded = membershipService.ChangePassword(username, model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        
    }
}
