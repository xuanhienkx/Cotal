using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cotal.Core.Domain.Interfaces;
using Cotal.Core.InfacBase.Entities;

namespace Cotal.App.Model.Models
{
    [Table("Announcements")]
    public class Announcement : EntityBase<int>
    {
        public Announcement()
        {
            AnnouncementUsers = new List<AnnouncementUser>();
        }      

        [StringLength(250)]
        [Required]
        public string Title { set; get; }

        public string Content { set; get; }

        public DateTime CreatedDate { get; set; }   
        public int UserId { set; get; }    
        public string UserName { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<AnnouncementUser> AnnouncementUsers { get; set; }

    }
}
