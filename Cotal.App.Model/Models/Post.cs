using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cotal.App.Model.Abstract;

namespace Cotal.App.Model.Models
{
  [Table("Posts")]
  public class Post : Auditable
  {
    [Required]
    [MaxLength(256)]
    public string Name { set; get; }

    [Required]
    [MaxLength(256)]
    [Column(TypeName = "varchar(256)")]
    public string Alias { set; get; }

    [Required]
    public int CategoryId { set; get; }

    [MaxLength(256)]
    public string Image { set; get; }

    [MaxLength(500)]
    public string Description { set; get; }

    public string Content { set; get; }

    public bool? HomeFlag { set; get; }
    public bool? HotFlag { set; get; }
    public int? ViewCount { set; get; }

    [ForeignKey("CategoryId")]
    public virtual PostCategory PostCategory { set; get; }

    public virtual IEnumerable<PostTag> PostTags { set; get; }
  }
}