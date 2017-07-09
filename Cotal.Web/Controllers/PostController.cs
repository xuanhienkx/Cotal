using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cotal.App.Business.Services;
using Cotal.App.Business.ViewModels.Post;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Paging;
using Cotal.Core.InfacBase.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cotal.Web.Controllers
{
  [Route("api/[controller]")]
  public class PostController : AdminControllerBase<PostController>
  {
    private readonly IPostService _postService;
    private IDataPager<Post, int> _pager;
    public PostController(ILoggerFactory loggerFactory, IPostService postService, IDataPager<Post, int> pager) : base(loggerFactory)
    {
      _postService = postService;
      _pager = pager;
    }
    [HttpGet("GetAll")]
    public IActionResult GetAll(int? categoryId, string keyword, int page, int pageSize = 20)
    {

      var result = _pager.Query(page, pageSize,
        new Filter<Post>(x => (categoryId == null || x.CategoryId == categoryId)
                  && (string.IsNullOrEmpty(keyword) || x.Name.Contains(keyword) || x.Content.Contains(keyword))),
               new OrderBy<Post>(p => p.OrderByDescending(o => o.CreatedDate)), posts => posts.Include(c => c.PostCategory));
      return Ok(result);
       
    }
    // GET api/values/5
    [HttpGet("Detail/{id}")]
    public IActionResult Get(int id)
    {
      var p = _postService.GetById(id);
      return Ok(p);
    }
    [HttpPost("Created")]
    public IActionResult Created([FromBody] PostViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      model.CreatedBy = CurrentUser.UserName;
      model.CreatedDate = DateTime.Now;
      var db = _postService.Add(model);
      return Ok(db);
    }
    [HttpPut("Update")]
    public IActionResult Update([FromBody] PostViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      model.UpdatedBy = CurrentUser.UserName;
      model.UpdatedDate = DateTime.Now;
      _postService.Update(model);
      return Ok(JsonConvert.SerializeObject("Cap nhat thanh cong!.."));
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult Delete(int id)
    {
      _postService.Delete(id);
      return Ok(JsonConvert.SerializeObject("Xoa ban tin thanh cong!.."));
    }
  }
}
