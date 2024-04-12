using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ConfBooking
    {
        private SqlDataProvider _dataProvider;
        public ConfBooking()
        {
            _dataProvider = new SqlDataProvider();
        }

        public int SlotID { get; set; }
        public string Fname { set; get; }
        public string Title { set; get; }
        public string LFname { set; get; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public int ID { get; set; }
        public string Priest { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime DateTimeStamp { get; set; }

        //public bool insertConfBooking(ConfBooking booking)
        //{
        //    var result = _dataProvider.insertConfBooking(booking);
        //    return Convert.ToBoolean(result);
        //}

    }
}