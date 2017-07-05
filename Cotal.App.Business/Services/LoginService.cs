using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    Task<AppUserViewModel> CurrenrUser(string userName);
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

    public async Task<AppUserViewModel> CurrenrUser(string userName)
    {
      var user = await _userService.GetUserByUsername(userName);
      var roles = await _userService.GetRolsByUser(user.Id);
      var roleIds = roles.Select(x => x.Id).ToList();
      var permistion = _permission.GetByRoleIds(roleIds ?? new List<int>());
      var permistionView =
        _mapper.Map<IEnumerable<Permission>, IEnumerable<PermissionViewModel>>(permistion ?? new List<Permission>());
      var userView = _mapper.Map<AppUser, AppUserViewModel>(user);
      userView.Roles = roles.Select(x => x.Name).ToList();
      userView.RoleIds = roles.Select(x => x.Id).ToList();
      userView.Permissions = permistionView;
      return userView;
    }
  }
}