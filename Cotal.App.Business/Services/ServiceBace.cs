using System;
using Cotal.App.Data.Contexts;
using Cotal.Core.InfacBase.Paging;
using Cotal.Core.InfacBase.Repositories;
using Cotal.Core.InfacBase.Uow;

namespace Cotal.App.Business.Services
{
  public interface IServiceBase<T, TKey> 
  {
  }

  public abstract class ServiceBace<T, TKey> : IServiceBase<T, TKey> where TKey : IEquatable<TKey>
  {
    private readonly IUowProvider _uowProvider;    

    protected ServiceBace(IUowProvider uowProvider)
    {
      _uowProvider = uowProvider;
      UnitOfWork = _uowProvider.CreateUnitOfWork();
      DB = (CotalContex) _uowProvider.Context;
      Repository = UnitOfWork.GetRepository<T, TKey>();     
    }

    protected CotalContex DB { get; }

    protected IRepository<T, TKey> Repository { get; }
    protected IUnitOfWork UnitOfWork { get; }
  }

  public class GenericEntityService<T, TKey> : ServiceBace<T, TKey> where TKey : IEquatable<TKey>
  {
    public GenericEntityService(IUowProvider uowProvider) : base(uowProvider)
    {
    }
  }
}