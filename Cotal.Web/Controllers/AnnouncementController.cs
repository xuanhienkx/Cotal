using System;
using System.Collections.Generic;   
using AutoMapper;
using Cotal.App.Business.Services;
using Cotal.App.Business.ViewModels.Common;
using Cotal.App.Model.Models;
using Cotal.Core.Identity.Services;
using Cotal.Core.InfacBase.Paging;        
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cotal.Web.Controllers
{
  [Route("api/[controller]")]  
  public class AnnouncementController : AdminControllerBase
  {
    private IAnnouncementService _announcementService;

    public AnnouncementController( IAnnouncementService announcementService)  
    {
      _announcementService = announcementService;
    }
    [HttpGet("GetTopMyAnnouncement")]
    public IActionResult GetTopMyAnnouncement()
    {
      int totalRow = 0;
      List<Announcement> model = _announcementService.ListAllUnread(CurrentUser.Id, 1, 10, out totalRow);
      IEnumerable<AnnouncementViewModel> modelVm = Mapper.Map<List<Announcement>, List<AnnouncementViewModel>>(model);
      PaginationSet<AnnouncementViewModel> pagedSet = new PaginationSet<AnnouncementViewModel>()
      {
        PageIndex = 1,
        TotalRows = totalRow,
        PageSize = 10,
        Items = modelVm
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
