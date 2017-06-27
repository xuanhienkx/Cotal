using System;
using System.Linq;
using System.Threading.Tasks;
using Cotal.Core.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Cotal.Core.Identity.Data
{
    public interface IDbInitializer
    {
        void Initialize();
    }

    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public DbInitializer(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void Initialize()
        {
            //create database schema if none exists
            _context.Database.EnsureCreated();

            //If there is already an Administrator role, abort
            if (_context.Roles.Any(r => r.Name == "Administrator")) return;
            //Create the Administartor Role
            await _roleManager.CreateAsync(new AppRole("Administrator","Quản trị viên"));
            await _roleManager.CreateAsync(new AppRole("Member", "Người dùng"));
            var user = new AppUser()
            {
                UserName = "admin",
                Email = "phapht@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Nguyen xuan phap",
                Avatar = "/assets/images/img.jpg",
                Gender = true,
                Status = true
            };
            string password = "Phap@3658!$";
            await _userManager.CreateAsync(user, password);
            var adminUser = _userManager.FindByNameAsync("admin").Result;
            await _userManager.AddToRoleAsync(adminUser, "Administrator");
        }
    }
}