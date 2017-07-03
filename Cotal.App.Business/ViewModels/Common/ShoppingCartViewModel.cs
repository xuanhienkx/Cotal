﻿using System;
using Cotal.App.Business.ViewModels.Product;

namespace Cotal.App.Business.ViewModels.Common
{
  [Serializable]
  public class ShoppingCartViewModel
  {
    public int ProductId { set; get; }
    public ProductViewModel Product { set; get; }
    public int Quantity { set; get; }
  }
}