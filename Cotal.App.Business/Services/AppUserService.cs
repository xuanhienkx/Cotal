using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cotal.App.Business.Infrastructure.Extensions;
using Cotal.App.Business.ViewModels.System;
using Cotal.App.Model.Models;
using Cotal.Core.Identity.Models;
using Cotal.Core.Identity.Services;

namespace Cotal.App.Business.Services
{
  public interface IAppUserService
  {
    Task<AppUserViewModel> Get(string userName);
    Task<AppUserViewModel> Get(int id);
    Task<IEnumerable<AppUserViewModel>> GetAll();
    IEnumerable<AppUserViewModel> GetAll(int page, int pageSize, out int totalRow, string filter = null);
    Task<AppUserViewModel> Create(AppUserViewModel model);
    Task<AppUserViewModel> Update(AppUserViewModel model);
    Task Delete(int id);
  }
  public class AppUserService : IAppUserService
  {
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public AppUserService(IUserService userService, IMapper mapper)
    {
      _userService = userService;
      _mapper = mapper;
    }

    public async Task<AppUserViewModel> Get(string userName)
    {
      var user = await _userService.GetUserByUsername(userName);
      var roles = await _userService.GetRolesNameByUser(userName);
      var userView = _mapper.Map<AppUser, AppUserViewModel>(user);
      userView.Roles = roles;
      return userView;
    }

    public async Task<AppUserViewModel> Get(int id)
    {
      var user = await _userService.GetUserByUserId(id);
      var roles = await _userService.GetRolesNameByUser(user.Id);
      var userView = _mapper.Map<AppUser, AppUserViewModel>(user);
      userView.Roles = roles;
      return userView;
    }

    public async Task<IEnumerable<AppUserViewModel>> GetAll()
    {
      var list =await _userService.GetAllAsync();
      return _mapper.Map<IEnumerable<AppUser>, IEnumerable<AppUserViewModel>>(list);
    }

    public IEnumerable<AppUserViewModel> GetAll(int page, int pageSize, out int totalRow, string filter = null)
    {
      var list = _userService.GetAll(page, pageSize, out totalRow, filter);
      return _mapper.Map<IEnumerable<AppUser>, IEnumerable<AppUserViewModel>>(list);
    }

    public async Task<AppUserViewModel> Create(AppUserViewModel model)
    {
      var newAppUser = new AppUser();
      newAppUser.UpdateUser(model);
      var result = await _userService.Create(newAppUser, model.Password, model.Roles);
      if (result)
      {
        return await Get(model.UserName);
      }
      else
      {
        throw new Exception("Not crate");
      }
    }

    public async Task<AppUserViewModel> Update(AppUserViewModel model)
    {
      var appUser = await _userService.GetUserByUserId(model.Id);
      appUser.UpdateUser(model);
      var result = await _userService.Update(appUser, model.Roles);
      if (!result)
      {
        throw new Exception("Not Update");
      }
      return await Get(model.Id);
    }

    public async Task Delete(int id)
    {
      await _userService.Delete(id);
    }
  }
}