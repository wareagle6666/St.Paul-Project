using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models.Interface;
using WebApplication2.Models;

namespace WebApplication2.Controllers.API
{
    public class UsersController : ApiController
    {
        private readonly IDataProvider _dataProvider;
        public UsersController()
        {

        }
        public UsersController(IDataProvider dataProvider)
        {
            if (dataProvider == null)
                throw new ArgumentNullException("dataProvider");

            _dataProvider = dataProvider;
        }
       
        public IEnumerable<Users> Get()
        {
            return _dataProvider.GetAllUsers();
        }
    }
}
