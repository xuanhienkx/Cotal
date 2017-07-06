using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Cotal.App.Business.Infrastructure.Extensions;
using Cotal.App.Business.ViewModels.System;
using Cotal.Core.Identity.Models;
using Cotal.Core.Identity.Services;

namespace Cotal.App.Business.Services
{
  public interface IAppRoleService
  {
    Task<IEnumerable<AppRoleViewModel>> GetAll();
    Task<IEnumerable<AppRoleViewModel>> GetAll(Expression<Func<AppRole, bool>> expression);
    IEnumerable<AppRoleViewModel> GetAll(int page, int pageSize, out int totalRow, string filter = null);
    Task<AppRoleViewModel> Get(int id);
    Task<AppRoleViewModel> Get(string name);
    Task<AppRoleViewModel> CreateRole(AppRoleViewModel model);
    Task<bool> UpdateRole(AppRoleViewModel model);
    Task<bool> DeleteRole(int id);
  }
  public class AppRoleService : IAppRoleService
  {
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AppRoleService(IUserService userService, IMapper mapper)
    {
      _userService = userService;
      _mapper = mapper;
    }

    public async Task<IEnumerable<AppRoleViewModel>> GetAll()
    {
      var list = await _userService.GetAllRole();
      return _mapper.Map<IEnumerable<AppRole>, IEnumerable<AppRoleViewModel>>(list ?? new List<AppRole>());
    }

    public async Task<IEnumerable<AppRoleViewModel>> GetAll(Expression<Func<AppRole, bool>> expression)
    {
      var list = await _userService.GetAllRole(expression);
      return _mapper.Map<IEnumerable<AppRole>, IEnumerable<AppRoleViewModel>>(list ?? new List<AppRole>());
    }

    public IEnumerable<AppRoleViewModel> GetAll(int page, int pageSize, out int totalRow, string filter = null)
    {
      var list = _userService.GetAllRole(page, pageSize, out totalRow, filter);
      return _mapper.Map<IEnumerable<AppRole>, IEnumerable<AppRoleViewModel>>(list ?? new List<AppRole>());
    }

    public async Task<AppRoleViewModel> Get(int id)
    {
      var role = await _userService.GetRole(id);
      return _mapper.Map<AppRole, AppRoleViewModel>(role ?? new AppRole());
    }

    public async Task<AppRoleViewModel> Get(string name)
    {
      var role = await _userService.GetRole(name);
      return _mapper.Map<AppRole, AppRoleViewModel>(role ?? new AppRole());
    }

    public async Task<AppRoleViewModel> CreateRole(AppRoleViewModel model)
    {
      var role = new AppRole();
      role.UpdateApplicationRole(model);
      try
      {
        var created = await _userService.CreateRole(role);
        if (created)
        {
          return await Get(model.Name);
        }
        else
        {
          throw new Exception("Have error when create role");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return new AppRoleViewModel();
      }
    }

    public async Task<bool> UpdateRole(AppRoleViewModel model)
    {
      try
      {
        var role = await _userService.GetRole(model.Id);
      role.UpdateApplicationRole(model);
      var updated = await _userService.UpdateRole(role);
      if (updated)
      {
        return true;
      }
        else
        {
          throw new Exception("Have error when create role");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return false;
      }
    }

    public async Task<bool> DeleteRole(int id)
    {
      return await _userService.DeleteRole(id);
    }
  }
}