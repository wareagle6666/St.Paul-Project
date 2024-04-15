using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class PriestConfContController : Controller
    {
        // GET: PriestConfCont
        public ActionResult Index()
        {
            var confList = new ConfSlot();
            var ConfList = confList.GetConfSlotbyPriest();
            return View(ConfList);
        }

        // GET: PriestConfCont/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PriestConfCont/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                var CurrentUser = "fr.stevensaintpaul@gmail.com";
                // TODO: Add insert logic here
                List<string> fullnames = new List<string>();
                foreach (var item in collection.AllKeys)
                {

                    fullnames.Add(item);
                }

                for (var i = 0; i < fullnames.Count();)
                {
                    var NewConf = new ConfSlot();
                    NewConf.Priest = CurrentUser;
                    NewConf.Date = Request[fullnames[i]];
                    var timeconvert = DateTime.Parse(Request[fullnames[i + 1]]);
                    NewConf.fromDate = timeconvert.ToString("h:mm tt");
                    var timeconvert2 = DateTime.Parse(Request[fullnames[i + 2]]);
                    NewConf.toDate = timeconvert2.ToString("h:mm tt");
                    NewConf.InserConfSlotsConfSlot(NewConf);
                    i = i + 3;
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: PriestConfCont/Delete/5
        public ActionResult Delete(int id)
        {
            var NewConf = new ConfSlot();
            NewConf.DeleteConfSlot(id);

            return RedirectToAction("Index");
        }


    }

}