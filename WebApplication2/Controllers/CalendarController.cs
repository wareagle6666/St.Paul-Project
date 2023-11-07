
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    [AllowAnonymous]
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }
    }
}