using System.IO;
using AutoMapper;
using Cotal.App.Business;
using Cotal.App.Data.Contexts;
using Cotal.Core.Identity;
using Cotal.Core.Identity.Data;
using Cotal.Core.Identity.Models;
using Cotal.Core.InfacBase.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Cotal.Web
{
  public partial class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
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


      services.AddDbContext<CotalContex>(
        options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddDataAccess<CotalContex>();

      #endregion

      // Add framework services.
      // services.AddMvc();
      //AddJsonOptions
      services.AddMvc().AddJsonOptions(options =>
      {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;              
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
      });
      // .NET Native DI Abstraction
      RegisterServices(services);
      // Register the Swagger generator, defining one or more Swagger documents
      var pathToDoc = Configuration["Swagger:FileName"];
      // services.AddSwaggerGen();
      services.AddSwaggerGen(options =>
      {
        options.SwaggerDoc("v1",
          new Info
          {
            Title = "Cotal API",
            Version = "v1",
            Description = "A Cotal ASP.NET Core Web API",
            TermsOfService = "None",
            Contact = new Contact { Name = "Xuanphap", Email = "", Url = "https://vics.com.vn" },
            License = new License { Name = "Use under LIM", Url = "https://vics.com.vn" }
          }
        );

        var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, pathToDoc);
        options.IncludeXmlComments(filePath);
        options.DescribeAllEnumsAsStrings();
      });
    }

    private static void RegisterServices(IServiceCollection services)
    {
      services.AddAutoMapper();
      // Application
      // services.AddSingleton(AutoMapperConfig.RegisterMappings());
      services.AddSingleton(Mapper.Configuration);
      services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
      // Adding dependencies from another layers (isolated from Presentation)
      IdentityInjectorBootStrapper.RegisterServices(services);
      CotalInjectorBootStrapper.RegisterServices(services);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      ConfigureAuth(app);

      app.Use(async (context, next) =>
      {
        await next();

        // If there's no available file and the request doesn't contain an extension, we're probably trying to access a page.
        // Rewrite request to use app root
        if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) &&
            !context.Request.Path.Value.StartsWith("/api"))
        {
          context.Request.Path = "/index.html";
          context.Response.StatusCode = 200; // Make sure we update the status code, otherwise it returns 404
          await next();
        }
      });


      app.UseMvc(routes =>
      {
        routes.MapRoute(
          "default",
          "api/{controller=Home}/{action=Index}/{id?}");
        routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
      });
      // app.UseMvcWithDefaultRoute();
      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseSwagger(c => { c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value); });
      // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cotal API V1"); });
    }
  }
}
