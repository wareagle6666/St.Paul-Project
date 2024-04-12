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
        List<NotificationsModel> GetAllNotifications();
        int SaveNotification(string Description, string Title, string Subtitle, string UserId, string Token);
        int DeleteUserAccount(string UserId);
        List<ConfSlot> GetConfSlotbyPriest(string Priest);
        List<ConfSlot> GetAllConfSlot();
        int InserConfSlots(ConfSlot slot);
        int DeleteConfSlot(int ID);
        int UpdateConfSlot(int ID);
        int insertConfBooking(ConfBooking booking);
        ConfSlot GetConfSlotbyID(int ID);
        Users GetUserByUsername(string Username);
    }
}
