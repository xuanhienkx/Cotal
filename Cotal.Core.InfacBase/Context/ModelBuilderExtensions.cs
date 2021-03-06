﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cotal.Core.InfacBase.Context
{
    public static class ModelBuilderExtensions
    {
        public static void LowerCaseTablesAndFields(this ModelBuilder modelBuilder)
        {
            foreach ( var entityType in modelBuilder.Model.GetEntityTypes() )
            {
                // Skip shadow types
                if ( entityType.ClrType == null )
                {
                    continue;
                }

                // Set the table name to the (simple) name of the CLR type and lowercase it
                entityType.Relational().TableName = entityType.ClrType.Name.ToLower();

                // Lowercase all properties
                foreach ( var property in entityType.GetProperties() )
                {
                    property.Relational().ColumnName = property.Name.ToLower();
                }
            }
        }

        public static void DisableCascadingDeletes(this ModelBuilder modelBuilder)
        {
            foreach ( var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()) )
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
