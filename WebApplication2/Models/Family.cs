using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Family
    {
        private SqlDataProvider _dataProvider;

        public Family()
        {
            _dataProvider = new SqlDataProvider();
        }
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Grade { set; get; }
        public string Relation { get; set; }

        public List<Family> GetFamilies(string ID)
        {

            var list = _dataProvider.GetFamilies(ID);

            return list;
        }
    }
}