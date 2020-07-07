using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class RegistrationPageController : Controller
    {
        // GET: RegistrationPage
        public ActionResult Index()
        {
            Events eventclass = new Events();
            var listOfEvents = new Events().getAllEventsForUser(User.Identity.Name);

            if(listOfEvents.Count> 0)
            {
                foreach (var item in listOfEvents)
                {
                    var list = eventclass.GetGuestListForEvent(User.Identity.Name, item.eventID);
                    item.GuestCount = list.Count;
                }
            }
            return View(listOfEvents);
        }

        // GET: RegistrationPage/Details/5
        public ActionResult Details(Guid eventID)
        {
            Events eventclass = new Events();

            var currentevent = eventclass.getSingleEvent(eventID);
            var currenteventdetailslist = eventclass.GetGuestListForEvent(User.Identity.Name, eventID);

            var stringdate = currentevent.eventDate.ToString("dddd, dd MMMM hh:mm tt");


            ViewBag.EventTitle = currentevent.eventName + " " + stringdate;
            ViewBag.GuestCount = currenteventdetailslist.Count;


            return View(currenteventdetailslist);
        }

        //// POST: RegistrationPage/Delete/5
        //[HttpPost]
        public ActionResult Delete(Guid eventID)
        {
            try
            {
                // TODO: Add delete logic here
                var value = new Events().DeleteSingleEvent(User.Identity.Name, eventID);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return View();
            }
        }
    }
}
