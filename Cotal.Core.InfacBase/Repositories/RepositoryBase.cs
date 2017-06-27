using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cotal.Core.InfacBase.Repositories
{
    public abstract class RepositoryBase<TContext,TKey> : IRepositoryInjection where TContext : DbContext where TKey : IEquatable<TKey>
    {
        protected RepositoryBase(ILogger<DataAccess> logger, TContext context)
        {
            this.Logger = logger;
            this.Context = context;
        }

        protected ILogger Logger { get; private set; }
        protected TContext Context { get; private set; }

        public IRepositoryInjection SetContext(DbContext context)
        {
            this.Context = (TContext)context;
            return this;
        }



        //IRepositoryInjection<TContext> IRepositoryInjection<TContext>.SetContext(TContext context)
        //{
        //    this.Context = context;
        //    return this;
        //}
    }
}
