using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cotal.App.Business.Infrastructure.Extensions;
using Cotal.App.Business.ViewModels.System;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Uow;
using Microsoft.EntityFrameworkCore;

namespace Cotal.App.Business.Services
{
  public interface IFunctionService
  {
    FunctionViewModel Create(FunctionViewModel function);

    IEnumerable<FunctionViewModel> GetAll(string filter);

    IEnumerable<FunctionViewModel> GetAllWithPermission(List<int> roleIds);

    IEnumerable<FunctionViewModel> GetAllWithParentId(string parentId);

    FunctionViewModel Get(string id);

    void Update(FunctionViewModel function);

    void Delete(string id);

    int Save();
    bool CheckExistedId(string id);
  }
  public class FunctionService : ServiceBace<Function, string>, IFunctionService
  {
    private IMapper _mapper;
    public FunctionService(IUowProvider uowProvider, IMapper mapper) : base(uowProvider)
    {
      _mapper = mapper;
    }
    public FunctionViewModel Create(FunctionViewModel function)
    {
      var model = new Function();
      model.UpdateFunction(function);
      model = Repository.Add(model);
      Save();
      return _mapper.Map<Function, FunctionViewModel>(model);
    }

    public IEnumerable<FunctionViewModel> GetAll(string filter)
    {
      var functions = Repository.Query(x => x.Status && (string.IsNullOrEmpty(filter) || x.Name.Contains(filter)));
      return _mapper.Map<IEnumerable<Function>, IEnumerable<FunctionViewModel>>(functions);
    }

    public IEnumerable<FunctionViewModel> GetAllWithPermission(List<int> roleIds)
    {
      var qr = (from f in DB.Functions
                join p in DB.Permissions on f.Id equals p.FunctionId
                where roleIds.Contains(p.RoleId) && (p.CanRead == true)
                select f);
      var parentIds = qr.Select(x => x.ParentId).Distinct();
      qr = qr.Union(Repository.Query(x => parentIds.Contains(x.Id)));                                       
      return _mapper.Map<IEnumerable<Function>, IEnumerable<FunctionViewModel>>(qr.AsEnumerable());
    }

    public IEnumerable<FunctionViewModel> GetAllWithParentId(string parentId)
    {
      var functions = Repository.Query(f => f.ParentId == parentId, o => o.OrderBy(x => x.ParentId));
      return _mapper.Map<IEnumerable<Function>, IEnumerable<FunctionViewModel>>(functions);
    }

    public FunctionViewModel Get(string id)
    {
      var model = Repository.Get(id);
      return _mapper.Map<Function, FunctionViewModel>(model);
    }

    public void Update(FunctionViewModel function)
    {
      var model = Repository.Get(function.Id);
      model.UpdateFunction(function);
      Repository.Update(model);
    }

    public void Delete(string id)
    {
      Repository.Remove(id);
    }

    public int Save()
    {
      return UnitOfWork.SaveChanges();
    }

    public bool CheckExistedId(string id)
    {
      return Repository.Any(x => x.Id == id);
    }
  }
}