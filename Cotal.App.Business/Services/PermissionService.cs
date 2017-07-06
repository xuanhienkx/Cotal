using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cotal.App.Business.Infrastructure.Extensions;
using Cotal.App.Business.ViewModels.System;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Uow;
using Microsoft.EntityFrameworkCore;

namespace Cotal.App.Business.Services
{
  public interface IPermissionService
  {
    Task<IEnumerable<PermissionViewModel>> GetByFunctionId(string functionId);
    IEnumerable<PermissionViewModel> GetByRoleIds(List<int> roleIds);
    void Add(PermissionViewModel permission);
    void DeleteAll(string functionId);
    void Save();
  }

  public class PermissionService : ServiceBace<Permission, int>, IPermissionService
  {
    private readonly IMapper _mapper;
    private readonly IAppRoleService _roleService;
    public PermissionService(IUowProvider uowProvider, IMapper mapper, IAppRoleService roleService ) : base(uowProvider)
    {
      _mapper = mapper;
      _roleService = roleService;
    }

    public async Task<IEnumerable<PermissionViewModel>> GetByFunctionId(string functionId)
    {      
      var list = Repository.Query(x => x.FunctionId == functionId);
      var vList=_mapper.Map<IEnumerable<Permission>, IEnumerable<PermissionViewModel>>(list);
      foreach (var p in vList)
      {
        if (p.AppRole==null)
        {
          p.AppRole = await _roleService.Get(p.RoleId);
        }
      }
      return vList;
    }

    public IEnumerable<PermissionViewModel> GetByRoleIds(List<int> roleIds)
    {
      var list = Repository.Query(x => roleIds.Contains(x.RoleId));
      var vList = _mapper.Map<IEnumerable<Permission>, IEnumerable<PermissionViewModel>>(list);
      return vList;
    }

    public void Add(PermissionViewModel permission)
    {
      var db = new Permission();
      db.UpdatePermission(permission);
      db.FunctionId = permission.FunctionId;
      Repository.Add(db);
    }

    public void DeleteAll(string functionId)
    {
      Repository.RemoveMulti(x => x.FunctionId == functionId);
      UnitOfWork.SaveChanges();
    }

    public void Save()
    {
      UnitOfWork.SaveChanges();
    }
  }
}