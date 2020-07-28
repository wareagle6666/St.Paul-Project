using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class GuestList
    {
        private SqlDataProvider _dataProvider;
        public GuestList()
        {
            _dataProvider = new SqlDataProvider();
        }
        public GuestList GetSingleGuestDetails(Guid guestID)
        {
            var guest = _dataProvider.GetSingleGuestDetails(guestID);
            return guest; 
        }

        public int UpdateSingleGuest (GuestList guest)
        {
            var result = _dataProvider.UpdateSingleGuest(guest);
            return result;
        }
        public int DeleteSingleGuest(Guid guestID) {
 
          var result = _dataProvider.DeleteSingleGuest(guestID);
            return result;
        }

        public int CheckInGuest(Guid guestID)
        {

            var result = _dataProvider.CheckInGuest(guestID);
            return result;
        }
        public int UnCheckInGuest(Guid guestID)
        {

            var result = _dataProvider.UnCheckInGuest(guestID);
            return result;
        }
        public List<GuestList> GetEventGeustList (Guid eventID)
        {
            var list = _dataProvider.GetGuestFullListForEvent(eventID);
            return list;
        }
        public Guid EventID { get; set; }
        public Guid UserID { get; set; }
        public Guid GuestID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "is Deacon?")]
        public bool isDeacon { get; set; }
        public bool isDeleted { get; set; }
        public DateTime timeStamp { get; set; }
        public bool isCheckedIn { get; set; }
        public DateTime CheckIntimeStamp { get; set; }
        public string EventOwner { get; set; }
    }
}