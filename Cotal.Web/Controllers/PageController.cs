using Cotal.App.Business.Services;
using Cotal.App.Business.ViewModels.Post;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Paging;
using Cotal.Core.InfacBase.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cotal.Web.Controllers
{
  [Route("api/[controller]")]
  public class PageController : AdminControllerBase<PageController>
  {
    private readonly IDataPager<Page, int> _dataPager;
    private readonly IPageService _pageService;
    public PageController(ILoggerFactory loggerFactory, IDataPager<Page, int> dataPager, IPageService pageService) : base(loggerFactory)
    {
      _dataPager = dataPager;
      _pageService = pageService;
    }
    // GET: api/values
    [HttpGet("GetAll")]
    public IActionResult GetAll(int page, int pageSize = 20, string keyword = "")
    {
      var result = _dataPager.Query(page, pageSize,
        new Filter<Page>(
          x => (string.IsNullOrEmpty(keyword) || x.Name.Contains(keyword) || x.Content.Contains(keyword))));
      return Ok(result);
    }

    // GET api/values/5
    [HttpGet("Detail/{id}")]
    public IActionResult Get(int id)
    {
      var p = _pageService.Get(id);
      return Ok(p);
    }
    // GET api/values/GetByAlias
    [HttpGet("GetByAlias")]
    public IActionResult GetByAlias(string alias)
    {         
      var p = _pageService.GetByAlias(alias);   
      return Ok(p);
    }

    // POST api/values
    [HttpPost]
    public IActionResult Post([FromBody]PageViewModel model)
    {
      var db = _pageService.Create(model);
      return Ok(db);
    }

    // PUT api/values/5
    [HttpPut]
    public IActionResult Put([FromBody]PageViewModel model)
    {
        _pageService.Update(model);
      return Ok(JsonConvert.SerializeObject("Update thanh cong"));
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      _pageService.Delete(id);
      return Ok(JsonConvert.SerializeObject("xoa thanh cong"));
    }


  }
}
