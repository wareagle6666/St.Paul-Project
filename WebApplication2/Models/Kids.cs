using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Kids
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Father { get; set; }

        public string Mother { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public string City { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string Grade { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Attended { get; set; } = false;
        public List<Notes> Notes { get; set; }

    }
}