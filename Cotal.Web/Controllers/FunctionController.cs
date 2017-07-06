using System.Collections.Generic;
using System.Linq;
using Cotal.App.Business.Services;
using Cotal.App.Business.ViewModels.System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cotal.Web.Controllers
{
  [Route("api/[controller]")]
  public class FunctionController : AdminControllerBase<FunctionController>
  {
    private readonly IFunctionService _functionService;

    public FunctionController(IFunctionService functionService, ILoggerFactory loggerFactory) : base(loggerFactory)
    {
      _functionService = functionService;
    }


    [HttpGet("GetAllHierachy")]
    public IActionResult GetAllHierachy()
    {
      IEnumerable<FunctionViewModel> model;
      model = CurrentRoleNames.Contains("Administrator")
        ? _functionService.GetAll(string.Empty)
        : _functionService.GetAllWithPermission(CurrentRoleIds);

      var parents = model.Where(x => x.Parent == null);
      var functionViewModels = parents as IList<FunctionViewModel> ?? parents.ToList();
      foreach (var parent in functionViewModels)
        parent.ChildFunctions = model.Where(x => x.ParentId == parent.Id).ToList();

      return Ok(functionViewModels);
    }
    [HttpGet("GetAll")]
    public IActionResult GetAll(string filter = "")
    {
      var model = _functionService.GetAll(filter);
      return Ok(model);
    }
    [HttpGet("Detail/{id}")]
    public IActionResult Detail(string id)
    {
      var function = _functionService.Get(id);
      return Ok(function);
    }
    [HttpPost("Create")]
    public IActionResult Create(FunctionViewModel model)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);
      var function = _functionService.Create(model);
      return Ok(function);
    }
    [HttpPut("Update")]
    public IActionResult Update(FunctionViewModel model)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);
      var function = _functionService.Create(model);
      return Ok(function);
    }
    [HttpDelete("Delete")]
    public IActionResult Delete(string id)
    {
      _functionService.Delete(id);
      return Ok();
    }

  }
}
