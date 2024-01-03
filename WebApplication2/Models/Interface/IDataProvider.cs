using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models.Interface
{
    public interface IDataProvider
    {
        List<Users> GetAllUsers();
        List<Mobile> GetAllMobileTokens();
         int CreateMobileRecord(string UserId, string Token, string DeviceType);
         int UpdateLastLogin(string Token);
         string GetUserIdByUserEmail(string Email);
         List<UserRoles> GetUserRolesByUserId(string UserId);
    }
}
