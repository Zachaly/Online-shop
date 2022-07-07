using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Online_shop.DataBase;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Online_Shop.UI
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
            services.AddRazorPages();
            
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["DefaultConnection"]);
            });
            services.AddSession(option =>
            {
                option.Cookie.Name = "Cart";
                option.Cookie.MaxAge = TimeSpan.FromDays(2);
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);

            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            }); 

            app.UseMvc();
        }
    }
}
