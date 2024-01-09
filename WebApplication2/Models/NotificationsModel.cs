using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class NotificationsModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = String.Empty;
        public string Title { get; set; } = String.Empty;
        public string Subtitle { get; set; } = String.Empty;
        public DateTime CreateDate { get; set; } 
        public string UserId { get; set; } = String.Empty;
        public string Token { get; set; } = String.Empty;
    }
}