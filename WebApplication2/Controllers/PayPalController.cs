﻿using Elmah;
using System;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class PayPalController : Controller
    {
        // GET: PayPal
        public ActionResult Index()
        {
            return View();
        }

        // GET: PayPal/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PayPal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PayPal/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return View();
            }
        }

        // GET: PayPal/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PayPal/Edit/5
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

        // GET: PayPal/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PayPal/Delete/5
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
