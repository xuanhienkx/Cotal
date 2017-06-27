using Microsoft.EntityFrameworkCore;

namespace Cotal.Core.InfacBase.Repositories
{
    public interface IRepositoryInjection
    {
        IRepositoryInjection SetContext(DbContext context);
    }
}