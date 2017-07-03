using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cotal.App.Business.ViewModels.System;
using Cotal.App.Model.Models;
using Cotal.Core.Identity.Models;
using Cotal.Core.Identity.Services;

namespace Cotal.App.Business.Services
{
  public interface ILoginService
  {
    bool IsLogin(string userName, string password);
    AppUserViewModel CurrenrUser(string userName);
  }

  public class LoginService : ILoginService
  {
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IPermissionService _permission;

    public LoginService(IUserService userService, IPermissionService permission, IMapper mapper)
    {
      _userService = userService;
      _permission = permission;
      _mapper = mapper;
    }

    public bool IsLogin(string userName, string password)
    {
      return _userService.Login(userName, password);
    }

    public AppUserViewModel CurrenrUser(string userName)
    {
      var user = _userService.GetUserByUsername(userName).Result;
      var roles = _userService.GetRolsByUser(user.Id);
      var roleIds = roles.Select(x => x.Id).ToList();
      var permistion = _permission.GetByRoleIds(roleIds ?? new List<int>());
      var permistionView =
        _mapper.Map<IEnumerable<Permission>, IEnumerable<PermissionViewModel>>(permistion ?? new List<Permission>());
      var userView = _mapper.Map<AppUser, AppUserViewModel>(user);
      userView.Roles = _mapper.Map<IEnumerable<AppRole>, IEnumerable<AppRoleViewModel>>(roles ?? new List<AppRole>());
      userView.Permissions = permistionView;
      return userView;
    }
  }
}