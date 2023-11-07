using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;
using WebApplication2.Models;
using Elmah;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class AddGalleryImagesController : Controller
    {
        private SqlDataProvider _datarepo = new SqlDataProvider();
        // GET: AddGalleryImages
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage()
        {

            try
            {
                var files = Request.Files;
                if (files[0].FileName == "")
                {
                    ViewBag.ErrorMessage = "You didn't add an image";
                    return View("Index");
                }
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    Image img = new Image();
                    img.ImageTitle = files[i].FileName;

                    MemoryStream ms = new MemoryStream();
                    files[i].InputStream.CopyTo(ms);
                    img.ImageData = ms.ToArray();
                    img.ImageType = 0;
                    img.SaveImage(img);
                }
                ViewBag.Message = "Image(s) stored in atabase!";
                return View("Index");
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return View();
            }
        }

        public ActionResult UploadSingeImage()
        {

            try
            {

                var files = Request.Files;
                if(files[0].FileName == "") {
                    ViewBag.ErrorMessage = "You didn't add an image";
                    return View("Index");
                }
                Image img = new Image();
                img.ImageTitle = files[0].FileName;

                MemoryStream ms = new MemoryStream();
                files[0].InputStream.CopyTo(ms);
                img.ImageData = ms.ToArray();
                img.ImageType = 1;
                img.SaveImage(img);

                ViewBag.Message = "Image(s) stored in atabase!";
                return View("Index");
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return View();
            }
        }


        public ActionResult EventScreen()
        {
            var list = _datarepo.GetListOfImageEvents();
            return View(list);
        }

        public ActionResult CreateImageEvent()
        {
            return View();
        }

        public ActionResult EditImageEvent()
        {
            return View();
        }

        //// POST: Kids/Create
        //[HttpPost]
        //public ActionResult Create(ImageEvent Kid)
        //{
        //    try
        //    {
        //        var result = 1;//_datarepo.CreateKid(Kid, User.Identity.Name);

        //        if (result == 1)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            throw new Exception("Couldn't Save Record");
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        var test = ex.Message;
        //        return View();
        //    }
        //}

        //// GET: Kids/Edit/5
        //public ActionResult Edit(int ID)
        //{
        //    var kidsClasses = new KidsController();

        //    var kid = _datarepo.GetSingleKidByID(ID);
        //    var classes = kidsClasses.GetClasses(kid.ClassID);

        //    foreach (var c in classes)
        //    {
        //        if (c.Value == kid.ClassID.ToString())
        //        {
        //            ViewBag.DefaultID = c.Text;
        //        }
        //    }
        //    ViewData["Classes"] = classes;
        //    ViewBag.Classes = classes;
        //    return View(kid);
        //}

        //// POST: Kids/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int Id, ImageEvent Kid)
        //{
        //    try
        //    {
        //        var result = _datarepo.UpdateKid(Kid, User.Identity.Name);

        //        if (result == 1)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            throw new Exception("Couldn't Save Record");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var test = ex.Message;
        //        return View();
        //    }
        //}


    }
}
