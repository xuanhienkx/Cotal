using System;
using System.Threading;
using System.Threading.Tasks;
using Cotal.Core.InfacBase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cotal.Core.InfacBase.Uow
{
    public interface IUnitOfWorkBase : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);  
        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>();
        TRepository GetCustomRepository<TRepository>();
    }
}