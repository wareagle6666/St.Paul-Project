using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Calendar
    {
        public Calendar()
        {

        }
        public Calendar(string name, DateTime startime, DateTime endtime)
        {
            eventName = name;
            startTime = startime;
            endTime = endtime;
        }
        public Calendar(string[] name, string startime)
        {
            deacons = name;
            date = startime;
        }
        public string[] deacons { get; set; }
        public string date { get; set; }
        public DateTime endTime { get; set; }

        public string eventName { get; set; }
        public DateTime startTime { get; set; }
    }
}