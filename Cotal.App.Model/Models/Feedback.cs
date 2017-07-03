using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cotal.Core.InfacBase.Entities;

namespace Cotal.App.Model.Models
{
  [Table("Feedbacks")]
  public class Feedback : EntityBase<int>
  {
    [StringLength(250)]
    [Required]
    public string Name { set; get; }

    [StringLength(250)]
    public string Email { set; get; }

    [StringLength(500)]
    public string Message { set; get; }

    public DateTime CreatedDate { set; get; }

    public bool Status { set; get; }
  }
}