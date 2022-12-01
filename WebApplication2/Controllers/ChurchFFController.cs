using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ChurchFFController : Controller
    {
        // GET: ChurchFF
        public ActionResult Index()
        {
            string url = "https://suscopts.org/coptic-orthodox/fasts-and-feasts/";
            var web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);


            var Title = doc.DocumentNode.SelectNodes("//h2")[0].InnerHtml;

            var FeastList = new List<Feasts>();
            try
            {
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//table"))
                {
                    var Name = "";
                    var Date = "";
                    var counter = 0;
                    foreach (var cell in node.SelectNodes("//td"))
                    {
                        if(counter == 0)
                        {
                            Name = cell.InnerHtml;
                            counter++;
                        }
                        else
                        {
                            Date = cell.InnerHtml;
                            FeastList.Add(new Feasts(Name, Date));
                            Name = "";
                            counter = 0;
                        }
                       
                    }
           
                }


                foreach(var item in FeastList)
                {
                    item.FeastDate = item.FeastDate.Replace("&ndash;", "- ");
                }

            }
            catch (Exception ex)
            {
                // handle error
                var test = ex.Message;
            }
            Title = Title.Replace("&amp;", " & ");
            Title = Title.Replace("&ndash;", " - ");
            ViewBag.FeastTitle = Title;
            ViewBag.List = FeastList;
            return View();
        }
    }
}