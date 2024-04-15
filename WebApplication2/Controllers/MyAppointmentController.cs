using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class MyAppointmentController : Controller
    {
        // GET: MyAppointment
        public ActionResult Index()
        {

            var _dataprovider = new SqlDataProvider();

            var result = _dataprovider.GetUserAppointes(User.Identity.Name);

            return View(result);
        }
    }
}