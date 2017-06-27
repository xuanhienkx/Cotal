using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cotal.Core.InfacBase.Entities;

namespace Cotal.App.Model.Models
{
    [Table("Colors")]
    public class Color : EntityBase<int>
    {

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Code { get; set; }
    }
}