﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.Interface;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class DEACONSController : Controller
    {

        // GET: DEACONS
        public ActionResult Index()
        {

            IList<Users> studentLis = new Users().getusers();
            ViewData["users"] = studentLis;
            return View();
        }
    }
}