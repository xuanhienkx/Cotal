using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cotal.App.Business.Services;
using Cotal.App.Business.ViewModels.System;
using Cotal.Core.InfacBase.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cotal.Web.Controllers
{
  [Route("api/[controller]")]
  public class RoleController : AdminControllerBase<RoleController>
  {
    private readonly IAppRoleService _appRoleService;
    public RoleController(ILoggerFactory loggerFactory, IAppRoleService appRoleService) : base(loggerFactory)
    {
      this._appRoleService = appRoleService;
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
      var result= await _appRoleService.DeleteRole(id);
      return Ok(result);
    }
  }
}
