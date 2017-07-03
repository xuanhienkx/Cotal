using Cotal.App.Business.ViewModels.Common;

namespace Cotal.App.Business.ViewModels.Product
{
    public class ProductTagViewModel
    {
        public int ProductID { set; get; }

        public string TagID { set; get; }

        public virtual ProductViewModel Post { set; get; }

        public virtual TagViewModel Tag { set; get; }
    }
}