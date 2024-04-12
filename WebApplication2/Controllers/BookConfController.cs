using Elmah;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class BookConfController : Controller
    {

        // GET: BookConf
        public ActionResult Index()
        {
            return View();
        }

        // GET: BookConf/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookConf/Create
        public ActionResult Create()
        {

            return View();
        }

        public ActionResult FrEleia()
        {
            var confList = new ConfSlot();
            var ConfList = confList.GetConfSlotbyPriest("freleia@gmail.com");
            return View(ConfList);
        }
        public ActionResult FrEleia2()
        {
            var confList = new ConfSlot();
            var ConfList = confList.GetConfSlotbyPriest("freleia@gmail.com");
            return View(ConfList);
        }
        public ActionResult Book(int ID)
        {
            var confList = new ConfSlot();
            var SlotDetail = confList.GetConfSlotbyID(ID);

            if (SlotDetail.Priest.Trim() == "FrElishaSoliman@gmail.com")
            {
                ViewBag.Priest = "Fr.Elisha";
            }
            if (SlotDetail.Priest == "freleia@gmail.com")
            {
                ViewBag.Priest = "Fr.Eleia";
            }

            ViewBag.Date = SlotDetail.Date;
            ViewBag.From = SlotDetail.fromDate;
            ViewBag.To = SlotDetail.toDate;
            ViewBag.ID = ID;

            return View();

        }
        public ActionResult FrElisha()
        {
            var confList = new ConfSlot();
            var ConfList = confList.GetConfSlotbyPriest("FrElishaSoliman@gmail.com");
            return View(ConfList);
        }
        public ActionResult FrElisha2()
        {
            var confList = new ConfSlot();
            var ConfList = confList.GetConfSlotbyPriest("FrElishaSoliman@gmail.com");
            return View(ConfList);
        }
        // POST: BookConf/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            //var ConfID = Convert.ToInt32(Request["ID"]);
            //var confList = new ConfSlot();
            //var SlotDetail = confList.GetConfSlotbyID(ConfID);
            //var booking = new ConfBooking();
            //var user = new Users();

            //var fromDate = SlotDetail.Date + " " + SlotDetail.fromDate;
            //var toDate = SlotDetail.Date + " " + SlotDetail.toDate;

            //booking.Title = "Confession for " + Request["fname"] + " " + Request["lname"] + " : " + DateTime.Parse(SlotDetail.Date).DayOfWeek.ToString() + " " + SlotDetail.Date + " between " + SlotDetail.fromDate + " - " + SlotDetail.toDate;
            //booking.FromDate = Convert.ToDateTime(fromDate);
            //booking.ToDate = Convert.ToDateTime(toDate);

            //booking.Fname = Request["fname"];
            //booking.LFname = Request["lname"];
            //booking.Email = Request["email"];
            //booking.Phone = Request["number"];
            //booking.Message = Request["message"];
            //booking.SlotID = ConfID;
            //booking.Priest = SlotDetail.Priest;

            //string private_key = "";
            //string calendarId = @"";
            //string client_email = @"";

            //if (SlotDetail.Priest.Trim() == "FrElishaSoliman@gmail.com")
            //{
            //    private_key = "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDPs8jyS52fwDsf\n1fka30AM/P2AMyBWgORnnBI1QvfMYYgIEg3pZtmx81UfqwmvnvH2cg7h1i5ZbqjO\nxYSM1gAyqpB0WKewg96+ZSEZbRQ3gFIP8AFPfiyLnxPxolb1Iz9MaNZBLbnQRMzJ\njHAroFZHAJNkGgnIXyrT7uOcytyr6iSTjyrlzsQZz5Pdf+g4WPERZ4bzq48uBz74\nj7+6llVut0mP+M7eTmQqWdm+bf9sYvT8tRzoVeApUz2xpT/cR0qnSrgA2+4K14fO\nUrOxvJP+uz52PYxL5cIgUCTVKtCd/ZxAA/AUdT4J4ZZOqwqUblvzUkrH7KaTzHLj\nWoDSWwrpAgMBAAECggEABmBinu2rXSgtqICNMUIBWWS686CeMYmreXLVLEVuDhxA\nbN8BBlVeAZSS3HIHzhh3HTXLNyu4KI65J//wXdKEFwodfTTrkQO7ZuT76yfqIvoR\niuO3ZpVdELxFhHmTads3KkVdroqU1cwG7XkeSHiXrX5bCAu0mS4yxmbjHgEsXSq4\n49OqUq3dXpkI4H95mxxa33CY5OsKEeXDDfcGra0629tlWG25Sn0zWHi7m1vZfiwS\nLfEX4s83mbOW5SeEFhYixQ833f2wbxRG75o8IUF14P9iF4K8OmPBCUdL6y8W7WDu\nK4Yfu+91UnoWQeRf3Sv+z27ig106xomvUqF4iIRYAQKBgQDpugJBzyBJ638r0eO9\nU1yC7FvsVqk8B5FvSB7fUuddA37Mfr6Ai8AY576MLPBdIkD2hD884clH+L9yHxgG\niyS6aQ+sK7j0dxojiHoPftEup5tcMot5iXOO6OHaz/1GlJJdTNUDrz69hIuyEXjp\nmtXHS2qECb1NKbgR5t3T95vOQQKBgQDjfuMoaJkYZ4TlDReS5kNuK7qjlN/YX/4W\nd67i3D5s1EkherK1GkE1q/LU77cJWTPDLTqa0j3o/s29OjJ5PYMngKZQ5TmRp3+N\ng0egMTC5APO4q+yipfKHD7DPDG3oId3Q9bDMlCOC0cCtRofq1HX5f/3sHmiWhHx5\n4m4FqstiqQKBgQCvasa046KlOBVbKw1VFBKzxHd9WtFrV3bE4YkiLZOt0KjDDEEo\nMzr6tjvh8vx3ufSt0DWyGPe7h6sQGNwsVRQ8wAteOfZ3rEg28QBDTvfqnyrQOLNL\nhWjwkkKouj+qUGnFkpuxRz68eJVsEQcBtQ7LpAOOk8y69BvNe8tzgjwywQKBgAMd\n//rTxBVDhdylm0cWBKKD0uDe71pFpnOkXC8bmXdgSUg+KCi3HWGg44jrZ/Sm1kC8\nY+svKk4A/8yx+XMT1rI53w2Itos6YlMME3Usd2BxlOVY5bsszu/u1RthLjhaDnII\ni82h0gK+4Qb+ymn7U0qB6HbNy90UH6iD48IMHxFRAoGAaE+dRGGSI2GU9lntWJII\ntDDIBXrXdegpXXeUV35/xW+anmmilX2v4l5dr6TJEDdvQ5lHry3NGYN543BV72vC\nwaoOJ5s+HR8R+MWoHLFK3E8Y8NAmF4Vng36WOYCWTJ9HPFhLUWm9E1wdsRPSQJOJ\nkI4g4Gvy+FKS4ydxvdjVFJU=\n-----END PRIVATE KEY-----\n";
            //    calendarId = @"frelishasoliman@gmail.com";
            //    client_email = @"frelishaapi@stmary-314020.iam.gserviceaccount.com";
            //}
            //else if (SlotDetail.Priest.Trim() == "freleia@gmail.com")
            //{
            //    private_key = "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDRzg2w4R78Ar3u\nO96o9l6ltkJyKrh2SuxKuXBhdzSbnbKH9vvvbUjjZUOxLWF1uMibYd + cr3rjUgg0\ncsw87iTZwUYnjuQTjyDSUhPetOdvb4exvAP0ULXJfPPP1vZmW8v5I2vw8HAQn2DO\nRhPU1Rv / SecySt5w6i6sw5 + G4qZz6Oj9k / C1TFZA50zUSmAGQ / y8Tb8EQrxi6YCu\nd7To / wAyiO5hLgoIe / l3BxF96N3 + MOQDHSkj / RKqHJjJnSBaVUavVEsCImV3YZVq\ngvdoBe + LmLgOWm2R50yxCJ + jo / jQhoBFpX + Pz04qoyLvmlqGY6ZZovsBdvbB1LhT\nIZGtvd7VAgMBAAECggEABgWM / QupEQCfJOxQ + rZ / hvARPiedvb9uwsJJyHJcby76\nhc5 / 8qJHWkd4xugPiQpxr4wCmAOtWkLre13kIMeRBTL36YC / aRRNk9vJODYnAlrI\nIUDNOEDsxsiwUsHPtqZh11e97F9qR//bDnCSdyRatlwPp7Dga4s9JbZDqOJudpsm\nbszCIw5PkXRFzYpF/W2KIrLebvv8cImQT2HQpmF2317Hf/s5ZqxN6n16zzzILf12\n2OpI36SeAHPli0CKwTOoQrQQTySUksdrlwl8gvKvoAH9/OfDaSUY9B+9vEcxuMz3\nN89geH4oouQB+cCUrNFi4YDSLaA0jHGxnhsuqrtTbQKBgQDqwmCwzalLhSdYBCD/\njpEAwiAT8EbKU7ewmTQWMxKQAPRQcEssgqZbEURzonUdmf+iqOoj/Y+leo2M94/Z\n3YbBkk3zx22uRdGdQsMBE2GCnz/+LrezlHq9FZVS/PD+oBJndTzF4h7da/IcIwfc\nPd11h+yH0FDQ52W52RzL6qFStwKBgQDkyaspENBHDoTa2At/z4vY97pgG69DyGxb\nC9s6hBiitV7gQNAq+fdKc/8jrg0Tttosv/uFXTVENfgdOIWVuzg0fpAR7WraZN9D\noJglojDvwUF0NJg0N+CAvg+PEHTqrMDagwZJ0uT0459aH6b8ZEegbzfsH0ScK4PP\nAXaUx1Te0wKBgAluf8X6rUeMdPUzTUQXLTozkmhaqe1tCZ89uHr/2EoSz73/lPWX\n1ikoe3CN4VisHDojwSxq/n9uegtk0tG09boHL7yQkZaD1ZUU3pLHfY5q6X5D0DuV\nxoycNmCcKu5/7d1cN3HX0Neig5qfyawOjDwxls4qD2UlNtANqfuDVLHnAoGAP8bL\n9MWgGb69YDgLdJSJmnDsPnBZOh77vaGfej9qkqjWitLmdKR7wAB35h1VveIiDYly\n9ulVuO8GfDbOXj/zWgbR6H1zZO/roPF6mDsPQzZvJZKnvx64cYsJ0Uq4HNcNZ5xQ\nu3GTq4RsXnYmWYMmyF1YjHTm7lXfB2yT/sNWMEsCgYEA6daO0aZcj32ePPOR5byg\n1kEji7lyFKjd9UXMEptKH0/4+iijax5y8JOXNkCizefszUGraGJfR9Hd0IWh8CWX\njktnN9k4yWi2vAfW8qK8yCVydpIcfyAamCd47uFtuP1t7FJGo45vI1r3F8NkgqCm\n6pUwk5jbb6cM4qYkkD56Tvw=\n-----END PRIVATE KEY-----\n";
            //    calendarId = @"freleia@gmail.com";
            //    client_email = @"frelieaapi@stmary-314020.iam.gserviceaccount.com";

            //}



            //try
            //{
            //    var result = GoogleEventCreate(booking, client_email, private_key, calendarId);
            //    //var result = true;
            //    var currentPriest = user.GetUserByUsername(SlotDetail.Priest);

            //    if (result)
            //    {

            //        var results = SlotDetail.UpdateConfSlot(booking.SlotID);
            //        var bookingResult = booking.insertConfBooking(booking);
            //        //var results = true;
            //        //var bookingResult = true;
            //        if (results && bookingResult)
            //        {
            //            var Emailbdy = $@"Hi " + booking.Fname + " " + booking.LFname + $@"<br><br><div>Your request for " + booking.Title + " booked successfully.";
            //            string subject = "Booked For: " + booking.Title;

            //            subject.SendEmail("StMaryAtlanta@coc-services.com", new List<string> { booking.Email }, null, null, Emailbdy, true, null);
            //            var text = "Your request for " + booking.Title + " booked successfully";
            //            text.SendText(booking.Phone);

            //            var Priest = "";
            //            if (SlotDetail.Priest.Trim() == "FrElishaSoliman@gmail.com")
            //            {
            //                Priest = "Fr.Elisha";
            //            }
            //            if (SlotDetail.Priest.Trim() == "freleia@gmail.com")
            //            {
            //                Priest = "Fr.Eleia";
            //            }

            //            var AlertTitle = DateTime.Parse(SlotDetail.Date).DayOfWeek.ToString() + " " + SlotDetail.Date + " between " + SlotDetail.fromDate + " - " + SlotDetail.toDate;
            //            StringBuilder BookingDetail = new StringBuilder();
            //            BookingDetail.AppendLine("Name: " + booking.Fname + " " + booking.Fname);
            //            BookingDetail.AppendLine("Email: " + booking.Email);
            //            BookingDetail.AppendLine("Phone: " + booking.Phone);
            //            BookingDetail.AppendLine("Message: " + booking.Message);

            //            var Emailbdy2 = $@"Hi " + Priest + $@"<br><br><div>Confession was booked for " + AlertTitle + " . By: <br><br>" + BookingDetail.ToString();
            //            string subject2 = "Booked For: " + AlertTitle;

            //            subject2.SendEmail("StMaryAtlanta@coc-services.com", new List<string> { currentPriest.Email }, null, null, Emailbdy2, true, null);
            //            var text2 = "You have a new " + booking.Title;
            //            text2.SendText(currentPriest.PhoneNumber);



            //        }
            //        else
            //        {
            //            throw new Exception("booking failed");
            //        }
            //        //send email
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        ViewBag.ErrorMessage = "We couldn't finish the approval. Please conact admin";
            //        return RedirectToAction("Index");
            //    }
            //}
            //catch (Exception e)
            //{
            //    ViewBag.ErrorMessage = e.Message;
            //    ErrorSignal.FromCurrentContext().Raise(e);
            //    return View();
            //}
            return View();
        }
        public bool GoogleEventCreate(ConfBooking Details, string client_email, string private_key, string calendarId)
        {

            dynamic mymodel = new ExpandoObject();





            try
            {
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

                var ev = new Event();

                String fullMessage = "Confession for " + Details.Fname + " " + Details.Fname + " " + Details.Phone;

                StringBuilder Description = new StringBuilder();
                Description.AppendLine("Name: " + Details.Fname + " " + Details.Fname);
                Description.AppendLine("Email: " + Details.Email);
                Description.AppendLine("Phone: " + Details.Phone);
                Description.AppendLine("Message: " + Details.Message);

                var format = "yyyy-MM-dd'T'HH:mm:ss";
                var curTZone = TimeZoneInfo.Local;
                var dateStart = new DateTimeOffset(Details.FromDate, curTZone.GetUtcOffset(Details.FromDate));
                var dateEnd = new DateTimeOffset(Details.ToDate, curTZone.GetUtcOffset(Details.ToDate));
                var startTimeString = dateStart.DateTime.ToString(format);
                var endTimeString = dateEnd.DateTime.ToString(format);




                EventDateTime start = new EventDateTime();
                //start.DateTime = Details.FromDate;
                start.DateTimeRaw = startTimeString;
                start.TimeZone = "America/New_York";
                EventDateTime end = new EventDateTime();
                // end.DateTime = Details.ToDate;
                end.DateTimeRaw = endTimeString;
                end.TimeZone = "America/New_York";
                ev.Start = start;
                ev.End = end;
                ev.Summary = fullMessage;
                ev.Description = Description.ToString();


                var recurringEvent = service.Events.Insert(ev, calendarId).Execute();


                return true;
            }
            catch (Exception e)
            {

                ErrorSignal.FromCurrentContext().Raise(e);
                return false;
            }

        }
    }
}