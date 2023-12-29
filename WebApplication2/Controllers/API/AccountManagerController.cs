using Elmah;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using WebApplication2.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace WebApplication2.Controllers.API
{
    public class AccountManagerController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private string baseUrlChurch = "https://stpaulatlanta.org/";

        public AccountManagerController()
        {
        }

        public AccountManagerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;

        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public async Task<string> Login(LoginViewModel model)
        {
            var modelJson = JsonConvert.SerializeObject(model);
            if (!ModelState.IsValid)
            {
                return "Failed" ;
            }


            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            ErrorSignal.FromCurrentContext().Raise(new Exception("failed to login  1-1"));
            switch (result)
            {
                case SignInStatus.Success:
                    return result.ToString();
                case SignInStatus.LockedOut:
                    ErrorSignal.FromCurrentContext().Raise(new Exception(modelJson + " " + " account is lockedout"));
                    return "Lockedout";
                case SignInStatus.RequiresVerification:
                    return "Verification";
                case SignInStatus.Failure:
                default:
                    ErrorSignal.FromCurrentContext().Raise(new Exception(modelJson + " " + " failed"));
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return "Failed";
            }
        }



        public async Task<string> LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return "Success";
        }
        public async Task<string> Register(RegisterViewModel model, string secret)
        {
            var modelJson = JsonConvert.SerializeObject(model);
            if (secret == "STMOBILE")
            {

                //string EncodedResponse = Request.Form["g-Recaptcha-Response"];
                //bool IsCaptchaValid = (ReCaptchaClass.Validate(EncodedResponse) == "true" ? true : false);

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.PhoneNumber };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await new Users().UserCreationUpdate(user.Id, model.FirstName, model.LastName, model.PhoneNumber);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);


                    ///INJECT HERE THE USER DETAILS AND INFORMATION 




                    //For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    //Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = baseUrlChurch + "Account/ConfirmEmail?userId=" + user.Id + "&code=" + code;
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return "Success";
                }
                ErrorSignal.FromCurrentContext().Raise(new Exception(modelJson + " " + "Failed to create account"));
                AddErrors(result);

            }
            ErrorSignal.FromCurrentContext().Raise(new Exception(modelJson + " " + "Can't create account"));

            // If we got this far, something failed, redisplay form
            return "Failed";
        }

        public async Task<string> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return "ForgotPasswordConfirmation";
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = baseUrlChurch + "Account/ResetPassword?userId=" + user.Id + "&code=" + code;
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return "Success";
            }

            // If we got this far, something failed, redisplay form
            return "Failed";
        }


        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }



        #endregion

    }
}
