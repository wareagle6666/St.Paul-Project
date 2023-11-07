using System;
using System.Web.Mvc;
using WebApplication2.Models;
using Elmah;
using System.IO;

namespace WebApplication2.Controllers
{
    public class HymnsController : Controller
    {
        // GET: Hymns
        public ActionResult Index()
        {
            var fIles = new Hymns();
            var list = fIles.GetFiles();

            return View(list);
        }
        [Authorize]
        public ActionResult HymnsAdmin()
        {
            var fIles = new Hymns();
            var list = fIles.GetFiles();

            return View(list);
        }
        // GET: Hymns/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize]
        // GET: Hymns/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Audio(int Id)
        {
            var fIles = new Hymns();
            var HymnFile = fIles.GetHymnsById(Id);
          
            if(HymnFile.FileData.Length == 0)
            {
                throw new Exception();
            }
            MemoryStream ms = new MemoryStream(HymnFile.FileData);
        
            return new FileStreamResult(ms, "audio/mp3");
        }
        public ActionResult RenderFile(int Id)
        {

            var fIles = new Hymns();
            var HymnsFile = fIles.GetHymnsById(Id);

            if (HymnsFile.FileType.Length == 0)
            {
                throw new Exception();
            }

            return new FileContentResult(HymnsFile.FileData, HymnsFile.FileType);
        }
        [HttpPost]
        public ActionResult UploadFiles(FormCollection form)
        {

            try
            {
                var DisplayTitleValue = form["DisplayTitle"];
                var files = Request.Files;
                if (files[0].FileName == "")
                {
                    ViewBag.ErrorMessage = "You didn't add an image";
                    return View("Index");
                }

                for (var i = 0; i < Request.Files.Count; i++)
                {
                    Hymns HymnsFile = new Hymns();
                    HymnsFile.FileTitle = files[i].FileName;
                    HymnsFile.DisplayTitle = DisplayTitleValue;
                    MemoryStream ms = new MemoryStream();
                    files[i].InputStream.CopyTo(ms);
                    HymnsFile.FileData = ms.ToArray();
                    HymnsFile.FileType = "application/pdf";
                    HymnsFile.SaveFile(HymnsFile, User.Identity.Name);
                }
                ViewBag.Message = "Image(s) stored in atabase!";

                return RedirectToAction("HymnsAdmin");
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);

                return RedirectToAction("HymnsAdmin");
            }
        }


        public ActionResult DeleteFiles(int Id)
        {
            var fIles = new Hymns();
            var result = fIles.DeleteHymnsFile(Id);


            return RedirectToAction("HymnsAdmin");
        }

        // POST: Hymns/Create
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

        // GET: Hymns/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hymns/Edit/5
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

        // GET: Hymns/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hymns/Delete/5
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
