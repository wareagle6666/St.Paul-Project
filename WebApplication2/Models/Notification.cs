using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Notification
    {
        public string body { get; set; } = String.Empty;
        public string title { get; set; } = String.Empty;
        public string click_action { get; set; } = String.Empty;
        public string sound { get; set; } = String.Empty;
        public int badge { get; set; } 
    }
}