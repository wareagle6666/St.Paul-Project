﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}