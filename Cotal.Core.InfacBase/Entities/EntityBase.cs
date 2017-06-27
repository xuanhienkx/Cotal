using System;
using System.ComponentModel.DataAnnotations;

namespace Cotal.Core.InfacBase.Entities
{
    public class EntityBase<TKey> where TKey : IEquatable<TKey>
    {
        // This is the base class for all entities.
        // The DataAccess repositories have this class as constraint for entities that are persisted in the database.

        [Key]
        public virtual TKey Id { get; set; }
    }
}
