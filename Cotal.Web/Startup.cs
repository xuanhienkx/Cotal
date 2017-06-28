using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cotal.App.Data.Contexts;
using Cotal.Core.Identity;
using Cotal.Core.Identity.Data;
using Cotal.Core.Identity.Models;
using Cotal.Core.InfacBase.Startup;
using Cotal.Web.Authen;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Cotal.Web
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      #region AppContext config

      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddIdentity<AppUser, AppRole>(options =>
        {
          options.Password.RequireDigit = false;
          options.Password.RequiredLength = 4;
          options.Password.RequireLowercase = false;
          options.Password.RequireNonAlphanumeric = false;
          options.Password.RequireUppercase = false;
          options.Cookies.ApplicationCookie.AccessDeniedPath = "/home/access-denied";
        })
        .AddEntityFrameworkStores<ApplicationDbContext, int>()
        .AddDefaultTokenProviders();


      services.AddDbContext<CotalContex>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddDataAccess<CotalContex>();

      #endregion
      // Add framework services.
      services.AddMvc();

      // .NET Native DI Abstraction
      RegisterServices(services);
    }
    private static void RegisterServices(IServiceCollection services)
    {
      // Adding dependencies from another layers (isolated from Presentation)
      IdentityInjectorBootStrapper.RegisterServices(services);
      services.AddScoped<IDbCotalInitializer, DbCotalInitializer>();
    }
    private static readonly string secretKey = "mysupersecret_secretkey!123";
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();
      app.Use(async (context, next) =>
      {
        await next();

        // If there's no available file and the request doesn't contain an extension, we're probably trying to access a page.
        // Rewrite request to use app root
        if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) && !context.Request.Path.Value.StartsWith("/api"))
        {
          context.Request.Path = "/index.html";
          context.Response.StatusCode = 200; // Make sure we update the status code, otherwise it returns 404
          await next();
        }
      });

      app.UseDefaultFiles();
      app.UseStaticFiles();
      // Add JWT generation endpoint:
      var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
      var options = new TokenProviderOptions
      {
        Audience = "ExampleAudience",
        Issuer = "ExampleIssuer",
        SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
      };

      app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));

      app.UseMvc();
      
    }
                                                                
  }
}
