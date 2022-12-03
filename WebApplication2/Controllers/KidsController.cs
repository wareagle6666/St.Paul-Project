using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class KidsController : Controller
    {
        private SqlDataProvider _datarepo = new SqlDataProvider();
        // GET: Kids
        public ActionResult Index()
        {
            if(User.IsInRole("Priest") || User.IsInRole("SuperAdmin"))
            {
                var kids = _datarepo.GetAllKids();
                foreach(var kid in kids)
                {
                    var result =  _datarepo.GetKidAttendance(kid.ID);
                    if(result != null)
                    {
                        kid.Attendance = result;
                    }
                    else
                    {
                        kid.Attendance = new AttendanceClass();
                    }
                   
                }
                var orderList = kids.OrderBy(x => x.Attendance.Attended).ToList();
                var orderList2 = (from x in kids orderby x.Attendance.Attended ascending select x).ToList();

                return View(orderList2);
            }
            else
            {
                var classID = _datarepo.GetServantClass(User.Identity.Name);

                var kids = new List<Kids>();

                foreach(var ID in classID)
                {
                    var Result = _datarepo.GetKidsByClassID(ID);
                    kids.AddRange(Result);
                }
               
                foreach (var kid in kids)
                {
                    var result = _datarepo.GetKidAttendance(kid.ID);
                    if (result != null)
                    {
                        kid.Attendance = result;
                    }
                    else
                    {
                        kid.Attendance = new AttendanceClass();
                    }
                }
                ViewBag.ClassCount = classID.Count();
                return View(kids);
            }
           
        }

        // GET: Kids/Details/5
        public ActionResult Details(int ID)
        {
           var kid =  _datarepo.GetSingleKidByID(ID);
            var notes = _datarepo.GetKidsNotes(ID);

            foreach (var note in notes)
            {
                note.CreatedBy = _datarepo.GetUserNameById(note.CreatedBy);
            }

            ViewBag.Notes = notes;
            return View(kid);
        }
        public ActionResult Attendance(int ID)
        {
            var kid = _datarepo.DeleteKidsNote(ID);
            var name = _datarepo.GetSingleKidByID(ID);

            ViewBag.Name = name.FirstName + " "+name.LastName;
  
            return View(kid);
        }
        // GET: Kids/Create
        public ActionResult Create()
        {
            var classes = GetClasses(0);
            ViewBag.Classes = classes;
            return View();
        }
        private List<SelectListItem> GetClasses(int ID)
        {
            var Values = _datarepo.GetAllClasses();
            List<SelectListItem> Classes = new List<SelectListItem>();

            if (ID != 0 )
            {
                foreach (var c in Values)
                {
                    if (c.ID == ID.ToString())
                    {
                        Classes.Add(new SelectListItem { Text = c.ClassName, Value = c.ID, Selected =true });

                    }
                    else
                    {
                        Classes.Add(new SelectListItem { Text = c.ClassName, Value = c.ID});
                    }
                }

            }
            else
            {
                foreach (var c in Values)
                {

                    Classes.Add(new SelectListItem { Text = c.ClassName, Value = c.ID });
                }

            }
            return Classes;
        }
        public ActionResult CheckIn(int ID)
        {
            var result = _datarepo.AddKidsAttendance(ID, User.Identity.Name);
            return RedirectToAction("Index");
        }
        public ActionResult UnCheckIn(int ID,int AttendanceID)
        {
            var result = _datarepo.RemoveKidsAttendance(ID, User.Identity.Name, AttendanceID);
            return RedirectToAction("Index");
        }
        // POST: Kids/Create
        [HttpPost]
        public ActionResult Create(Kids Kid, FormCollection form)
        {
            try
            {
                Kid.ClassID = Convert.ToInt32( form["Classes"]);
                var result  = _datarepo.CreateKid(Kid, User.Identity.Name);

                if (result == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("Couldn't Save Record");
                }


            }
            catch (Exception ex)
            {
                var test = ex.Message;
                return View();
            }
        }

        // GET: Kids/Edit/5
        public ActionResult Edit(int ID)
        {
            var kid = _datarepo.GetSingleKidByID(ID);
            var classes = GetClasses(kid.ClassID);

            foreach (var c in classes)
            {
                if(c.Value == kid.ClassID.ToString())
                {
                    ViewBag.DefaultID = c.Text;
                }
            }
            ViewData["Classes"] = classes;
            ViewBag.Classes = classes;
            return View(kid);
        }

        // POST: Kids/Edit/5
        [HttpPost]
        public ActionResult Edit(int ID, Kids Kid)
        {
            try
            {
                var result = _datarepo.UpdateKid(Kid, User.Identity.Name);

                if (result == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("Couldn't Save Record");
                }

            }
            catch (Exception ex)
            {
                var test = ex.Message;
                return View();
            }
        }

        //// GET: Kids/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Kids/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
