using System.ComponentModel.DataAnnotations.Schema;
using Cotal.Core.InfacBase.Entities;

namespace Cotal.App.Model.Models
{
  [Table("AnnouncementUsers")]
  public class AnnouncementUser : EntityBase<int>
  {
    [Column(Order = 1)]
    public int AnnouncementId { get; set; }

    [Column(Order = 2)]
    public int UserId { get; set; }

    public bool HasRead { get; set; }
    /*[ForeignKey("UserId")]
      public IUser AppUser { get; set; }*/

    [ForeignKey("AnnouncementId")]
    public virtual Announcement Announcement { get; set; }
  }
}