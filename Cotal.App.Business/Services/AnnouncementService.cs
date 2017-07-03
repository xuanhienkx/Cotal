using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cotal.App.Business.ViewModels.Common;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Repositories;
using Cotal.Core.InfacBase.Uow;

namespace Cotal.App.Business.Services
{
  public interface IAnnouncementService
  {
    AnnouncementViewModel Create(AnnouncementViewModel announcement, int userId);

    IEnumerable<AnnouncementViewModel> GetListByUserId(int userId, int pageIndex, int pageSize, out int totalRow);

    IEnumerable<AnnouncementViewModel> GetListByUserId(int userId, int top);

    void Delete(int notificationId);

    void MarkAsRead(int userId, int notificationId);

    AnnouncementViewModel GetDetail(int id);

    IEnumerable<AnnouncementViewModel> GetListAll(int pageIndex, int pageSize, out int totalRow);

    IEnumerable<AnnouncementViewModel> ListAllUnread(int userId, int pageIndex, int pageSize, out int totalRow);

    void Save();
  }

  public class AnnouncementService : ServiceBace<Announcement, int>, IAnnouncementService
  {
    private readonly IRepository<AnnouncementUser, int> _announcementUserRepository;
    private readonly IMapper _mapper;

    public AnnouncementService(IUowProvider uowProvider, IMapper mapper) : base(uowProvider)
    {
      _mapper = mapper;
      _announcementUserRepository = UnitOfWork.GetRepository<AnnouncementUser, int>();
    }

    public AnnouncementViewModel Create(AnnouncementViewModel announcement, int userId)
    {
      var newAnnoun = new Announcement
      {
        Content = announcement.Content,
        Status = announcement.Status,
        Title = announcement.Title,
        CreatedDate = DateTime.Now,
        UserId = userId
      };
      foreach (var user in announcement.AnnouncementUsers)
        newAnnoun.AnnouncementUsers.Add(new AnnouncementUser
        {
          UserId = user.UserId,
          HasRead = false
        });
      var db = Repository.Add(newAnnoun);
      Save();
      return _mapper.Map<Announcement, AnnouncementViewModel>(db);
    }

    public IEnumerable<AnnouncementViewModel> GetListByUserId(int userId, int pageIndex, int pageSize, out int totalRow)
    {
      totalRow = Repository.Count(a => a.UserId == userId);
      var list = Repository.QueryPage(pageIndex, pageSize, a => a.UserId == userId);
      return _mapper.Map<IEnumerable<Announcement>, IEnumerable<AnnouncementViewModel>>(list);
    }

    public IEnumerable<AnnouncementViewModel> GetListByUserId(int userId, int top)
    {
      var list = Repository.Query(x => x.UserId == userId).Take(top);
      return _mapper.Map<IEnumerable<Announcement>, IEnumerable<AnnouncementViewModel>>(list);
    }

    public void Delete(int notificationId)
    {
      Repository.Remove(notificationId);
      Save();
    }

    public void MarkAsRead(int userId, int notificationId)
    {
      var announs = _announcementUserRepository.Query(x => x.UserId == userId && x.AnnouncementId == notificationId)
        .FirstOrDefault();
      if (announs == null)
        _announcementUserRepository.Add(new AnnouncementUser
        {
          AnnouncementId = notificationId,
          UserId = userId,
          HasRead = true
        });
      else
        announs.HasRead = true;
      Save();
    }

    public AnnouncementViewModel GetDetail(int id)
    {
      var db = Repository.Get(id);
      return _mapper.Map<Announcement, AnnouncementViewModel>(db);
    }

    public IEnumerable<AnnouncementViewModel> GetListAll(int pageIndex, int pageSize, out int totalRow)
    {
      totalRow = Repository.Count();
      var list = Repository.QueryPage(pageIndex, pageSize, null, q => q.OrderBy(x => x.CreatedDate));
      return _mapper.Map<IEnumerable<Announcement>, IEnumerable<AnnouncementViewModel>>(list);
    }

    public IEnumerable<AnnouncementViewModel> ListAllUnread(int userId, int pageIndex, int pageSize, out int totalRow)
    {
      totalRow = Repository.Count(a => a.UserId == userId);
      var list = Repository.QueryPage(pageIndex, pageSize, a => a.UserId == userId, q => q.OrderBy(x => x.CreatedDate));
      return _mapper.Map<IEnumerable<Announcement>, IEnumerable<AnnouncementViewModel>>(list);
    }

    public void Save()
    {
      UnitOfWork.SaveChanges();
    }
  }
}