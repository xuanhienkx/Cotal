using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Uow;

namespace Cotal.App.Business.Services
{
  public interface IErrorService
  {
    Error Create(Error error);

    int Save();
  }

  public class ErrorService : ServiceBace<Error, int>, IErrorService
  {
    public ErrorService(IUowProvider uowProvider) : base(uowProvider)
    {
    }

    public Error Create(Error error)
    {
      return Repository.Add(error);
    }

    public int Save()
    {
      return UnitOfWork.SaveChanges();
    }
  }
}