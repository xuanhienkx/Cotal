using System.Linq;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Uow;

namespace Cotal.App.Business.Services
{
  public interface IPageService
  {
    Page GetByAlias(string alias);
    Page Create(Page page);
    void Update(Page page);
    void Save();
  }
  public class PageService : ServiceBace<Page, int>, IPageService
  {
    public PageService(IUowProvider uowProvider) : base(uowProvider)
    {
    }

    public Page GetByAlias(string alias)
    {
      return Enumerable.FirstOrDefault<Page>(Repository.Query(x => x.Alias == alias));
    }

    public Page Create(Page page)
    {
      return Repository.Add(page);
    }

    public void Update(Page page)
    {
      Repository.Update(page);
      Save();
    }

    public void Save()
    {
      UnitOfWork.SaveChanges();
    }
  }
}