using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class AttendanceClass
    {
        public int? ID { get; set; }
        public int? StudentId { get; set; }
        public bool Attended { get; set; } = false;
        public DateTime? CreatedDate { get; set; }

    }
}