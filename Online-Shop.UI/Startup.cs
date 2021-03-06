using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Online_Shop.Database;
using Online_Shop.Domain.Infrastructure;
using Online_Shop.UI.Infrastructure;
using Stripe;
using System;
using FluentValidation;
using Online_Shop.UI.Validators;

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

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Accounts/Login";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
                options.AddPolicy("Manager", policy => policy.RequireClaim("Role", "Manager"));
                // Admin has access to same thing that manager has
                options.AddPolicy("Manager", policy => policy.
                    RequireAssertion(context => 
                    context.User.HasClaim("Role", "Manager") 
                    || context.User.HasClaim("Role", "Admin")));
            });

            services.AddSession(option =>
            {
                option.Cookie.Name = "Cart";
                option.Cookie.MaxAge = TimeSpan.FromMinutes(20);
            });

            services.AddMvc(option => option.EnableEndpointRouting = false).
                AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Admin");
                    options.Conventions.AuthorizePage("/Admin/ConfigureUsers", "Admin");
                });

            services.AddValidatorsFromAssemblyContaining<AddCustomerInformationRequestValidator>();

            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];


            services.AddApplicationInfrastucture();
            services.AddApplicationServices();
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

            app.UseAuthentication();
            
            app.UseSession();

            app.UseMvcWithDefaultRoute();
        }
    }
}
