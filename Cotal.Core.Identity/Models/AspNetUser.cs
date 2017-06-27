using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cotal.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Cotal.Core.Identity.Models
{
    public class CotalUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;
        public CotalUser(UserManager<AppUser> userManager, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _userManager = userManager;
        }

        public int Id
        {
            get
            {
                int id = Convert.ToInt32(_userManager.GetUserId(_accessor.HttpContext.User));
                return id;
            }
        }
        public string Name => _accessor.HttpContext.User.Identity.Name;

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return  _accessor.HttpContext.User.Claims;
        }
    }

 /*   public class CotalRole : IRole
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly RoleManager<AppRole> _userManager;

        public CotalRole(IHttpContextAccessor accessor, RoleManager<AppRole> userManager)
        {
            _accessor = accessor;
            _userManager = userManager;
        }

        public int Id { get; }
        public string Name { get; }
    }*/
}
