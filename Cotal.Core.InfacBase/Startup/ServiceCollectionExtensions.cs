using System;
using Cotal.Core.InfacBase.Context;
using Cotal.Core.InfacBase.Paging;
using Cotal.Core.InfacBase.Repositories;
using Cotal.Core.InfacBase.Uow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cotal.Core.InfacBase.Startup
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddDataAccess<TEntityContext>(this IServiceCollection services) where TEntityContext : EntityContextBase<TEntityContext>
        {
            RegisterDataAccess<TEntityContext>(services);
            return services;
        }

        private static void RegisterDataAccess<TEntityContext>(IServiceCollection services) where TEntityContext : EntityContextBase<TEntityContext>
        {
            services.TryAddSingleton<IUowProvider, UowProvider>();
            services.TryAddTransient<IEntityContext, TEntityContext>();
            services.TryAddTransient(typeof(IRepository<,>), typeof(GenericEntityRepository<,>));
            services.TryAddTransient(typeof(IDataPager<,>), typeof(DataPager<,>));
        }

        private static void ValidateMandatoryField(string field, string fieldName)
        {
            if ( field == null ) throw new ArgumentNullException(fieldName, $"{fieldName} cannot be null.");
            if ( field.Trim() == String.Empty ) throw new ArgumentException($"{fieldName} cannot be empty.", fieldName);
        }

        private static void ValidateMandatoryField(object field, string fieldName)
        {
            if ( field == null ) throw new ArgumentNullException(fieldName, $"{fieldName} cannot be null.");
        }
    }
}
