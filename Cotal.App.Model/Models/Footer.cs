using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cotal.Core.InfacBase.Entities;

namespace Cotal.App.Model.Models
{
    [Table("Footers")]
    public class Footer : EntityBase<string>
    {
        [Key]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public override string Id { set; get; }
        [Required]
        public string Content { set; get; }
    }
}