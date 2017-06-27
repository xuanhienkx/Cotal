using System;
using System.Linq;
using System.Threading.Tasks;
using Cotal.Core.InfacBase.Query;

namespace Cotal.Core.InfacBase.Paging
{
    public interface IDataPager<TEntity,TKey> where TKey : IEquatable<TKey>
    {
        DataPage<TEntity, TKey> Get(int pageNumber, int pageLength, OrderBy<TEntity> orderby = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        DataPage<TEntity, TKey> Query(int pageNumber, int pageLength, Filter<TEntity> filter, OrderBy<TEntity> orderby = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<DataPage<TEntity, TKey>> GetAsync(int pageNumber, int pageLength, OrderBy<TEntity> orderby = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
        Task<DataPage<TEntity, TKey>> QueryAsync(int pageNumber, int pageLength, Filter<TEntity> filter, OrderBy<TEntity> orderby = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
    }
}
