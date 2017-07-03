using System.Collections.Generic;
using Cotal.App.Business.ViewModels.System;

namespace Cotal.App.Business.ViewModels.DataContracts
{
    public class SavePermissionRequest
    {
        public string FunctionId { set; get; }

        public IList<PermissionViewModel> Permissions { get; set; }
    }
}