using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Online_shop.Database;
using System;
using System.Linq;
using System.Security.Claims;

namespace Online_Shop.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            try
            {
                // Creating default identities if they are not present
                using (var scope = host.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                    context.Database.EnsureCreated();

                    if (!context.Users.Any())
                    {
                        var admin = new IdentityUser
                        {
                            UserName = "Admin"
                        };

                        var manager = new IdentityUser
                        {
                            UserName = "Manager"
                        };

                        userManager.CreateAsync(admin, "zaq1@WSx").GetAwaiter().GetResult();
                        userManager.CreateAsync(manager, "zaq1@WSX").GetAwaiter().GetResult();

                        var adminClaim = new Claim("Role", "Admin");
                        var managerClaim = new Claim("Role", "Manager");

                        userManager.AddClaimAsync(admin, adminClaim).GetAwaiter().GetResult();
                        userManager.AddClaimAsync(manager, managerClaim).GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
