using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class EventsAdminPageController : Controller
    {
        // GET: EventsAdminPage
        public ActionResult Index()
        {
            Events classEvent = new Events();
             var list = new List<Events>();

            if (User.IsInRole("DoorCheckin"))
            {

                list = classEvent.GetCheckInEvents();
            }
            else
            {
                list = classEvent.GetAllEventsForAdmins();
            }
            return View(list);
        }

        // GET: EventsAdminPage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventsAdminPage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventsAdminPage/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EventsAdminPage/Edit/5
        public ActionResult Edit(Guid eventID)
        {
            GuestList GuestClass = new GuestList();
            Events eventclass = new Events();

            var list = GuestClass.GetEventGeustList(eventID);        
    
            var currentevent = eventclass.getSingleEvent(eventID);
            var currenteventdetailslist = eventclass.GetGuestListForEvent(User.Identity.Name, eventID);

            var stringdate = currentevent.eventDate.ToString("dddd, dd MMMM hh:mm tt");
            ViewBag.EventTitle = currentevent.eventName + " " + stringdate;
            ViewBag.EventID = eventID;
            var groupedlist = list.GroupBy(GuestList => GuestList.UserID);

            return View(groupedlist);
        }

        public ActionResult Check(Guid guestID, Guid EventID)
        {
            GuestList GuestClass = new GuestList();
            var list = GuestClass.CheckInGuest(guestID);

            return RedirectToAction("Edit", new { eventID = EventID });
        }
        public ActionResult UnCheck(Guid guestID, Guid EventID)
        {
            GuestList GuestClass = new GuestList();
            var list = GuestClass.UnCheckInGuest(guestID);

            return RedirectToAction("Edit", new { eventID = EventID });
        }
        // POST: EventsAdminPage/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EventsAdminPage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventsAdminPage/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
