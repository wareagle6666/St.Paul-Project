using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Notification
    {
        public string body { get; set; }
        public string title { get; set; }
        public string click_action { get; set; }
    }
}