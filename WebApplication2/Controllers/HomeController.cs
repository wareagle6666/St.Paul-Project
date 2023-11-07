using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication2.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System.Dynamic;


namespace WebApplication2.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private SqlDataProvider _datarepo = new SqlDataProvider();
        public ActionResult Index()
        {
            var imageList = new Image();

            var image = imageList.GetHomeImage();


            var private_key = "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCMHTulq5+mBj7L\ngVPUeSOQYNcerziDgjuBbHYOnp1boegnhqqcM0aMgsijd80xkfScYqDSbhMbU5H9\nAnYxJSRBlviu2e9TH/722ycBGZG7/NvydEUmZeizbLpuktz7/uyycRB88oTUxlIK\nWA4Xv1VffJ/qR1hoxKeY3Q5d6kO0ExTRwLIFLfh+gerChdBiBmTg/NCD3VoVCfOb\nqIeVitQnxwr7YHs0YsVcdR4udFTJEtapzz/sbMqCoZWetT5ka0o/KMl5euUmJtOS\nJdGee+D0qWXgDerT7zuAORiGJZXRxoCYu3bzLxkYTdpRo6sie9iIZw0QbwTY3ZLZ\n+GQZSGfZAgMBAAECggEAFmCKKNNYE/PmDipWxmryZcSRlrJKnEuC+rs6akClcP+n\n9r1ofLnTspfQjgrmwoaaQmpItP16H7XLwv9O+6+q3Vl3LgsFpd5I916Dbtwz4jS9\nYTjLMm65fgOyDVF3Pue1IGqI75ZBO0TRJXBwD8AxqlEFA9g7zmpZ5Jf+thEL7wjr\nQfAJQ3+CzV1BX5PNzjeNlB2DLo11V16k9l7Rcr/0vaIVGltghyUN+WmmfeQRB4ya\nVpU0vaymV/xAG9tqD6fVNCWHBaiVJSGi0npz7wTgnrXAw0rA0DUtBI3peukTiAu/\nLSeWZzEd0YqvLgBUc1Ql448gSZ1WiKink/yPplZz4QKBgQDDKDYEa1qP/HrKwOGO\nS75mQTQKtxeppFNXf7xCKd0BxG8kZNXkPYpyKzifaLzq3eIfaaAa/KCTEDsagKLB\nQbwGdOK1zttzwoeccXVhCRRKaH07o0kIIgk7mfd1uwTlXXLeOhX6syw+Yp/v2DSv\n2gJZoS/qBeIwPi/DP7xoEA2bKwKBgQC3y/bN/tZR0lDbmYy2Iml92X8F0IocVNSz\nukHIakZkBkyM3Phrds5qlGJkXkaKDOc6iCLdxfSv3ZA4pxWroSu4xJkHMjsgcCyv\nXQWYNAHIrnRQf2f5Dya0EW1y1aGbnu9XJUfNrZjwsKUX2k0zi9XSVJodziWDhBEC\nXOOEvNW3CwKBgFLoiFpESUsWVgxA+RH8t7y1QqiytAjL2OQ5Tf9FzbBshVMOy5eV\nVXW/SuTeGDQnY2M4+l28qXMQ2CsgLwSvKnQLwCzA3pZFNFuQD+/TbZ1W2q01Z1df\nLlD1zh1kAGZ41s32G1RQPiGawJuiXG/AzHLLeZGQQlGAecYXE7GZ8E3VAoGBAK0t\nGzDhKqMRnWosda6vIeKYadzarycerwNhPdZOfGCGpt4a3l6zJK67fTHAuoocukom\nN2PqvzBtfRREKmD6jS2c3+st4xQfXZsl9L2CUyP5bFLkmNzM79S+jveiXpE44Z6k\n29Dp/q7aYdPlttQYr7FsUYI/2IzghP5K830ASAYdAoGAXQdz/Tm85ddmJkr3rBrR\nBeIIUc3Du2ViOw7+HoqTWdoF7vrqI8oeJq08ljYz4Egjj2+6lAad6YA87AB4ZEip\nA2WJ2HsN2RLhA6uzS+hRZxDkzv4uXzIKwlpZliF5+kH9ET0WP9Hgys/T+qYxUiXA\nTDV0EbaUnAgEkP2PV7vHY/k=\n-----END PRIVATE KEY-----\n";
            string calendarId = @"8k24l0tb9ghfsgmf8a2tuo3bto@group.calendar.google.com";
            var client_email = @"stpaulservice@stmary-314020.iam.gserviceaccount.com";
            dynamic mymodel = new ExpandoObject();
            News News = new News();
            var NewsList = News.GetSpotNews();


            var credential =
                new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(client_email)
                {
                    Scopes = new string[] { CalendarService.Scope.Calendar }
                }.FromPrivateKey(private_key));
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
            });

            var calendar = service.Calendars.Get(calendarId).Execute();
            // Define parameters of request.
            EventsResource.ListRequest listRequest = service.Events.List(calendarId);
            listRequest.TimeMin = DateTime.Today.AddDays(-1);
            listRequest.ShowDeleted = false;
            listRequest.SingleEvents = true;
            listRequest.MaxResults = 10;
            listRequest.TimeZone = "Eastern Standard Time";
            listRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            var events = listRequest.Execute();
            var list = events.Items;

            List<Models.Calendar> eventlist = new List<Models.Calendar>();
            var today = DateTime.Today;
            var tz = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            if (list != null || list.Count >= 0)
            {
                foreach (var item in list)
                {
                    if (item.Start.DateTime.Value.Date == today.Date || item.Start.DateTime.Value.Date == today.AddDays(1).Date)
                    {
                        DateTime StartTime = item.Start.DateTime.Value;
                        DateTime EndTime = item.End.DateTime.Value;

                        var Start = TimeZoneInfo.ConvertTime(StartTime, tz);

                        var End = TimeZoneInfo.ConvertTime(EndTime, tz);
                        eventlist.Add(new Models.Calendar(item.Summary, Start, End));
                    }
                }
            }


            mymodel.Image = image;
            mymodel.eventlist = eventlist;
            mymodel.NewsList = NewsList;

            return View(mymodel);
        }
    }
}
