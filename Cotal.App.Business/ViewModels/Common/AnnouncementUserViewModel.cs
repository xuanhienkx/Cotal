using Cotal.App.Business.ViewModels.System;

namespace Cotal.App.Business.ViewModels.Common
{
    public class AnnouncementUserViewModel
    {
        public int AnnouncementId { get; set; }

        public string UserId { get; set; }

        public bool HasRead { get; set; }

        public virtual AppUserViewModel AppUser { get; set; }

        public virtual AnnouncementViewModel Announcement { get; set; }
    }
}