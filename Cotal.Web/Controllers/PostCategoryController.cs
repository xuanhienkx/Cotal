using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cotal.App.Business.Services;
using Cotal.App.Business.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cotal.Web.Controllers
{
  [Route("api/[controller]")]
  public class PostCategoryController : AdminControllerBase<PostCategoryController>
  {
    private readonly IPostCategoryService _categoryService;
    public PostCategoryController(ILoggerFactory loggerFactory, IPostCategoryService categoryService) : base(loggerFactory)
    {
      _categoryService = categoryService;
    }
    // GET: api/values
    [HttpGet("GetAll")]
    public IActionResult GetAll(string filter ="")
    {
      var list = _categoryService.GetAll(filter);
      return Ok(list);
    }
    [HttpGet("GetAllHierachy")]
    public IActionResult GetAllHierachy(int? selectedParent = null)
    {
      var list = _categoryService.GetAllByParentId(selectedParent);
      return Ok(list);
    }
    // GET api/values/5
    [HttpGet("Detail/{id}")]
    public IActionResult Detail(int id)
    {
      var mode = _categoryService.GetById(id);
      return Ok(mode);
    }

    // POST api/values
    [HttpPost("Created")]
    public IActionResult Created([FromBody]PostCategoryViewModel model)
    {
      model.CreatedBy = CurrentUser.UserName;
      model.CreatedDate = DateTime.Now;
      var db =_categoryService.Add(model);
      return Ok(db);                       
    }

    // PUT api/values/5
    [HttpPut("Update")]
    public IActionResult Update([FromBody]PostCategoryViewModel model)
    {
      model.UpdatedBy = CurrentUser.UserName;
      model.UpdatedDate = DateTime.Now;
      _categoryService.Update(model);
      return Ok(JsonConvert.SerializeObject("Cập nhật thành công"));
    }

    // DELETE api/values/5
    [HttpDelete("Delete/{id}")]
    public void Delete(int id)
    {
      _categoryService.Delete(id);
    }


  }
}
