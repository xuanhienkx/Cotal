using System.Collections.Generic;

namespace Cotal.App.Business.ViewModels.System
{
    public class AppUserViewModel
    {
        public int Id { set; get; }
        public string FullName { set; get; }
        public string BirthDay { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string UserName { set; get; }
        public string Address { get; set; }
        public string PhoneNumber { set; get; }
        public string Avatar { get; set; }
        public bool Status { get; set; }

        public string Gender { get; set; }

        public IEnumerable<AppRoleViewModel> Roles { get; set; }
        public IEnumerable<PermissionViewModel> Permissions { get; set; }
    }
}