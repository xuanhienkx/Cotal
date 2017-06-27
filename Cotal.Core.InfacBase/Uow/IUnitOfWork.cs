using System;
using System.Threading;
using System.Threading.Tasks;
using Cotal.Core.InfacBase.Repositories;

namespace Cotal.Core.InfacBase.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IRepository<TEntity,TKey> GetRepository<TEntity, TKey>();
        TRepository GetCustomRepository<TRepository>();
    }
}
