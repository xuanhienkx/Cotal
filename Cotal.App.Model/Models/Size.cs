using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cotal.Core.InfacBase.Entities;

namespace Cotal.App.Model.Models
{
    [Table("Sizes")]
    public class Size : EntityBase<int>
    {

        [StringLength(250)]
        public string Name { get; set; }
    }
}