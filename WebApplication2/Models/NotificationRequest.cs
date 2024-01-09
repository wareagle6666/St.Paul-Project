using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class NotificationRequest
    {
        public string to { get; set; }
        public Notification notification { get; set; }
    }
}