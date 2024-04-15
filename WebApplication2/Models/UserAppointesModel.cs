using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class UserAppointesModel
    {
        public int ID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string FromDate { get; set; }
        public string ToDate { get; set; }

    }
}