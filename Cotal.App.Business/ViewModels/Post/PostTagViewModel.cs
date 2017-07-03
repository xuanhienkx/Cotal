using Cotal.App.Business.ViewModels.Common;

namespace Cotal.App.Business.ViewModels.Post
{
    public class PostTagViewModel
    {
        public int PostID { set; get; }

        public string TagID { set; get; }

        public virtual PostViewModel Post { set; get; }

        public virtual TagViewModel Tag { set; get; }
    }
}