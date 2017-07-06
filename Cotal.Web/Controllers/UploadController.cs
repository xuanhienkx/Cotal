using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cotal.Web.Controllers
{
  [Route("api/[controller]")]
  public class UploadController : AdminControllerBase<UploadController>
  {
    private IHostingEnvironment _environment;

    public UploadController(ILoggerFactory loggerFactory, IHostingEnvironment environment) : base(loggerFactory)
    {
      _environment = environment;
    }
    [HttpPost("SaveImage")]
    public async Task<IActionResult> SaveImage(ICollection<IFormFile> files, string type = "")
    {
      Dictionary<string, object> dict = new Dictionary<string, object>();
      try
      {
        var uploads = "";
        int flag = 1;
        foreach (var file in files)
        {
          if (file.Length > 0)
          {
            int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB

            IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
            var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            var extension = ext.ToLower();
            if (!AllowedFileExtensions.Contains(extension))
            {
              var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
              dict.Add("error", message);
              return BadRequest(dict);
            }
            string directory = string.Empty;
            switch (type)
            {
              case "avatar":
                directory = "UploadedFiles/Avatars/";
                break;
              case "product":
                directory = "UploadedFiles/Products/";
                break;
              case "news":
                directory = "UploadedFiles/News/";
                break;
              case "banner":
                directory = "UploadedFiles/Banners/";
                break;
              default:
                directory = "UploadedFiles/Orther/";
                break;
            }
            uploads = Path.Combine(_environment.WebRootPath, directory);
            if (!Directory.Exists(uploads))
            {
              Directory.CreateDirectory(uploads);
            }
            var fileSave = directory + file.FileName;
            var pathSave = Path.Combine(_environment.WebRootPath, uploads, file.FileName);
            using (var fileStream = new FileStream(pathSave, FileMode.Create))
            {
              await file.CopyToAsync(fileStream);
                                                             
              dict.Add($"imageUrl_{flag}", $"/{fileSave}");
             
            }
          }
        }
        var messageUpload = string.Format("Image Updated Successfully.");
        dict.Add($"status", $"Ok");
        dict.Add($"messageUpload", $"{messageUpload}");
        return Ok(dict);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);

      }

    }
  }
}
