﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cotal.App.Model.Models
{
  [Table("OrderDetails")]
  public class OrderDetail
  {
    [Key]
    [Column(Order = 1)]
    public int OrderId { set; get; }

    [Key]
    [Column(Order = 2)]
    public int ProductId { set; get; }

    public int Quantity { set; get; }

    public decimal Price { set; get; }

    public int ColorId { get; set; }

    public int SizeId { get; set; }

    [ForeignKey("OrderId")]
    public virtual Order Order { set; get; }

    [ForeignKey("ProductId")]
    public virtual Product Product { set; get; }

    [ForeignKey("ColorId")]
    public virtual Color Color { set; get; }

    [ForeignKey("SizeId")]
    public virtual Size Size { set; get; }
  }
}