using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cotal.Core.InfacBase.Entities;

namespace Cotal.App.Model.Models
{
  [Table("VisitorStatistics")]
  public class VisitorStatistic : EntityBase<int>
  {
    [Required]
    public DateTime VisitedDate { set; get; }

    [MaxLength(50)]
    public string IpAddress { set; get; }
  }
}