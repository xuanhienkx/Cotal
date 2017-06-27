using System;
using Cotal.Core.InfacBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cotal.Core.InfacBase.Repositories
{
    public class GenericEntityRepository<TEntity,TKey> : EntityRepositoryBase<DbContext, TEntity,TKey> where TEntity : EntityBase<TKey>, new() where TKey : IEquatable<TKey>
    {
		public GenericEntityRepository(ILogger<DataAccess> logger) : base(logger, null)
		{ }
	}
}