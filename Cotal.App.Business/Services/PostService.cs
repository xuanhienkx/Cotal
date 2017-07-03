using System.Collections.Generic;
using System.Linq;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Uow;
using Microsoft.EntityFrameworkCore;

namespace Cotal.App.Business.Services
{
  public interface IPostService
  {
    Post Add(Post post);

    void Update(Post post);

    void Delete(int id);

    IEnumerable<Post> GetAll();

    IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow);

    IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow);

    Post GetById(int id);

    IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

    void Save();
  }

  public class PostService : ServiceBace<Post, int>, IPostService
  {
    public PostService(IUowProvider uowProvider) : base(uowProvider)
    {
    }

    public Post Add(Post post)
    {
      return Repository.Add(post);
    }

    public void Update(Post post)
    {
      Repository.Update(post);
      Save();
    }

    public void Delete(int id)
    {
      Repository.Remove(id);
      Save();
    }

    public IEnumerable<Post> GetAll()
    {
      return Repository.GetAll(x => x.OrderByDescending(p => p.CreatedDate),
        posts => posts.Include(p => p.PostCategory));
    }

    public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow)
    {
      totalRow = Repository.Count();
      return Repository.QueryPage(page, pageSize, post => post.Id != 0, null,
        posts => posts.Include(p => p.PostCategory)).ToList();
    }

    public IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
    {
      totalRow = Repository.Count();
      return Repository.QueryPage(page, pageSize, post => post.CategoryId == categoryId, null,
        posts => posts.Include(p => p.PostCategory)).ToList();
    }

    public Post GetById(int id)
    {
      return Repository.Get(id);
    }

    public IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
    {
      totalRow = Repository.Count();
      return Repository.QueryPage(page, pageSize, post => post.PostTags.Any(v => v.TagId == tag)).ToList();
    }

    public void Save()
    {
      UnitOfWork.SaveChanges();
    }
  }
}