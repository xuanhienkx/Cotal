using System;
using System.Linq;
using System.Net;
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
  public class UserController : AdminControllerBase<UserController>
  {
    private readonly IAppUserService _userService;

    public UserController(ILoggerFactory loggerFactory, IAppUserService appUserService) : base(loggerFactory)
    {
      _userService = appUserService;
    }

    [HttpGet("GetListPaging")]
    public IActionResult GetListPaging(int page, int pageSize, string filter = null)
    {
      try
      {
        var totalRow = 0;
        var model = _userService.GetAll(page, pageSize, out totalRow, filter);
        var pagedSet = new PaginationSet<AppUserViewModel>
        {
          PageIndex = page,
          PageSize = pageSize,
          TotalRows = totalRow,
          Items = model
        };
        return Ok(pagedSet);
      }
      catch (Exception e)
      {
        Logger.LogError(e.Message);
        return BadRequest(e.Message);
      }
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
      if (id <= 0)
        return BadRequest(nameof(id) + " không có giá trị.");
      var user = await _userService.Get(id);
      if (user == null)
        return BadRequest("Không có dữ liệu");
      return Ok(user);
    }

    [HttpPost("Created")]
    public async Task<IActionResult> Created([FromBody]AppUserViewModel model)
    {
      if (ModelState.IsValid)
      {
        var db = await _userService.Create(model);
        return Ok(db);
      }
      return BadRequest(ModelState);
    }
    [HttpPut("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody]AppUserViewModel model)
    {
      if (ModelState.IsValid)
        try
        {
          var db = await _userService.Update(model);
          return Ok(db);
        }
        catch (Exception e)
        {
          Logger.LogError(e.Message);
          return BadRequest(e.Message);
        }
      return BadRequest(ModelState);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await _userService.Delete(id);
        return Ok(id);
      }
      catch (Exception e)
      {
        Logger.LogError(e.Message);
        return BadRequest(e.Message);
      }
    }
  }
}
