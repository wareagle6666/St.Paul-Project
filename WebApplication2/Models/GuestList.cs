using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class GuestList
    {
        public Guid EventID { get; set; }
        public Guid UserID { get; set; }
        public Guid GuestID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isDeacon { get; set; }
        public bool isDeleted { get; set; }
        public DateTime timeStamp { get; set; }
    }
}