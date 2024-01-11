using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using WebApplication2.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Elmah;

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


        public async Task<string> SendNotification(string Title, string Message)
        {
            var _dataprovider = new SqlDataProvider();

            var result = _dataprovider.SaveNotification(Message, Title, "", "", "");

            if (result == 1)
            {
                try
                {
                    var listofTokens = _dataprovider.GetAllMobileTokens();

                    foreach (var mobileToken in listofTokens)
                    {
                        var request = new NotificationRequest()
                        {
                            to = mobileToken.Token,
                            notification = new Notification()
                            {
                                body = Message,
                                title = Title,
                                click_action = "NEW_MESSAGE_CATEGORY",
                                sound = "default"
                            } ,
                            priority = "High",
                            data = new NotificationData()
                            {
                                story_id = new Guid().ToString()
                            }
                        };

                        string jsonString = JsonConvert.SerializeObject(request);

                        var _http = new HttpClient
                        {
                            BaseAddress = new Uri("https://fcm.googleapis.com/fcm/send")
                        };

                        var httprequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send")
                        {
                            Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
                        };

                        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "AAAAmt0eW0c:APA91bFPuS_8a80Wks7E7jqHpmtcTd1e9ZppKfK0h6f6o-d_o3gi8uMs8QXLmObJSEFD8Q5lsLmmVr91TR_6oNMD9w7cXq19YZ_4_Ow6SZhFl2sQc1lXNdQ8Q9KIyJr5_vQ0HOEvCGYq");

                        var httpresult = await _http.SendAsync(httprequest);
                        if (httpresult.StatusCode != HttpStatusCode.OK)
                        {
                            throw new Exception("Mobile Failed to get notification: " + mobileToken.Token);
                        }
                        
                    }



                }
                catch (Exception e)
                {
                    var issue = e;
                    ErrorSignal.FromCurrentContext().Raise(e);
                }


                return "Success";
            }
            else
            {
                return "Failed";
            }
        }

        public async Task<List<NotificationsModel>> GetAllNotifications()
        {
            var _dataprovider = new SqlDataProvider();

            var result = _dataprovider.GetAllNotifications();
            return result;
        }



    }
}
