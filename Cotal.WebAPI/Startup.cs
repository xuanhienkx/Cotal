using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cotal.App.Data.Contexts;
using Cotal.Core.Identity;
using Cotal.Core.Identity.Authorization;
using Cotal.Core.Identity.Data;
using Cotal.Core.Identity.Models;
using Cotal.Core.InfacBase.Startup;
using Cotal.WebAPI.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace Cotal.WebAPI
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);


            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
            // Add framework services.
            services.AddMvc();
            var pathToDoc = Configuration["Swagger:FileName"];
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Cotal API",
                        Version = "v1",
                        Description = "A simple api to search using geo location in Elasticsearch",
                        TermsOfService = "None"
                    }
                );

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, pathToDoc);
                options.IncludeXmlComments(filePath);
                options.DescribeAllEnumsAsStrings();
            });

            // services.AddScoped<ISearchProvider, SearchProvider>();
            services.AddAutoMapper();
            // Enable the use of an [Authorize("Bearer")] attribute on methods and classes to protect.
            //services.AddAuthorization(auth =>
            //{
            //    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            //        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            //        .RequireAuthenticatedUser().Build());
            //});
            //services.AddAuthorization(options =>
            //{
            //    //  options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.AddPolicy("CanWriteCustomerData", policy => policy.Requirements.Add(new ClaimRequirement("Customers", "Write")));
            //    options.AddPolicy("CanRemoveCustomerData", policy => policy.Requirements.Add(new ClaimRequirement("Customers", "Remove")));
            //});
            // .NET Native DI Abstraction
            RegisterServices(services);
            services.AddScoped<IDbCotalInitializer, DbCotalInitializer>();
        }                                                                                           

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IHttpContextAccessor accessor, IDbInitializer dbInitializer, IDbCotalInitializer cotalInitializer)
        {
               
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //  app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseFacebookAuthentication(new FacebookOptions()
            {
                AppId = "SetYourDataHere",
                AppSecret = "SetYourDataHere"
            });
            dbInitializer.Initialize();
            cotalInitializer.Initialize();

            // Add JWT generation endpoint:  
            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "localhost:5838",
                    ValidAudience = "localhost:5838",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = TokenAuthOption.Key,                                                
                    ValidateLifetime = true
                }
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
            });
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);
            });

            app.UseSwaggerUi(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            });
        }
        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            IdentityInjectorBootStrapper.RegisterServices(services);
        }
    }
}
