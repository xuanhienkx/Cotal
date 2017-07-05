using System;
using System.Reflection;
using Cotal.App.Business.Services;
using Cotal.App.Data.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Cotal.App.Business
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public abstract class DependencyAttribute : Attribute
  {
    protected DependencyAttribute(ServiceLifetime dependencyType)
    {
      DependencyType = dependencyType;
    }

    public ServiceLifetime DependencyType { get; set; }

    public Type ServiceType { get; set; }

    public ServiceDescriptor BuildServiceDescriptor(TypeInfo type)
    {
      var serviceType = ServiceType ?? type.AsType();
      return new ServiceDescriptor(serviceType, type.AsType(), DependencyType);
    }
  }

  public class CotalInjectorBootStrapper
  {
    // private readonly Assembly _assembly = typeof(IServiceBase).Assembly;

    public static void RegisterServices(IServiceCollection services)
    {
     //  services.AddTransient(typeof(IServiceBase<,>), typeof(GenericEntityService<,>));
      services.AddScoped<IDbCotalInitializer, DbCotalInitializer>();

      //// Infra - Identity Services                          
      services.AddTransient<IErrorService, ErrorService>();
      services.AddTransient<IFunctionService, FunctionService>();
      services.AddTransient<IAnnouncementService, AnnouncementService>();
      services.AddTransient<IPermissionService, PermissionService>();
      services.AddTransient<ILoginService, LoginService>();
      services.AddTransient<IAppUserService, AppUserService>();                  
      services.AddTransient<IAppRoleService, AppRoleService>();                  
    }
  }
}