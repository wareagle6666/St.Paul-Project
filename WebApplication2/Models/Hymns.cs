using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Hymns
    {
        private SqlDataProvider _dataProvider;
        public Hymns()
        {
            _dataProvider = new SqlDataProvider();
        }

        public int Id { get; set; }
        public string DisplayTitle { get; set; }
        public string FileTitle { get; set; }
        public byte[] FileData { get; set; }
        public string FileType { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }

        public int SaveFile(Hymns file, string Username)
        {
            var result = _dataProvider.SaveHymnsFiles(file, Username);
            return result;
        }

        public List<Hymns> GetFiles()
        {
            var result = _dataProvider.GetAllHymnsFiles();
            return result;
        }

        public int DeleteHymnsFile(int Id)
        {
            var restult = _dataProvider.DeleteHymnsFile(Id);
            return restult;
        }
        public Hymns GetHymnsById (int Id)
        {
            var result = _dataProvider.GetHymnsById(Id);
            return result;
        }
    }
}