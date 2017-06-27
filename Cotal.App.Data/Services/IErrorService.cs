using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Uow;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Cotal.App.Data.Services
{
    public interface IErrorService
    {
        void Create(Error error);

        int Save();
    }

    public class ErrorService : ServiceBace<Error, int>, IErrorService
    {        
        public ErrorService(IUowProvider uowProvider) : base(uowProvider)
        {
        }

        public void Create(Error error)
        {
              Repository.Add(error);
        }

        public int Save()
        {
           return UnitOfWork.SaveChanges();
        }

        
    }
}