using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cotal.Core.Identity.Data;
using Cotal.Core.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cotal.Core.Identity.Services
{
  public interface IUserService   
  {
    Task<AppUser> GetUserByUsername(string username);
    Task<IdentityResult> CreateUser(AppUser user, string password);
    PasswordVerificationResult VerifyHashedPassword(AppUser user, string password);
    Task<IList<Claim>> GetClaims(AppUser user);
    bool Login(string userName, string password);
    IEnumerable<AppRole> GetRolsByUser(int useId);
    List<int> GetRolesIdByUser(int useId);
    List<string> GetRolesNameByUser(int useId);
    List<string> GetRolesNameByUser(string userName);
  }
  public class UserService : IUserService
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly IPasswordHasher<AppUser> _passwordHasher;
    private readonly RoleManager<AppRole> _roleManager;

    public UserService(ApplicationDbContext context, UserManager<AppUser> userManager, IPasswordHasher<AppUser> hasher, RoleManager<AppRole> roleManager)
    {
      _userManager = userManager;
      _passwordHasher = hasher;
      _roleManager = roleManager;
    }

    public async Task<AppUser> GetUserByUsername(string username)
    {
      return await _userManager.FindByNameAsync(username);
    }
    public IEnumerable<AppRole> GetRolsByUser(int useId)
    {
      return _roleManager.Roles.Where(x => x.Users.Any(u => u.UserId == useId)).ToList();
    }

    public List<int> GetRolesIdByUser(int useId)
    {
      var roles = GetRolsByUser(useId);
      return roles.Select(x => x.Id).ToList();
    }

    public List<string> GetRolesNameByUser(int useId)
    {
      var roles = GetRolsByUser(useId);
      return roles.Select(x => x.Name).ToList();
    }

    public List<string> GetRolesNameByUser(string userName)
    {
      var user = GetUserByUsername(userName).Result;
      var roles = _roleManager.Roles.Where(x => x.Users.Any(u => u.UserId == user.Id));
      return roles.Select(x => x.Name).ToList();
    }

    public async Task<IdentityResult> CreateUser(AppUser user, string password)
    {
      return await _userManager.CreateAsync(user, password);
    }

    public PasswordVerificationResult VerifyHashedPassword(AppUser user, string password)
    {
      return _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
    }

    public async Task<IList<Claim>> GetClaims(AppUser user)
    {
      return await _userManager.GetClaimsAsync(user);
    }

    public bool Login(string userName, string password)
    {
      var result = GetUserByUsername(userName).Result;
      if (result == null) return false;
      var p = VerifyHashedPassword(result, password);
      return p == PasswordVerificationResult.Success;
    }
  }
}