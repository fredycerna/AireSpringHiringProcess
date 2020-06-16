using AireSpring.Data.Core;
using AireSpring.Domain.Infraestructure;
using AireSpring.Domain.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace AireSpring.TelerikWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services
                .AddRazorPages()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            /* Injection of Telerik framework.
               Automapper
               UnitOfWork
               EmployeService
             */

            services.AddKendo();
            services.AddAutoMapper(options => options.AddProfile<MappingProfile>(), typeof(Startup));
            services.AddScoped<IUnitOfWork, UnitOfWork>(u => new UnitOfWork(Configuration.GetConnectionString("AireSpringDb")));
            services.AddScoped<IEmployeeService, EmployeeService>();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

        }
    }
}
