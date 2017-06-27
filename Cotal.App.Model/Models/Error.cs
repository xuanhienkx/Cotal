using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cotal.Core.InfacBase.Entities;

namespace Cotal.App.Model.Models
{
    [Table("Errors")]
    public class Error : EntityBase<int>
    {                                     

        public string Message { set; get; }

        public string StackTrace { set; get; }

        public DateTime CreatedDate { set; get; }
    }
}