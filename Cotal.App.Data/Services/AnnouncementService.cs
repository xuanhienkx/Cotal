using System.Collections.Generic;
using Cotal.App.Model.Models;

namespace Cotal.App.Data.Services
{
    public interface IAnnouncementService
    {
        Announcement Create(Announcement announcement);

        List<Announcement> GetListByUserId(string userId, int pageIndex, int pageSize, out int totalRow);

        List<Announcement> GetListByUserId(string userId, int top);

        void Delete(int notificationId);

        void MarkAsRead(string userId, int notificationId);

        Announcement GetDetail(int id);

        List<Announcement> GetListAll(int pageIndex, int pageSize, out int totalRow);

        List<Announcement> ListAllUnread(string userId, int pageIndex, int pageSize, out int totalRow);

        void Save();
    }
    public class AnnouncementService
    {
        
    }
}