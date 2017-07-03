using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cotal.Core.InfacBase.Entities;

namespace Cotal.App.Model.Models
{
  [Table("ProductTags")]
  public class ProductTag : EntityBase<int>
  {
    [Column(Order = 1)]
    public int ProductId { set; get; }

    [Column(Order = 2)]
    [MaxLength(50)]
    public string TagId { set; get; }

    [ForeignKey("ProductId")]
    public virtual Product Product { set; get; }

    [ForeignKey("TagId")]
    public virtual Tag Tag { set; get; }
  }
}