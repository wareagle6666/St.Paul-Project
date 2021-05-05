using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Contacts
    {
        private SqlDataProvider _dataProvider;

        public Contacts()
        {
            _dataProvider = new SqlDataProvider();
        }

       public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<Contacts> GetContacts()
        {

            var list = _dataProvider.GetContacts();

            return list;
        }

    }
}