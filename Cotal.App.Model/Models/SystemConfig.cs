using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cotal.Core.InfacBase.Entities;

namespace Cotal.App.Model.Models
{
    [Table("SystemConfigs")]
    public class SystemConfig : EntityBase <int>
    {    

        [Required]                    
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Code { set; get; }

        [MaxLength(50)]
        public string ValueString { set; get; }

        public int? ValueInt { set; get; }
    }
}