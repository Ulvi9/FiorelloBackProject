﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PartialViewHomeWork.Dal;

namespace PartialViewHomeWork
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(_config["ConnectionStrings:DefaultConnection"]);
            });
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(20);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseMvc(routes =>{
                routes.MapRoute(
                name: "areas",
                template: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
                routes.MapRoute("default",
                    "{controller=Home}/{action=Index}/{id?}");
                
            });

        }
    }
}
