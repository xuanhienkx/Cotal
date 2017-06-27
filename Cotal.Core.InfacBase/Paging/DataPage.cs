using System;
using System.Collections.Generic;

namespace Cotal.Core.InfacBase.Paging
{
    public class DataPage<TEntity, TKey> where TKey : IEquatable<TKey>
    {
        public IEnumerable<TEntity> Data { get; set; }
        public long TotalEntityCount { get; set; }

        public int PageNumber { get; set; }
        public int PageLength { get; set; }

        public int TotalPageCount {
            get
            {
                return Convert.ToInt32(Math.Ceiling((decimal)TotalEntityCount / PageLength));
            }
        }
    }
}
