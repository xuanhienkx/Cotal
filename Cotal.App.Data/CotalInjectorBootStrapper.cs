using System;
using System.Reflection;
using Cotal.App.Data.Contexts;
using Cotal.App.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cotal.App.Data
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class DependencyAttribute : Attribute
    {
        public ServiceLifetime DependencyType { get; set; }

        public Type ServiceType { get; set; }

        protected DependencyAttribute(ServiceLifetime dependencyType)
        {
            DependencyType = dependencyType;
        }

        public ServiceDescriptor BuildServiceDescriptor(TypeInfo type)
        {
            var serviceType = ServiceType ?? type.AsType();
            return new ServiceDescriptor(serviceType, type.AsType(), DependencyType);
        }
    }

    public class CotalInjectorBootStrapper
    {
         
        public static void RegisterServices(IServiceCollection services)
        {

            services.AddScoped<IDbCotalInitializer, DbCotalInitializer>();
            // Infra - Identity Services                          
             services.AddTransient<IErrorService, ErrorService>();
        }
         
    }     

}