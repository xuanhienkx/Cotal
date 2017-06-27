using System;
using System.ComponentModel.DataAnnotations;
using Cotal.Core.InfacBase.Entities;

namespace Cotal.App.Model.Abstract
{
    public abstract class Auditable : EntityBase<int>, IAuditable
    {
        public DateTime? CreatedDate { set; get; }

        [MaxLength(256)]
        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        [MaxLength(256)]
        public string UpdatedBy { set; get; }

        [MaxLength(256)]
        public string MetaKeyword { set; get; }

        [MaxLength(256)]
        public string MetaDescription { set; get; }

        public bool Status { set; get; }
    }
}