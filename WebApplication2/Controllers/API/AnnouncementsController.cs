using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using WebApplication2.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Elmah;
using WebApplication2.Models.Interface;
using System.Web.Mvc;


namespace WebApplication2.Controllers.API
{
    public class AnnouncementsController  : ApiController
    {
        public AnnouncementsController()
        {
        }

        public async Task<List<PdfFiles>> GetAnnoucements()
        {
            var _dataprovider = new SqlDataProvider();
            var result = _dataprovider.GetAllAnnouncementFiles();

            return result;
        }


        public async Task<PdfFiles> GetFileById(int Id)
        {
            var fIles = new PdfFiles();
            var Announcement = fIles.GetAnnouncementById(Id);

            if (Announcement.FileType.Length == 0)
            {
                throw new Exception();
            }

            return Announcement;
        }
    }
}