using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using Microsoft.AspNet.Identity;
using Elmah;
using System.Text;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class ChurchEventPagesController : Controller
    {
        private Guid currentEventID;
        private EmailService emailService = new EmailService();
        // GET: ChurchEventPages
        public ActionResult Index()
        {
            IList<Events> eventlist = new Events().getAllEvents();

            return View(eventlist);
        }

        //Sign up page 
        // GET: ChurchEventPages/Create/ID
        public ActionResult Create(Guid eventID)
        {

            Events eventDetails = new Events().getSingleEvent(eventID);
            var stringdate = eventDetails.eventDate.ToString("dddd, dd MMMM hh:mm tt");
            ViewBag.EventTitle = eventDetails.eventName + " " + stringdate;

            ViewBag.CurrentCount = eventDetails.eventRSVP;
            ViewBag.currentEventID = eventID;
            return View(eventDetails);
        }

        // POST: ChurchEventPages/Create
        [HttpPost]
        public ActionResult Create(IList<GuestList> contactList)
        {
            StringBuilder listofnames = new StringBuilder();
            try
            {
                if (contactList.Count > 0 ) {
                    var methods = new Events();
                    var username = User.Identity.Name;
                    foreach (var item in contactList) {
                        var result = methods.addGuest(username, item.EventID, item.FirstName, item.LastName, item.isDeleted);
                        listofnames.AppendFormat("{0} {1}", item.FirstName, item.LastName);
                    }

                    Events eventDetails = new Events().getSingleEvent(contactList[0].EventID);
                    var message = new IdentityMessage();

                    message.Destination = User.Identity.Name;
                    message.Subject = "Event Regestration " + DateTime.Now.ToString("dddd, dd MMMM hh:mm tt");
                    message.Body = "You have regisetered for " + eventDetails.eventName + " " + eventDetails.eventDate.ToString("dddd, dd MMMM hh:mm tt") + " <br/> with total of " + contactList.Count.ToString() + " individual(s) the following name(s): <br/> <br/>  " + listofnames + " <br/><br/> To edit or adjust your registrations please visit <a href=" + "https://www.stpaulatlanta.org/" + ">St.Paul Church</a>";
                    emailService.EventEmail(message);
                }
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return View();
            }
        }

        // POST: ChurchEventPages/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return View();
            }
        }

        // GET: ChurchEventPages/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}
