﻿using GiveCRM.Web.Attributes;

namespace GiveCRM.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using GiveCRM.BusinessLogic;
    using GiveCRM.Web.Models;
    using GiveCRM.Web.Services;

    [HandleErrorWithElmah]
    public class AccountController : Controller
    {
        private readonly IMembershipService membershipService;
        private readonly IAuthenticationService authenticationService;
        private readonly IUrlValidationService urlValidationService;

        public AccountController(IMembershipService membershipService, IAuthenticationService authenticationService, IUrlValidationService urlValidationService)
        {
            this.membershipService = membershipService;
            this.authenticationService = authenticationService;
            this.urlValidationService = urlValidationService;
        }

        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (this.membershipService.ValidateUser(model.UserName, model.Password))
                {
                    this.authenticationService.SetAuthorizationCredentials(model.UserName, model.RememberMe);
                    
                    if (this.urlValidationService.IsRedirectable(this, returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    
                    return this.RedirectToAction("Index", "Home");
                }

                this.ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            this.authenticationService.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                string error;
                if (membershipService.CreateUser(model.UserName, model.Password,model.Email, out error))
                {
                    authenticationService.SetAuthorizationCredentials(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError(string.Empty, error);
                
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    changePasswordSucceeded = this.membershipService.ChangePassword(username, model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                this.ModelState.AddModelError(string.Empty, "The current password is incorrect or the new password is invalid.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
    }
}
