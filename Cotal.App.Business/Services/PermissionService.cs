using System.Collections.Generic;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Uow;

namespace Cotal.App.Business.Services
{
  public interface IPermissionService
  {
    IEnumerable<Permission> GetByFunctionId(string functionId);
    IEnumerable<Permission> GetByRoleIds(List<int> roleIds);
    void Add(Permission permission);
    void DeleteAll(string functionId);
    void Save();
  }
  public class PermissionService : ServiceBace<Permission, int>, IPermissionService
  {
    public PermissionService(IUowProvider uowProvider) : base(uowProvider)
    {
    }

    public IEnumerable<Permission> GetByFunctionId(string functionId)
    {
      return Repository.Query(x => x.FunctionId == functionId);
    }

    public IEnumerable<Permission> GetByRoleIds(List<int> roleIds)
    {
      return Repository.Query(x => roleIds.Contains(x.RoleId));
    }

    public void Add(Permission permission)
    {
      Repository.Add(permission);
      Save();
    }

    public void DeleteAll(string functionId)
    {
      Repository.RemoveMulti(x => x.FunctionId == functionId);
    }

    public void Save()
    {
      UnitOfWork.SaveChanges();
    }
  }
}