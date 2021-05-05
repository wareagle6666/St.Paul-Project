using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace WebApplication2.Models
{
    public class Address
    {
        private SqlDataProvider _dataProvider;

        public Address()
        {
            _dataProvider = new SqlDataProvider();
        }

        public string ID { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

        public Address GetAddress(string ID)
        {
            var Address = _dataProvider.GetAddress(ID);

            return Address;
        }
        public Address GetAddressByID(string ID)
        {
            var Address = _dataProvider.GetAddressById(ID);

            return Address;
        }
        public int CreateAddress(string UserID, string Street1, string Street2, string City, string State, string Zipcode)
        {
            var result = _dataProvider.CreateAddress(UserID, Street1, Street2,  City,  State,  Zipcode);

            return result;
        }

        public int UpdateAddress(string AddressID, string Street1, string Street2, string City, string State, string Zipcode)
        {
            var result = _dataProvider.UpdateAddress(AddressID, Street1, Street2, City, State, Zipcode);

            return result;
        }
    }
}