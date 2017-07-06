using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    Task<AppUser> GetUserByUserId(int id);
    PasswordVerificationResult VerifyHashedPassword(AppUser user, string password);
    Task<IList<Claim>> GetClaims(AppUser user);
    bool Login(string userName, string password);
    Task<IEnumerable<AppUser>> GetAllAsync();
    IEnumerable<AppUser> GetAll(int page, int pageSize, out int totalRow, string filter = null);
    Task<bool> Create(AppUser model, string password, List<string> roles);
    Task<bool> Update(AppUser model, List<string> roles);
    Task<bool> Delete(int id);
    Task<IEnumerable<AppRole>> GetRolsByUser(int useId);
    Task<IEnumerable<AppRole>> GetAllRole();
    Task<IEnumerable<AppRole>> GetAllRole(Expression<Func<AppRole, bool>> expression);
    IEnumerable<AppRole> GetAllRole(int page, int pageSize, out int totalRow, string filter = null);
    Task<AppRole> GetRole(int id);
    Task<AppRole> GetRole(string name);
    Task<List<int>> GetRolesIdByUser(int useId);
    Task<List<string>> GetRolesNameByUser(int useId);
    Task<List<string>> GetRolesNameByUser(string userName);
    Task<bool> CreateRole(AppRole model);
    Task<bool> UpdateRole(AppRole model);
    Task<bool> DeleteRole(int id);
  }
  public class UserService : IUserService
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly IPasswordHasher<AppUser> _passwordHasher;
    private readonly RoleManager<AppRole> _roleManager;

    public UserService(UserManager<AppUser> userManager, IPasswordHasher<AppUser> hasher, RoleManager<AppRole> roleManager, ApplicationDbContext context)
    {
      _userManager = userManager;
      _passwordHasher = hasher;
      _roleManager = roleManager;
    }

    #region User

    public async Task<AppUser> GetUserByUsername(string username)
    {
      return await _userManager.FindByNameAsync(username);
    }

    public async Task<AppUser> GetUserByUserId(int id)
    {
      return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
      var users = await _userManager.Users.ToListAsync();
      return users;
    }

    public IEnumerable<AppUser> GetAll(int page, int pageSize, out int totalRow, string filter = null)
    {
      var users = _userManager.Users.Where(x => string.IsNullOrEmpty(filter) || x.UserName.Contains(filter)|| x.Email.Contains(filter));
      totalRow = users.Count();
      return users.OrderBy(x => x.UserName).Skip((page - 1) * pageSize).Take(pageSize);
    }

    public async Task<bool> Create(AppUser model, string password, List<string> roles)
    {
      var result = await _userManager.CreateAsync(model, password);
      if (result.Succeeded)
      {
        var newUser = await _userManager.FindByNameAsync(model.UserName);
        await _userManager.AddToRolesAsync(newUser, roles ?? new List<string>());
      }

      return result.Succeeded;
    }

    public async Task<bool> Update(AppUser model, List<string> roles)
    {
      var result = await _userManager.UpdateAsync(model);
      if (result.Succeeded)
      {
        var newUser = await _userManager.FindByNameAsync(model.UserName);
        var userRoles = await _userManager.GetRolesAsync(newUser);
        await _userManager.RemoveFromRolesAsync(newUser, userRoles.ToArray());
        await _userManager.AddToRolesAsync(newUser, roles ?? new List<string>());
        return result.Succeeded;
      }
      else
      {
        return false;
      }

    }

    public async Task<bool> Delete(int id)
    {
      var user = await GetUserByUserId(id);
      var result = await _userManager.DeleteAsync(user);
      return result.Succeeded;
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

    #endregion

    #region Roles

    public async Task<IEnumerable<AppRole>> GetRolsByUser(int useId)
    {
      return await _roleManager.Roles.Where(x => x.Users.Any(u => u.UserId == useId)).ToListAsync();
    }

    public async Task<IEnumerable<AppRole>> GetAllRole()
    {
      return await _roleManager.Roles.ToListAsync();
    }

    public async Task<IEnumerable<AppRole>> GetAllRole(Expression<Func<AppRole, bool>> expression)
    {
      return await _roleManager.Roles.Where(expression).ToListAsync();
    }

    public IEnumerable<AppRole> GetAllRole(int page, int pageSize, out int totalRow, string filter = null)
    {
      var roles = _roleManager.Roles.Where(x => string.IsNullOrEmpty(filter) || x.Description.Contains(filter)|| x.Name.Contains(filter));
      totalRow = roles.Count();
      return roles.OrderBy(x => x.Name).Skip((page - 1) * pageSize).Take(pageSize);
    }

    public async Task<AppRole> GetRole(int id)
    {
      var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
      return role;
    }

    public async Task<AppRole> GetRole(string name)
    {
      var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == name);
      return role;
    }

    public async Task<List<int>> GetRolesIdByUser(int useId)
    {
      var roles = await GetRolsByUser(useId);
      return roles.Select(x => x.Id).ToList();
    }

    public async Task<List<string>> GetRolesNameByUser(int useId)
    {
      var roles = await GetRolsByUser(useId);
      return roles.Select(x => x.Name).ToList();
    }

    public async Task<List<string>> GetRolesNameByUser(string userName)
    {
      var user = await GetUserByUsername(userName);
      var roles = _roleManager.Roles.Where(x => x.Users.Any(u => u.UserId == user.Id));
      return roles.Select(x => x.Name).ToList();
    }

    public async Task<bool> CreateRole(AppRole model)
    {
      var result = await _roleManager.CreateAsync(model);
      return result.Succeeded;
    }

    public async Task<bool> UpdateRole(AppRole model)
    {
      var result = await _roleManager.UpdateAsync(model);
      return result.Succeeded;
    }

    public async Task<bool> DeleteRole(int id)
    {
      var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
      var result = await _roleManager.DeleteAsync(role);
      return result.Succeeded;
    }

    #endregion

  }
}