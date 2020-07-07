using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Models
{

    public class EventEditingController : Controller
    {
        // GET: EventEditing
        public ActionResult Index(Guid eventID)
        {
            Events eventclass = new Events();

            var currentevent = eventclass.getSingleEvent(eventID);
            var currenteventdetailslist = eventclass.GetGuestListForEvent(User.Identity.Name, eventID);

            var stringdate = currentevent.eventDate.ToString("dddd, dd MMMM hh:mm tt");

            ViewBag.EventID = eventID;
            ViewBag.EventTitle = currentevent.eventName + " " + stringdate;
            ViewBag.GuestCount = currenteventdetailslist.Count;
            return View(currenteventdetailslist);
        }


        // GET: EventEditing/Edit/5
        public ActionResult Edit(Guid GuestID)
        {
            GuestList guestDetail = new GuestList();

            var guest = guestDetail.GetSingleGuestDetails(GuestID);

            return View(guest);
        }


        // POST: EventEditing/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "GuestID,FirstName,LastName,isDeacon")] GuestList guestEdit)
        {
            try
            {
                GuestList guestClass = new GuestList();

                var result = guestClass.UpdateSingleGuest(guestEdit);
                return RedirectToAction("Index", "RegistrationPage", new { area = "" });
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Remove(Guid GuestID, Guid eventID)
        {
            try
            {
                // TODO: Add delete logic here
                GuestList guestClass = new GuestList();
                var result = guestClass.DeleteSingleGuest(GuestID);

                return RedirectToAction("Index", new { eventID = eventID });
            }
            catch
            {
                return View();
            }
        }


    }
}
