using System.Collections.Generic;
using System.Linq;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Repositories;
using Cotal.Core.InfacBase.Uow;

namespace Cotal.App.Business.Services
{
  public interface IAnnouncementService
  {
    Announcement Create(Announcement announcement);

    List<Announcement> GetListByUserId(int userId, int pageIndex, int pageSize, out int totalRow);

    List<Announcement> GetListByUserId(int userId, int top);

    void Delete(int notificationId);

    void MarkAsRead(int userId, int notificationId);

    Announcement GetDetail(int id);

    List<Announcement> GetListAll(int pageIndex, int pageSize, out int totalRow);

    List<Announcement> ListAllUnread(int userId, int pageIndex, int pageSize, out int totalRow);

    void Save();
  }
  public class AnnouncementService : ServiceBace<Announcement, int>, IAnnouncementService
  {
    private readonly IRepository<AnnouncementUser, int> _announcementUserRepository;
    public AnnouncementService(IUowProvider uowProvider) : base(uowProvider)
    {
      _announcementUserRepository = UnitOfWork.GetRepository<AnnouncementUser, int>();
    }

    public Announcement Create(Announcement announcement)
    {
      return Repository.Add(announcement);
    }

    public List<Announcement> GetListByUserId(int userId, int pageIndex, int pageSize, out int totalRow)
    {
      totalRow = Repository.Count(a => a.UserId == userId);
      return Repository.QueryPage(pageIndex, pageSize, a => a.UserId == userId).ToList();
    }

    public List<Announcement> GetListByUserId(int userId, int top)
    {
      return Repository.Query(x => x.UserId == userId).Take(top).ToList();
    }

    public void Delete(int notificationId)
    {
      Repository.Remove(notificationId);
      Save();
    }

    public void MarkAsRead(int userId, int notificationId)
    {
      var announs = _announcementUserRepository.Query(x => x.UserId == userId && x.AnnouncementId == notificationId).FirstOrDefault();
      if (announs == null)
      {
        _announcementUserRepository.Add(new AnnouncementUser()
        {
          AnnouncementId = notificationId,
          UserId = userId,
          HasRead = true,
        });
      }
      else
      {
        announs.HasRead = true;
      }
      Save();

    }

    public Announcement GetDetail(int id)
    {
      return Repository.Get(id);
    }

    public List<Announcement> GetListAll(int pageIndex, int pageSize, out int totalRow)
    {
      totalRow = Repository.Count();
      return Repository.QueryPage(pageIndex, pageSize, null, q => q.OrderBy(x => x.CreatedDate)).ToList();
    }

    public List<Announcement> ListAllUnread(int userId, int pageIndex, int pageSize, out int totalRow)
    {
      totalRow = Repository.Count(a => a.UserId == userId);
      return Repository.QueryPage(pageIndex, pageSize, a => a.UserId == userId, q => q.OrderBy(x => x.CreatedDate)).ToList();
    }

    public void Save()
    {
      UnitOfWork.SaveChanges();
    }
  }
}