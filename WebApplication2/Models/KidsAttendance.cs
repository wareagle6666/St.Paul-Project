using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class KidsAttendance
    {
        public int ID { get; set; }
        public string Student { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ServantName { get; set; }

    }
}