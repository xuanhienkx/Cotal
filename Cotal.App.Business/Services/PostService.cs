using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Cotal.App.Business.Infrastructure.Extensions;
using Cotal.App.Business.ViewModels.Post;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Uow;
using Microsoft.EntityFrameworkCore;

namespace Cotal.App.Business.Services
{
  public interface IPostService
  {
    PostViewModel Add(PostViewModel post);

    void Update(PostViewModel post);

    void Delete(int id);

    IEnumerable<PostViewModel> GetAll();
    IEnumerable<PostViewModel> GetAll(int page, int pageSize, out int totalRow, Expression<Func<Post, bool>> fiter = null);

    IEnumerable<PostViewModel> GetAllPaging(int page, int pageSize, out int totalRow);

    IEnumerable<PostViewModel> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow);

    PostViewModel GetById(int id);

    IEnumerable<PostViewModel> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

    void Save();
  }

  public class PostService : ServiceBace<Post, int>, IPostService
  {
    private readonly IMapper _mapper;
    public PostService(IUowProvider uowProvider, IMapper mapper) : base(uowProvider)
    {
      _mapper = mapper;
    }

    public PostViewModel Add(PostViewModel model)
    {
      var post = new Post();
      post.UpdatePost(model);
      var db = Repository.Add(post);
      Save();
      return _mapper.Map<Post, PostViewModel>(db);
    }

    public void Update(PostViewModel model)
    {
      var post = Repository.Get(model.Id);
      post.UpdatePost(model);
      Repository.Update(post);
      Save();
    }

    public void Delete(int id)
    {
      Repository.Remove(id);
      Save();
    }

    public IEnumerable<PostViewModel> GetAll()
    {
      var list = Repository.GetAll(x => x.OrderByDescending(p => p.CreatedDate),
        posts => posts.Include(p => p.PostCategory));
      return _mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(list);
    }

    public IEnumerable<PostViewModel> GetAll(int page, int pageSize, out int totalRow, Expression<Func<Post, bool>> fiter = null)
    {
      totalRow = Repository.Count(fiter);
      var list = Repository.QueryPage(page, pageSize, fiter, x=>x.OrderByDescending(p=>p.CreatedDate).ThenBy(xs=>xs.Name));
      return _mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(list);
    }

    public IEnumerable<PostViewModel> GetAllPaging(int page, int pageSize, out int totalRow)
    {
      totalRow = Repository.Count();
      var list = Repository.QueryPage(page, pageSize, post => post.Id != 0, null,
        posts => posts.Include(p => p.PostCategory)).ToList();
      return _mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(list);
    }

    public IEnumerable<PostViewModel> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
    {
      totalRow = Repository.Count(post => post.CategoryId == categoryId);
      var list = Repository.QueryPage(page, pageSize, post => post.CategoryId == categoryId, null,
        posts => posts.Include(p => p.PostCategory)).ToList();
      return _mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(list);
    }

    public PostViewModel GetById(int id)
    {
      var db = Repository.Get(id);
      return _mapper.Map<Post, PostViewModel>(db);
    }

    public IEnumerable<PostViewModel> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
    {
      totalRow = Repository.Count();
      var list = Repository.QueryPage(page, pageSize, post => post.PostTags.Any(v => v.TagId == tag)).ToList();
      return _mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(list);
    }

    public void Save()
    {
      UnitOfWork.SaveChanges();
    }
  }
}