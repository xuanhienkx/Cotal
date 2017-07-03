using System;
using Cotal.App.Business.Services;
using Cotal.App.Business.ViewModels.Common;
using Cotal.Core.InfacBase.Paging;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cotal.Web.Controllers
{
  [Route("api/[controller]")]
  public class AnnouncementController : AdminControllerBase
  {
    private readonly IAnnouncementService _announcementService;

    public AnnouncementController(IAnnouncementService announcementService)
    {
      _announcementService = announcementService;
    }

    [HttpGet("GetTopMyAnnouncement")]
    public IActionResult GetTopMyAnnouncement()
    {
      var totalRow = 0;
      var model = _announcementService.ListAllUnread(CurrentUser.Id, 1, 10, out totalRow);
      var pagedSet = new PaginationSet<AnnouncementViewModel>
      {
        PageIndex = 1,
        TotalRows = totalRow,
        PageSize = 10,
        Items = model
      };
      return Ok(pagedSet);
    }

    [HttpGet("MarkAsRead")]
    public IActionResult MarkAsRead(int announId)
    {
      try
      {
        _announcementService.MarkAsRead(CurrentUser.Id, announId);
        return Ok(announId);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}
