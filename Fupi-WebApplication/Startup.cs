using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fupi_WebApplication.DAL;
using Fupi_WebApplication.Repositories;
using Fupi_WebApplication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rollbar;
using Rollbar.NetCore.AspNet;
using Rollbar.Telemetry;

namespace Fupi_WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddControllersWithViews();

            services.AddDbContext<FupiAppContext>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IWrapper, RepoWrapper>();
            services.AddScoped<FupiKeyService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddRollbarLogger(loggerOptions =>
            {
                loggerOptions.Filter =
                  (loggerName, loglevel) => loglevel >= LogLevel.Trace;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Fupi/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRollbarMiddleware();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Fupi}/{action=Index}");
            });
        }
    }
}
