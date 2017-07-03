using System.Collections.Generic;

namespace Cotal.Core.InfacBase.Paging
{
  public class PaginationSet<T>
  {
    public int PageIndex { set; get; }
    public int PageSize { get; set; }
    public int TotalRows { set; get; }
    public IEnumerable<T> Items { set; get; }
  }
}