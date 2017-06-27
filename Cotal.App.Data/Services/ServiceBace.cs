using System;
using Cotal.Core.InfacBase.Entities;
using Cotal.Core.InfacBase.Repositories;
using Cotal.Core.InfacBase.Uow;

namespace Cotal.App.Data.Services
{
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
}