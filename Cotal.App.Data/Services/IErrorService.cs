using System;
using Cotal.App.Model.Models;
using Cotal.Core.InfacBase.Entities;
using Cotal.Core.InfacBase.Repositories;
using Cotal.Core.InfacBase.Uow;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Cotal.App.Data.Services
{
    public interface IErrorService
    {
        void Create(Error error);

        int Save();
    }

    public abstract class ServiceBace<T,TKey> where T: EntityBase<TKey> where TKey : IEquatable<TKey>
    {
        private readonly IUowProvider _uowProvider;   

        protected ServiceBace(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
            UnitOfWork = _uowProvider.CreateUnitOfWork();
            Repository = UnitOfWork.GetRepository<T, TKey>();
        }

        protected IRepository<T, TKey> Repository { get; }  
        protected IUnitOfWork UnitOfWork { get; }
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