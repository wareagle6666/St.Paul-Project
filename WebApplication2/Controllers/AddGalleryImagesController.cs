﻿using System;
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





    }
}
