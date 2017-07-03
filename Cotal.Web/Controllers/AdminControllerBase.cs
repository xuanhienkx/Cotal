using System.Collections.Generic;
using System.Linq;
using Cotal.App.Business.Constants;
using Cotal.App.Business.ViewModels.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cotal.Web.Controllers
{
  [Authorize]
  public abstract class AdminControllerBase : Controller
  {
    protected AppUserViewModel CurrentUser
    {
      get
      {
        var userClaims = HttpContext.User.Claims.First(c => c.Type == Constants.CURRENT_USER).Value;
        return JsonConvert.DeserializeObject<AppUserViewModel>(userClaims);
        //=> userService.GetUserByUsername(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value).Result;
      }
    }

    protected List<string> CurrentRoleNames
    {
      get { return CurrentUser.Roles.Select(x => x.Name).ToList(); }
    }

    protected List<int> CurrentRoleIds
    {
      get { return CurrentUser.Roles.Select(x => x.Id).ToList(); }
    }
  }
}
