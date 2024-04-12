using Elmah;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication2
{
    public static class Helpers
    {
        public static string toJsonStr<T>(this T o)
        {
            return JsonConvert.SerializeObject(o);
        }
        public static T fromJsonStr<T>(this string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }
        public static string GetHashString(this string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        public static byte[] GetHash(this string inputString)
        {
            using (HashAlgorithm algorithm = MD5.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
        //public static void SendEmail(this string subj, string from, List<string> toLst, List<string> ccLst, List<string> bccLst, string body, bool isHtml = false, List<string> replyToLst = null)
        //{
        //    if (toLst.Any(x => x.ToLower() == "none@none.com")) return;
        //    var filteredLst = new List<string>();


        //    try
        //    {
        //        var mail = new MailMessage();
        //        mail.From = new MailAddress(from);

        //        if (toLst != null) foreach (var x in toLst) mail.To.Add(x);
        //        if (ccLst != null) foreach (var x in ccLst) mail.CC.Add(x);
        //        if (bccLst != null) foreach (var x in bccLst) mail.Bcc.Add(x);
        //        SmtpClient client = new SmtpClient();
        //        client.Port = 587;
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.UseDefaultCredentials = false;

        //        if (isAmazon)
        //            client.Credentials = new NetworkCredential("AKIA2PP2UJMQEFUHD2GO", @"BMUlK6oC/B+Z/hY92FhGB+NzDp3njeTKLkOy8q/0HZDX");
        //        client.Host = "m05.internetmailserver.net";
        //        client.EnableSsl = false;
        //        if (isAmazon)
        //        {
        //            client.Host = "email-smtp.us-east-2.amazonaws.com";
        //            client.EnableSsl = true;
        //        }

        //        mail.Subject = subj;
        //        mail.Body = body;
        //        mail.IsBodyHtml = isHtml;
        //        if (replyToLst == null)
        //            mail.ReplyToList.Add(new MailAddress("sgamil@yahoo.com"));
        //        else
        //        {
        //            foreach (var r in replyToLst)
        //            {
        //                mail.ReplyToList.Add(r);
        //            }

        //        }


        //        try
        //        {
        //            client.Send(mail);
        //        }
        //        catch (Exception ex)
        //        {
        //            ErrorSignal.FromCurrentContext().Raise(ex);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorSignal.FromCurrentContext().Raise(ex);
        //    }
        //}
    }
}