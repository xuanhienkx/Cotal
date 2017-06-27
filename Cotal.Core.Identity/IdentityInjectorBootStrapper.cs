using Cotal.Core.Domain.Interfaces;
using Cotal.Core.Identity.Authorization;
using Cotal.Core.Identity.Data;
using Cotal.Core.Identity.Models;
using Cotal.Core.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cotal.Core.Identity
{
    public class IdentityInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>(); ;

            // Application
            //services.AddSingleton(Mapper.Configuration);
            //services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            

            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();
            services.AddTransient<IUserService, UserService>();

            // Infra - Identity
            services.AddScoped<IUser, CotalUser>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            // Infra - Bus                                
        }

    }
}