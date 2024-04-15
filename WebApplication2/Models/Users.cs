using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication2.Models.Interface;

namespace WebApplication2.Models
{
    public class Users
    {

        private SqlDataProvider _dataProvider;

        public Users()
        {
            _dataProvider = new SqlDataProvider();
        }


        public List<Users> getusers()
        {

            var list = _dataProvider.GetAllUsers();
            return list;
        }

        public async Task<int> UserCreationUpdate (string userId, string FirstName, string LastName, string PhoneNumber)
        {
            var result = _dataProvider.CreateUserProfile( userId,  FirstName,  LastName,  PhoneNumber);
            return result;
        }
        public Users GetUserByUsername(string Username)
        {

            var list = _dataProvider.GetUserByUsername(Username);
            return list;
        }
        public string Id { get; set; }
        public Guid userID { get; set; }
        public Guid eventID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }

        public string Hometown { get; set; }
    }
}