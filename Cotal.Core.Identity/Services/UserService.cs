using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Cotal.Core.Identity.Data;
using Cotal.Core.Identity.Models;
using Cotal.Core.Identity.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;

namespace Cotal.Core.Identity.Services
{
    public interface IUserService
    {
        Task<AppUser> GetUserByUsername(string username);
        Task<IdentityResult> CreateUser(AppUser user, string password);
        PasswordVerificationResult VerifyHashedPassword(AppUser user, string password);
        Task<IList<Claim>> GetClaims(AppUser user);
        Task<bool> Login(LoginViewModel model);
    }
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public UserService(ApplicationDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> hasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = hasher;
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
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

        public async Task<bool> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
            return result.Succeeded;
        }
    }
}