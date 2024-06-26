﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Elmah;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using WebApplication2.Models;

namespace WebApplication2
{
    public class EmailService : IIdentityMessageService
    {

        public Task SendAsync(IdentityMessage message)
        {
            try {
                // Plug in your email service here to send an email.
                #region formatter
                string text = string.Format("Please click on this link to {0}: {1}", message.Subject, message.Body);
                string html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";

                html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message.Body);
                #endregion

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("st.paulchurch@stpaulatlanta.org", "St.Paul Coptic Orthodox Church");
                    mail.To.Add(message.Destination);
                    mail.Subject = message.Subject;
                    mail.Body = message.Body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("st.paulchurch@stpaulatlanta.org", "qdlc kzjb nopj lywy");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.Timeout = 400000;
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(mail);
                    }
                }
                return Task.FromResult(0);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return Task.FromResult(1);
            }

        }
        public Task EventEmail(IdentityMessage message)
        {
            try {

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("st.paulchurch@stpaulatlanta.org", "St.Paul Coptic Orthodox Church");
                    mail.To.Add(message.Destination);
                    mail.Subject = message.Subject;
                    mail.Body = message.Body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("st.paulchurch@stpaulatlanta.org", "qdlc kzjb nopj lywy");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.Timeout = 400000;
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                return Task.FromResult(0);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return Task.FromResult(1);
            }
        }
        public Task ConfEmails(EmailMessageModel message)
        {
            try
            {
                List<string> optionList = new List<string>
            { "AdditionalCardPersonAdressType", /* rest of elements */ };

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("st.paulchurch@stpaulatlanta.org", "St.Paul Coptic Orthodox Church");
                   
                    if(message.sentToo.Count != 0)
                    {
                        foreach(var email in message.sentToo)
                        {
                            mail.To.Add(email);
                        }
                    }
                    mail.Subject = message.Subject;
                    mail.Body = message.Body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("st.paulchurch@stpaulatlanta.org", "qdlc kzjb nopj lywy");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.Timeout = 400000;
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                return Task.FromResult(0);
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return Task.FromResult(1);
            }
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager which is used in this application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.  
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
