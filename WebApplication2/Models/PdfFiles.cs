using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models.Interface;

namespace WebApplication2.Models
{
    public class PdfFiles
    {
        private SqlDataProvider _dataProvider;
        public PdfFiles()
        {
            _dataProvider = new SqlDataProvider();
        }
        public int Id { get; set; }
        public string DisplayDate { get; set; }
        public string FileTitle { get; set; }
        public byte[] FileData { get; set; }
        public string FileType { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }


        public int SaveFile(PdfFiles file, string Username)
        {
            var result = _dataProvider.SaveFiles(file, Username);
            return result;
        }

        public List<PdfFiles> GetFiles()
        {
            var result = _dataProvider.GetAllAnnouncementFiles();
            return result;
        }

        public int DeleteAnnouncementFile(int Id)
        {
            var restult = _dataProvider.DeleteAnnouncementFile(Id);
            return restult;
        }

    }
}