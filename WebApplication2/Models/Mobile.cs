using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Mobile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = String.Empty;
        public string Token { get; set; } = String.Empty;
        public string DeviceType { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; } 
        public DateTime LastLoggedIn { get; set; } 
    }
}