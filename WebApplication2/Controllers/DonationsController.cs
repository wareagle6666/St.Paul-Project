﻿using Elmah;
using System;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    [RouteArea("MyArea")]
    public class DonationsController : Controller
    {
        [Route("~/Donation")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Donations/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Donations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donations/Create
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

        // GET: Donations/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Donations/Edit/5
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

        // GET: Donations/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Donations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

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
