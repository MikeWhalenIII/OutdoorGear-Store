using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OutdoorGear_Store.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.AspNetCore.Http;

namespace OutdoorGear_Store
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IProductRepository, EFProductRepository>();

            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp)); 
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews();

            // Sets up the in-memory data store for sessions
            services.AddMemoryCache();
            // Registers the services used to access session data
            services.AddSession();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Allows the session system to automatically associate requests with sessions when they arrive from the client
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Shows the specified page of items from the specified category
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{category}/Page{page:int}",
                    defaults: new { controller = "Product", action = "List" }
                );

                // Lists the specified page (in this case, page 2), showing items from all categories.
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "Page{page:int}",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                );

                // Shows the first page of items from a specific category
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{category}",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                );

                // Lists the first page of products from all categories
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                );

                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{controller}/{action}/{id?}"
                );
            });
        }
    }
}
