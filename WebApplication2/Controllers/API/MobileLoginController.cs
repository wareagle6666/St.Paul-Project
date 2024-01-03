using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers.API
{
    public class MobileLoginController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private string baseUrlChurch = "https://stpaulatlanta.org/";

        public MobileLoginController()
        {
        }

        public async Task<string> SaveMobileData(string UserId, string Token, string DeviceType)
        {
            var _dataprovider = new SqlDataProvider();

            var result = _dataprovider.CreateMobileRecord(UserId, Token, DeviceType);

            if (result == 1)
            {
                return "Success";
            }
            else
            {
                return "Failed";
            }
        }
        public async Task<List<Mobile>> GetAllMobileTokens()
        {
            var _dataprovider = new SqlDataProvider();

            var result = _dataprovider.GetAllMobileTokens();
            return result;
        }
        public async Task<string> DeviceLoggedIn(string Token)
        {
            var _dataprovider = new SqlDataProvider();

            var result = _dataprovider.UpdateLastLogin(Token);
            if (result == 1)
            {
                return "Success";
            }
            else
            {
                return "Failed";
            }
        }






    }
}
