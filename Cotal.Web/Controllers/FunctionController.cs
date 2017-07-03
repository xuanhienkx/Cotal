using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cotal.App.Business.Services;
using Cotal.App.Business.ViewModels.System;   
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cotal.Web.Controllers
{
  [Route("api/[controller]")]
  public class FunctionController : AdminControllerBase
  {
    private readonly IFunctionService _functionService;

    public FunctionController(IFunctionService functionService)
    {
      _functionService = functionService;
    }
    [HttpGet("GetAllHierachy")]
    public IActionResult GetAllHierachy()
    {
      IEnumerable<FunctionViewModel> model;
      model = CurrentRoleNames.Contains("Administrator") ? _functionService.GetAll(string.Empty) : _functionService.GetAllWithPermission(CurrentRoleIds);

      var parents = model.Where(x => x.Parent == null);
      var functionViewModels = parents as IList<FunctionViewModel> ?? parents.ToList();
      foreach (var parent in functionViewModels)
      {
        parent.ChildFunctions = model.Where(x => x.ParentId == parent.Id).ToList();
      }

      return Ok(functionViewModels);
    }
    // GET: api/values
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public string Get(string id)
    {
      return "value";
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
