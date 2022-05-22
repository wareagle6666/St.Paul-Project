using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class NotesController : Controller
    {
        private SqlDataProvider _datarepo = new SqlDataProvider();
        // GET: Notes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Notes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Notes/Create
        public ActionResult Create(int ID)
        {
            var note = new Notes();
            note.StudentID = ID;
            return View(note);
        }

        // POST: Notes/Create
        [HttpPost]
        public ActionResult Create(Notes Not)
        {

            try
            {
                var result = _datarepo.AddKidsNote(Not.Note, Not.ID, User.Identity.Name);

                return RedirectToAction("Details", "Kids", new { Not.ID });
            }
            catch
            {
                return View();
            }
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _datarepo.GetsingleNote(id);
            return View(data);
        }

        // POST: Notes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Notes note)
        {
            try
            {
         
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int id, int StudentID)
        {
            var data = _datarepo.DeleteKidsNote(id);
            var ID = StudentID;
            return RedirectToAction("Details", "Kids", new { ID });
        }

        // POST: Notes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
