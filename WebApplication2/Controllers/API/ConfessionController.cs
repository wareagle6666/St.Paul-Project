using Elmah;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Web.Helpers;

namespace WebApplication2.Controllers.API
{
    public class ConfessionController : ApiController
    {
        private EmailService emailService = new EmailService();
        public async Task<string> BookAnAppointment(int ConfID, string Email, string Message)
        {

            var confList = new ConfSlot();
            var SlotDetail = confList.GetConfSlotbyID(ConfID);
            var booking = new ConfBooking();
            var user = new Users();
            var UserEmailModel = new EmailMessageModel();
            var PriestEmailModel = new EmailMessageModel();

            var currentUser = user.GetUserByUsername(Email);



            var fromDate = SlotDetail.Date + " " + SlotDetail.fromDate;
            var toDate = SlotDetail.Date + " " + SlotDetail.toDate;

            booking.Title = currentUser.FirstName + " " + currentUser.LastName + " : " + DateTime.Parse(SlotDetail.Date).DayOfWeek.ToString() + " " + SlotDetail.Date + " between " + SlotDetail.fromDate + " - " + SlotDetail.toDate;
            booking.FromDate = Convert.ToDateTime(fromDate);
            booking.ToDate = Convert.ToDateTime(toDate);

            booking.Fname = currentUser.FirstName;
            booking.LFname = currentUser.LastName;
            booking.Email = currentUser.Email;
            booking.Phone = currentUser.PhoneNumber;
            booking.Message = Message;
            booking.SlotID = ConfID;
            booking.Priest = SlotDetail.Priest;

            string private_key = "";
            string calendarId = @"";
            string client_email = @"";

            private_key = "-----BEGIN PRIVATE KEY-----\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCj57qr05OV8WZy\nKnAUHFwUbbUD76GhdcOZwIM4/y0DbyIPibSb0jlNl3CzmJYn2LT+/CibM9nIV1Jv\n7I+pBT/N9yJ4aAGygvfDcV/RRRveVuT8HTdQ1nG8jpEOE3uYXSNjBUSsXCTC2u7g\nRVvvZOEtgkkiQQziBHwzalodRSC8VpHt0O5FVHtNyss3074IM1XxiRBYsp8+k6CJ\nh/6RQ9IuO9CqewpWekTN+hZU45TFbjIVXArm0/sgsQBWBLepY/8Fnk6scMruYxAp\nEtbHLLJet4GdnsEd7BcUsXPq/mum06ck4Du6WphR4TtGK9/RTBolSyYx9puSCwOG\n2hoHv9V7AgMBAAECggEAQWuGmHqUV67IgxzpKz2+ivDW5UFKNBW7Aq1SMve20b4T\nzDvceK1J7exaMXTfyfu3Emc3Fet2K36fCslS9dWAiAbyHTj2JHgdyEaCRhbdbrsk\nHnTS1VMihm4o+4NPO5tBOo6pwFnu2k9kNOO6NC0Bq5Zq9l8cc0HzCNqUKUakMEeB\n4mulNMpT0yqzGVKlXye0Bgp3dtWz67rJYwVFzXJp0KTKrKwIjgBKtYta4fpZdWtu\n+zJGlFA4NwbK7JcZbKZ/korkB4lyd6Jd2BJsuTYajtgLMOh/R4Y648ZAgT07A0ky\nDi68P94OFRkAI8SOit0hJXODMPvs+Nb8hH7xjsYtAQKBgQDPqg/moeyYsB4smmRB\natnIoCoHcTxSDtWNEu98/qziXlku0WTUk+KJar4ieiMZQ7RULiLyGoARQK9yqY16\nwWYcAEGsC8ap6n50elhst7atYgsSWVhZCV4JWgiXjbssaTxM7ojGezF6RA7BUBCM\nK06I0dBVYJDMG5wVx4bkuo31wQKBgQDKDjm5DAetFnTVOXUO8nG7sI07lh+ViQJ5\nWDEckSib5AU0mEC7HPRipMPip/Jra3Fc96H+IisyIK+osOOdj9qI/iyEshwLv3nX\nLmcQKdt9c6d2cNi8NNYPWosRQaq1mHzlSX8H9nZQstWnnlU9xWOuix7QigCx4Tna\nHs8lbiiyOwKBgBmX3v/QowUqZ32dxY9eaNrWCngCV6nRVDZfyA5CoPIBKMKhYejl\nFP1Un3xGSz8Y3gel6/6kj3YQ386k4N/pJ/l5ep6GkQRt9wnJ7k3v0l7J/41SX5YR\nlnpWk0qCWDgf/COLHmY/1pg3/Q2MHY345GPuX9u13AKbwH/aqGBWMZrBAoGAbbxm\nGxrufFHK6BbJfXGs1TugqLDyvFrpzg9YtOQdQvUG4rahyT2DeKN1g36lCTQUIGKZ\nRxU1kobv/9T5+ZOsI2Svtu2oj7TRLk2USdIB09NhKtFE1Ip5i2MoThn05aVIh5pv\n9FoljdRidyNltiOi7KO/+5BHqlPhAJZk+DYowgsCgYBneJyWR266VXbNURW9qXyd\nIXme484WHG94nkxog1q0Jny6yG6dE+lnE+JgEB3guAQlzRTVpAQpxequkjbrWr1J\n6ic1W9nubMoNIsSOq/6tKLe1fhJV1z5Al5h1ToCObxTHVi/9Spw6AxUgGm3hjKQn\n7hzuKVlHgV+Jbtr2JTOCgA==\n-----END PRIVATE KEY-----\n";
            calendarId = @"84d0cb572deb25db4056e06ba881ab2669783ef7da536d3924a171b0001468e6@group.calendar.google.com";
            client_email = @"frstevenconf@stmary-314020.iam.gserviceaccount.com";

            try
            {
                var result = GoogleEventCreate(booking, client_email, private_key, calendarId);


                if (result)
                {

                    var results = SlotDetail.UpdateConfSlot(booking.SlotID);
                    var bookingResult = booking.insertConfBooking(booking);

                    if (results && bookingResult)
                    {


                        UserEmailModel.Body = $@"Hi " + booking.Fname + " " + booking.LFname + $@"<br><br><div>Your request for " + booking.Title + " booked successfully.";
                        UserEmailModel.Subject = "Appointment Booked: " + booking.Title;
                        UserEmailModel.sentToo.Add(booking.Email);

                        var UserEmail = emailService.ConfEmails(UserEmailModel);


                        var Priest = "Fr.Stevens";


                        var AlertTitle = DateTime.Parse(SlotDetail.Date).DayOfWeek.ToString() + " " + SlotDetail.Date + " between " + SlotDetail.fromDate + " - " + SlotDetail.toDate;
                        StringBuilder BookingDetail = new StringBuilder();
                        BookingDetail.AppendLine("Name: " + booking.Fname + " " + booking.Fname);
                        BookingDetail.AppendLine("Email: " + booking.Email);
                        BookingDetail.AppendLine("Phone: " + booking.Phone);
                        BookingDetail.AppendLine("Message: " + booking.Message);

                        var prestMessage = "Name: " + booking.Fname + " " + booking.Fname + "<br>" + "Email: " + booking.Email + "<br>" + "Phone: " + booking.Phone + "<br>" + "Message: " + booking.Message;


                        PriestEmailModel.Body = $@"Hi " + Priest + $@"<br><br><div>Appointment was booked for " + AlertTitle + " . <br><br>" + prestMessage;
                        PriestEmailModel.Subject = "New Appointment: " + AlertTitle;
                        PriestEmailModel.sentToo.Add("fr.stevensaintpaul@gmail.com");

                        var PrestEmail = emailService.ConfEmails(PriestEmailModel);


                    }
                    else
                    {
                        throw new Exception("booking failed");
                    }
                    //send email
                    return "Success";
                }
                else
                {
          
                    return "Failed";
                }
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return "Failed";
            }

        }



        public async Task<List<ConfSlot>> GetBookingTimes()
        {
            var confList = new ConfSlot();
            var ConfList = confList.GetConfSlotbyPriest();
            return ConfList;
        }

        public async Task<List<UserAppointesModel>> GetUserCurrentAppointments(string email)
        {
            var _dataprovider = new SqlDataProvider();

            var result = _dataprovider.GetUserAppointes(email);
            return result;
        }


        private bool GoogleEventCreate(ConfBooking Details, string client_email, string private_key, string calendarId)
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

                String fullMessage = "Appointment for " + Details.Fname + " " + Details.Fname + " " + Details.Phone;

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