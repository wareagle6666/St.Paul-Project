using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Feasts
    {
        public Feasts()
        {
        }
        public Feasts(string Name, string Date )
        {
            FeastDate = Date;
            FeastName = Name;

        }
        public string FeastName { get; set; } 
        public string FeastDate { get; set; }
    }
}