using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class RegistrationPageController : Controller
    {
        // GET: RegistrationPage
        public ActionResult Index()
        {
            var listOfEvents = new Events().getAllEventsForUser(User.Identity.Name); 
            return View(listOfEvents);
        }

        // GET: RegistrationPage/Details/5
        public ActionResult Details(Guid eventID)
        {
            return View();
        }

        // GET: RegistrationPage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistrationPage/Create
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

        // GET: RegistrationPage/Edit/5
        public ActionResult Edit(Guid eventID)
        {
            return View();
        }

        // POST: RegistrationPage/Edit/5
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
                ErrorSignal.FromCurrentContext().Raise(e.InnerException);
                return View();
            }
        }

        //// GET: RegistrationPage/Delete/5
        //public ActionResult Delete(Guid eventID)
        //{
        //    Delete(User.Identity.Name, eventID);

        //    return RedirectToAction("Index");
        //}

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
                ErrorSignal.FromCurrentContext().Raise(e.InnerException);
                return View();
            }
        }
    }
}
