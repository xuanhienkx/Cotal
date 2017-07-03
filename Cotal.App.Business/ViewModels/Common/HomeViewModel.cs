using System.Collections.Generic;
using Cotal.App.Business.ViewModels.Product;

namespace Cotal.App.Business.ViewModels.Common
{
  public class HomeViewModel
  {
    public IEnumerable<SlideViewModel> Slides { set; get; }
    public IEnumerable<ProductViewModel> LastestProducts { set; get; }
    public IEnumerable<ProductViewModel> TopSaleProducts { set; get; }

    public string Title { set; get; }
    public string MetaKeyword { set; get; }
    public string MetaDescription { set; get; }
  }
}