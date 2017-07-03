using System.Collections.Generic;

namespace Cotal.App.Business.ViewModels.System
{
    public class ApplicationGroupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<AppRoleViewModel> Roles { set; get; }
    }
}