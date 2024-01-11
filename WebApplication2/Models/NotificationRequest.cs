using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class NotificationRequest
    {
        public string to { get; set; } = String.Empty;
        public Notification notification { get; set; }
        public NotificationData data { get; set; }
        public string priority { get; set; } = String.Empty;
    }
}