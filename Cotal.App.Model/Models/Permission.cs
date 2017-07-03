using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cotal.Core.InfacBase.Entities;

namespace Cotal.App.Model.Models
{
  [Table("Permissions")]
  public class Permission : EntityBase<int>
  {
    public int RoleId { get; set; }

    [StringLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string FunctionId { get; set; }

    public bool CanCreate { set; get; }

    public bool CanRead { set; get; }

    public bool CanUpdate { set; get; }

    public bool CanDelete { set; get; }

    /*[ForeignKey("RoleId")]
      public TRole AppRole { get; set; }*/

    [ForeignKey("FunctionId")]
    public Function Function { get; set; }
  }
}