using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cotal.App.Business.Services;
using Cotal.App.Business.ViewModels.DataContracts;
using Cotal.App.Business.ViewModels.System;
using Cotal.Core.InfacBase.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cotal.Web.Controllers
{
  [Route("api/[controller]")]
  public class RoleController : AdminControllerBase<RoleController>
  {
    private readonly IAppRoleService _appRoleService;
    private readonly IPermissionService _permissionService;
    private readonly IFunctionService _functionService;
    public RoleController(ILoggerFactory loggerFactory, IAppRoleService appRoleService, IPermissionService permissionService, IFunctionService functionService) : base(loggerFactory)
    {
      _appRoleService = appRoleService;
      _permissionService = permissionService;
      _functionService = functionService;
    }
    // GET: 
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
      var list = await _appRoleService.GetAll();
      return Ok(list);
    }
    // GET: api/values
    [HttpGet("GetListPaging")]
    public IActionResult GetListPaging(int page, int pageSize, string filter = null)
    {
      int totalRow = 0;
      var list = _appRoleService.GetAll(page, pageSize, out totalRow, filter);
      var pagedSet = new PaginationSet<AppRoleViewModel>
      {
        PageIndex = page,
        PageSize = pageSize,
        TotalRows = totalRow,
        Items = list
      };
      return Ok(pagedSet);
    }
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
      var role = await _appRoleService.Get(id);
      return Ok(role);
    }
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody]AppRoleViewModel model)
    {
      if (ModelState.IsValid)
      {
        var role = await _appRoleService.CreateRole(model);
        return Ok(role);
      }
      else
      {
        return BadRequest(ModelState);
      }
    }
    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody]AppRoleViewModel model)
    {
      if (ModelState.IsValid)
      {
        var role = await _appRoleService.UpdateRole(model);
        return Ok(role);
      }
      else
      {
        return BadRequest(ModelState);
      }
    }
    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
      var result = await _appRoleService.DeleteRole(id);
      return Ok(result);
    }
    [HttpGet("GetAllPermission")]
    public async Task<IActionResult> GetAllPermission(string functionId)
    {
      List<PermissionViewModel> listPermission = new List<PermissionViewModel>();
      var list = await _permissionService.GetByFunctionId(functionId);   
      var vList = list as IList<PermissionViewModel> ?? list.ToList();
      if (!vList.Any())
      {
        var roles = await _appRoleService.GetAll(x => x.Name != "Administrator");
        listPermission.AddRange(roles.Select(x => new PermissionViewModel()
        {
          RoleId = x.Id,
          CanCreate = false,
          CanDelete = false,
          CanRead = false,
          CanUpdate = false,
          AppRole = x
        }));
      }
      else
      {
        listPermission = vList.ToList();
      }
      return Ok(listPermission);
    }
    [HttpPost("SavePermission")]
    public IActionResult SavePermission([FromBody]SavePermissionRequest data)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      try
      {
        _permissionService.DeleteAll(data.FunctionId);
        foreach (var item in data.Permissions)
        {
          item.FunctionId = data.FunctionId;
          _permissionService.Add(item);
        }
        var functions = _functionService.GetAllWithParentId(data.FunctionId);
        var functionViewModels = functions as FunctionViewModel[] ?? functions.ToArray();
        if (functionViewModels.Any())
        {
          foreach (var item in functionViewModels)
          {
            _permissionService.DeleteAll(item.Id);
            foreach (var p in data.Permissions)
            {
              var childPermission = new PermissionViewModel()
              {
                FunctionId = item.Id,
                RoleId = p.RoleId,
                CanRead = p.CanRead,
                CanCreate = p.CanCreate,
                CanDelete = p.CanDelete,
                CanUpdate = p.CanUpdate
              };
              _permissionService.Add(childPermission);
            }
          }
        }
        _permissionService.Save();
        return Ok(JsonConvert.SerializeObject("Lưu quyền thành công"));
      }
      catch (Exception e)
      {
        Logger.LogError(e.Message);
        return BadRequest(JsonConvert.SerializeObject(e.Message));
      }
    }


  }
}
