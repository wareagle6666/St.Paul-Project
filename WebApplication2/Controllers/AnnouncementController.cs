using System;
using System.IO;
using System.Web.Mvc;
using WebApplication2.Models;
using Elmah;

namespace WebApplication2.Controllers
{
    public class AnnouncementController : Controller
    {
        // GET: Announcement
        public ActionResult Index()
        {
            var fIles = new PdfFiles();
            var list = fIles.GetFiles();
            return View(list);
        }
        [Authorize]
        public ActionResult AnnouncementAdmin()
        {
            var fIles = new PdfFiles();
            var list = fIles.GetFiles();

            return View(list);
        }
        // GET: Announcement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize]
        // GET: Announcement/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFiles(FormCollection form)
        {

            try
            {
                var DisplayDateValue = form["DisplayDate"];
                var files = Request.Files;
                if (files[0].FileName == "")
                {
                    ViewBag.ErrorMessage = "You didn't add an image";
                    return View("Index");
                }

                for (var i = 0; i < Request.Files.Count; i++)
                {
                    PdfFiles PDFfile = new PdfFiles();
                    PDFfile.FileTitle = files[i].FileName;
                    PDFfile.DisplayDate = DisplayDateValue;
                    MemoryStream ms = new MemoryStream();
                    files[i].InputStream.CopyTo(ms);
                    PDFfile.FileData = ms.ToArray();
                    PDFfile.FileType = "application/pdf";
                    PDFfile.SaveFile(PDFfile, User.Identity.Name);
                }
                ViewBag.Message = "Image(s) stored in atabase!";

                return RedirectToAction("AnnouncementAdmin");
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);

                return RedirectToAction("AnnouncementAdmin");
            }
        }
   
        public ActionResult RenderFile(int Id)
        {
           
            var fIles = new PdfFiles();
            var Announcement = fIles.GetAnnouncementById(Id);

            if(Announcement.FileType.Length == 0)
            {
                throw new Exception();
            }
      
            return new FileContentResult(Announcement.FileData, Announcement.FileType);
        }
        public ActionResult DeleteFiles(int Id)
        {
            var fIles = new PdfFiles();
            var result = fIles.DeleteAnnouncementFile(Id);


            return RedirectToAction("AnnouncementAdmin");
        }

        // POST: Announcement/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                //var result = News.GetAlltNews();
                //var count = 0;

                //foreach (var item in result)
                //{
                //    if (item.isSpotlight == true)
                //    {
                //        count++;
                //    }
                //}
                //Guid imgID = Guid.Empty;
                //News NewNews = new News();

                //Image img = new Image();
                //img.ImageTitle = Request.Files[0].FileName;
                //if (img.ImageTitle != null && img.ImageTitle != "")
                //{
                //    MemoryStream ms = new MemoryStream();
                //    Request.Files[0].InputStream.CopyTo(ms);
                //    img.ImageData = ms.ToArray();

                //    imgID = img.SaveNewsImage(img);
                //}

                //NewNews.NewsTitle = Request["Newstitle"];
                //NewNews.Link = Request["Link"];
                //if (count != 3 || count < 3)
                //{
                //    NewNews.isSpotlight = Convert.ToBoolean(Request["IsSpotlight"]);
                //}
                //else
                //{
                //    NewNews.isSpotlight = false;
                //}

                //NewNews.EnglishText = Request["Newsbodyenglish"];
                //NewNews.ArabicText = Request["Newsbodyarabic"];
                //NewNews.ImageID = imgID;
                //NewNews.Created = DateTime.Now;
                //NewNews.SaveNews(NewNews);


             
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return View();
            }
        }

        // GET: Announcement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Announcement/Edit/5
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

        // GET: Announcement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Announcement/Delete/5
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
