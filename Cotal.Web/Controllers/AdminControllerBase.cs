using System.Collections.Generic;
using System.Linq;
using Cotal.App.Business.Constants;
using Cotal.App.Business.ViewModels.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cotal.Web.Controllers
{
  public abstract class ControllerBase<T> : Controller //where T : Controller
  {
    protected ILogger Logger;
    protected ControllerBase(ILoggerFactory loggerFactory)
    {
      Logger = loggerFactory.CreateLogger<T>();
    }

  }
  [Authorize]
  public abstract class AdminControllerBase<T> : ControllerBase<T> //where T : Controller
  {
    public AdminControllerBase(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }
    protected AppUserViewModel CurrentUser
    {
      get
      {
        var userClaims = HttpContext.User.Claims.First(c => c.Type == Constants.CURRENT_USER).Value;
        return JsonConvert.DeserializeObject<AppUserViewModel>(userClaims);
        //=> userService.GetUserByUsername(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value).Result;
      }
    }

    protected List<string> CurrentRoleNames => CurrentUser.Roles;

    protected List<int> CurrentRoleIds => CurrentUser.RoleIds;
  }
}
