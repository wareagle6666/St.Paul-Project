using Elmah;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class MobileNotificationController : Controller
    {

      
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(FormCollection form)
        {

            try
            {
                var Title = form["Title"];
                var Message = form["Message"];

                var _dataprovider = new SqlDataProvider();

                var result = _dataprovider.SaveNotification(Message, Title, "", "", "");
                var NoticiationCount = _dataprovider.GetAllNotifications().Count + 1;
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
                                    click_action = "HANDLE_BREAKING_NEWS",
                                    sound = "default",
                                    badge = 1
                                },
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

                            var httpresult =  _http.SendAsync(httprequest);
                            if (httpresult.Result.StatusCode != HttpStatusCode.OK)
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

                    ViewBag.Message = "Message Sent!";

                    return View("Index");

                }


            }
            catch (Exception e)
            {
                ViewBag.Message = "Failed to send Message!";
                ErrorSignal.FromCurrentContext().Raise(e);

                return View("Index");
            }
            return View();
        }
    }
}