using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.Interface;
using WebApplication2.Models;
using System.Dynamic;
using Google.Apis.Calendar.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace WebApplication2.Controllers
{
    public class DEACONSController : Controller
    {

        // GET: DEACONS
        public ActionResult Index()
        {
            var private_key = "-----BEGIN PRIVATE KEY-----\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDbOtiAstMPAe1R\nO35oxSDCLQIPzM1rxFM8FjtLRrlkk492CoMT6rnJILnyOgbM9+01hnZNT6evfHr1\n6KL2ucBVMN5ZjwCVizUW3hyCIdkJVVXOVlmUEtPitAk5uXOr9UF0vAm4I7+l70LN\nkUKaHjI8VSXQWf73fUdIZ514w35u+F646QTRXZCKAQY+APvXhaAx/FiKzwmiK37V\nkq2miVkLbsAjbinhB0a4eo8au8eyWqISredeo3bpIG3zFxlY0s8OU05F4OIlSLY0\nSEN16a3wc8NTuqM8SSDYSk92hrqOTYPSGA+z13MM9YY74QKTWRNr1pqPDs464HY1\nWkNjcXu5AgMBAAECggEADVhsNI1uPrQh07eY7tzNLTTbMsYHXT+Sx44c5uyzvXUk\nHw3tw8kerSkyWJYCsviGtqLsUbF/JjYgMBcM0JMnK6hxaKdFZFUYkr9LofpYgXJx\n6kEka9inkF3gT+TDr1ybMvV9a/m1IW9KtEPa95HOAhE235OS61Lzg2tXUTIpYKze\nEIRwn97K14vQWZMrkxDv/Veh1DkSzU+gBDK3kEFWdWnxZUyTQrEtIQqkXjV5/xeY\nnqn5ih6Mz4Q3yrzKUYUr4kmv96KBPNh0lkegxZtP0taNNtOxnq0sPdjBdFGSZ1mp\nxxXs4g26VpCTg3/clj7xhSeZEtLZcZWKrXGsGLey/wKBgQDzvqHOrwHuG+F5FKwh\nlk/dW9eGyXcfMgLpaDCBh9AVOp5Li99s1ttovioVOfYQ6jE3sauTspPhrhRUz4Tm\nxjEu2RwH+INphtq+3VW5iqe4/VLadW497/Aqn9gYLcF/nlV1XC2QogfAkEtj0IYm\n5Wv/pMUQ9I/8q/jUFZA09etJDwKBgQDmQKuwrRA5E3F4FtmC380x7Ru6gOR5oy1X\n3ncjfbs9iwmT2811ki4wSrtrKTJGGQIREmWM30sq9qRRauOXdS2+Zth7QKlbyOWM\nY0k6HEbm+8RoUI5ld4vSD3FUkofGUPSNFMCACJBeskAv9Dv0mhiZNae9vtHV3J0d\n8CdjAkeetwKBgQC1tG87fQubTf2QHfEUONHg2w6Y1KeRAd9Z0OoYJf7hVImdF/C/\nWozGUL1ehY8CnRODyVd3C7FilDtj3i/dOfKAUimrZ3/Ps1Bu6OF3J/5Q0chqQxCj\n67LGk9Ksc6/ES2y5yLXPFHti+i65gkH/zRftxA+EzExquGkO8eRHFu0H0QKBgQCv\n/F/94tkSdtz0Z6qKUFp6vXrDGv8GRYVDsuKmutPhHyML4yVvpnItMQF8z3kAgN7G\ng6Qgfw7p4KgJHsSjn8l9zQfLdMm9QJjKq5xtkO1UYLxm6qNbxHqW7Hy3omOqXXf9\nWQ4lDeaiqmVLAcbNlvYULCFwcqfZaQyhtEu2biilkwKBgC+bHO7db+CLNopSXgq5\nqq55Huqg5DHffkO1TQhXWBH28J/mkNwpcz036qPU2TrfngVLyCp5OSJTtl8zgY3v\nm7dWjN2v675AD58F1vuHzKNHuxOMQatBKTACRfjzvyVid5PGhxJ8z5AYSrkQJTFB\nw19jssJ+rHcUlo0EmoXNisNZ\n-----END PRIVATE KEY-----\n";
            string calendarId = @"8qm6fin0q68qk381m6l4f9q9q0@group.calendar.google.com";
            var client_email = @"stpauldeaconapi@stmary-314020.iam.gserviceaccount.com";
            dynamic mymodel = new ExpandoObject();


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

            List<Calendar> eventlist = new List<Calendar>();
            var today = DateTime.Today;
            var tz = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            if (list != null || list.Count >= 0)
            {
                foreach (var item in list)
                {
                    var currentDate = DateTime.Parse(item.Start.Date);
                    string Date = currentDate.ToString("ddd, dd MMM yy");
                    string[] names = item.Summary.Split(',');

                    eventlist.Add(new Calendar(names, Date));
                }
            }

            return View(eventlist);

        }
    }
}