using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult Index()
        {
            var imageList = new Image();

            var images = imageList.GetGalleryImages();
            return View(images);
        }
        public ActionResult RenderPhoto(byte[] image)
        {
            return File(image, "image/jpeg");
        }
    }
}