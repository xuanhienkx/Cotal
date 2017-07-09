using System;
using System.ComponentModel.DataAnnotations;

namespace Cotal.App.Business.ViewModels.Post
{
  public class PageViewModel
  {
    public int Id { set; get; }
    public string Name { set; get; }
    public string Alias { set; get; }
    public string Content { set; get; }
    public bool Status { set; get; }
    public DateTime? CreatedDate { set; get; }     
    public string CreatedBy { set; get; }   
    public DateTime? UpdatedDate { set; get; }   
    public string UpdatedBy { set; get; }    
    public string MetaKeyword { set; get; } 
    public string MetaDescription { set; get; }
  }
}