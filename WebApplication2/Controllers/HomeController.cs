using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var imageList = new Image();

            var image = imageList.GetHomeImage();
            return View(image);
        }
    }
}
