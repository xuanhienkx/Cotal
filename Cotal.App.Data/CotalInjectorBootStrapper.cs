using Cotal.App.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cotal.App.Data
{
    public class CotalInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Infra - Identity Services
            services.AddTransient<IErrorService, ErrorService>();
        }
    }
}