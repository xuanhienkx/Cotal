using System.Collections.Generic;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Uow;

namespace Cotal.App.Business.Services
{
  public interface IPostCategoryService
  {
    PostCategory Add(PostCategory postCategory);

    void Update(PostCategory postCategory);

    void Delete(int id);

    IEnumerable<PostCategory> GetAll();

    IEnumerable<PostCategory> GetAllByParentId(int parentId);

    PostCategory GetById(int id);

    void Save();
  }

  public class PostCategoryService : ServiceBace<PostCategory, int>, IPostCategoryService
  {
    public PostCategoryService(IUowProvider uowProvider) : base(uowProvider)
    {
    }

    public PostCategory Add(PostCategory postCategory)
    {
      return Repository.Add(postCategory);
    }

    public void Update(PostCategory postCategory)
    {
      Repository.Update(postCategory);
    }

    public void Delete(int id)
    {
      Repository.Remove(id);
    }

    public IEnumerable<PostCategory> GetAll()
    {
      return Repository.GetAll();
    }

    public IEnumerable<PostCategory> GetAllByParentId(int parentId)
    {
      return Repository.Query(x => x.ParentID == parentId);
    }

    public PostCategory GetById(int id)
    {
      return Repository.Get(id);
    }

    public void Save()
    {
      UnitOfWork.SaveChanges();
    }
  }
}