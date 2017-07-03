using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cotal.App.Model.Abstract;

namespace Cotal.App.Model.Models
{
  [Table("Pages")]
  public class Page : Auditable
  {
    [Required]
    [MaxLength(256)]
    public string Name { set; get; }

    [MaxLength(256)]
    [Required]
    [Column(TypeName = "varchar(256)")]
    public string Alias { set; get; }

    public string Content { set; get; }
  }
}