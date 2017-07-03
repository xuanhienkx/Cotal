
using Microsoft.EntityFrameworkCore;

namespace Cotal.Core.InfacBase.Uow
{
    public interface IUowProvider
    {
        IUnitOfWork CreateUnitOfWork(bool trackChanges = true, bool enableLogging = false);
        IUnitOfWork CreateUnitOfWork<TEntityContext>(bool trackChanges = true, bool enableLogging = false) where TEntityContext : DbContext;
        DbContext Context { get; }
    }
}
