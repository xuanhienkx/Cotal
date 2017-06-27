using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Cotal.Core.InfacBase.Entities;
namespace Cotal.App.Model.Models
{
    [Table("PostTags")]
    public class PostTag : EntityBase<int>
    {         
        [Column(Order = 1)]
        public int PostId { set; get; }
               
        [Column(TypeName = "varchar(50)",Order =2)]
        [MaxLength(50)]                     
        public string TagId { set; get; }

        [ForeignKey("PostId")]
        public virtual Post Post { set; get; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { set; get; }
    }
}