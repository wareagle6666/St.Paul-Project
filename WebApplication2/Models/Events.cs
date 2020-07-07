using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Events
    {
        private SqlDataProvider _dataProvider;

        public Events()
        {
            _dataProvider = new SqlDataProvider();
        }
        public IEnumerable<GuestList> GuestList { get; set; }

        public List<Events> getAllEvents()
        {

            var list = _dataProvider.GetAllEvents();
            return list;
        }
        public Events getSingleEvent(Guid eventID)
        {

            var list = _dataProvider.GetSingleEvent(eventID);
            return list;
        }
        public int  addGuest(string UserName, Guid eventID, string firstName, string lastName, bool isDeacon) {
            var result = _dataProvider.addGuest(UserName,eventID, firstName, lastName, isDeacon);
            return result;
        }

        public List<Events> getAllEventsForUser(string userName)
        {

            var list = _dataProvider.GetAllEventsForUser(userName);
            return list;
        }

        public int DeleteSingleEvent(string userName, Guid eventID)
        {
            var result = _dataProvider.DeleteSingleEvent(userName, eventID);
            return result;
        }

        public List<GuestList> GetGuestListForEvent(string userName, Guid eventID)
        {
            var result = _dataProvider.GetGuestListForEvent(userName, eventID);
            return result;
        }
        public List<Events> GetCheckInEvents()
        {
            var list = _dataProvider.GetCheckInEvents();
            return list;
        }

        public Guid eventID { get; set; }
        public string eventName { get; set; }
        public DateTime eventDate { get; set; }
        public int eventCount { get; set; }
        public int eventRSVP { get; set; }
        public bool isLocked { get; set; }
        public int GuestCount { get; set; }


    }
}