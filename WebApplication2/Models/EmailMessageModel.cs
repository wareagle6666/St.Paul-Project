using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class EmailMessageModel
    {
        //
        // Summary:
        //     Destination, i.e. To email, or SMS phone number
        public  string Destination { get; set; }

        //
        // Summary:
        //     Subject
        public  string Subject { get; set; }

        //
        // Summary:
        //     Message contents
        public  string Body { get; set; }

        public List<string> sentToo { get; set; } = new List<string>();


    }
}