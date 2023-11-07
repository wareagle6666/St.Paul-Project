using System;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using Elmah;
using System.IO;

namespace WebApplication2.Controllers
{
    public class NewsController : Controller
    {
        News News = new News();
        // GET: News
        public ActionResult Index()
        {
            var result = News.GetAlltNews();
            return View(result);
        }
        [Authorize]
        public ActionResult Admin()
        {
            var result = News.GetAlltNews();
            var count = 0;

            foreach (var item in result)
            {
                if (item.isSpotlight == true)
                {
                    count++;
                }
            }
            ViewBag.Count = count;

            return View(result);
        }
        // GET: News/Details/5
        public ActionResult Details(Guid NewsID)
        {
            var singleNews = News.GetSingleNews(NewsID);
            return View(singleNews);
        }
        // [Authorize]
        // GET: News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var result = News.GetAlltNews();
                var count = 0;

                foreach (var item in result)
                {
                    if (item.isSpotlight == true)
                    {
                        count++;
                    }
                }
                Guid imgID = Guid.Empty;
                News NewNews = new News();

                Image img = new Image();
                img.ImageTitle = Request.Files[0].FileName;
                if (img.ImageTitle != null && img.ImageTitle != "")
                {
                    MemoryStream ms = new MemoryStream();
                    Request.Files[0].InputStream.CopyTo(ms);
                    img.ImageData = ms.ToArray();

                    imgID = img.SaveNewsImage(img);
                }

                NewNews.NewsTitle = Request["Newstitle"];
                NewNews.Link = Request["Link"];
                if (count != 3 || count < 3)
                {
                    NewNews.isSpotlight = Convert.ToBoolean(Request["IsSpotlight"]);
                }
                else
                {
                    NewNews.isSpotlight = false;
                }

                NewNews.EnglishText = Request["Newsbodyenglish"];
                NewNews.ArabicText = Request["Newsbodyarabic"];
                NewNews.ImageID = imgID;
                NewNews.Created = DateTime.Now;
                NewNews.SaveNews(NewNews);


                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return View();
            }
        }

        // GET: News/Edit/5
        public ActionResult Edit(Guid NewsID)
        {
            var singleNews = News.GetSingleNews(NewsID);
            return View(singleNews);
        }
        public ActionResult MakeSpotlight(Guid NewsID)
        {
            var Result = News.MakeSpotlight(NewsID);
            return RedirectToAction("Admin");
        }
        public ActionResult RemoveMakeSpotlight(Guid NewsID)
        {
            var Result = News.RemoveMakeSpotlight(NewsID);
            return RedirectToAction("Admin");
        }
        // POST: News/Edit/5
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, Guid ID, bool IsSpotlight, FormCollection collection)
        {
            try
            {

                var singleNews = News.GetSingleNews(ID);

                if (file != null)
                {
                    MemoryStream ms = new MemoryStream();
                    file.InputStream.CopyTo(ms);
                    singleNews.ImageData = ms.ToArray();

                    singleNews.ImageTitle = file.FileName;

                }


                singleNews.NewsTitle = Request["Newstitle"];
                singleNews.Link = Request["Link"];
                singleNews.isSpotlight = IsSpotlight;
                singleNews.EnglishText = Request["EnglishText"];
                singleNews.ArabicText = Request["ArabicText"];

                singleNews.UpdateNews(singleNews);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
                return View();
            }
        }

        // GET: News/Delete/5
        public ActionResult Delete(Guid NewsID)
        {
            News.DeleteNews(NewsID);
            return RedirectToAction("Admin");
        }

        //// POST: News/Delete/5
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
