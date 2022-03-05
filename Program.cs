using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using LibApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace LibApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                SeedData.Initialize(services);

                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<Role>>();

                SeedData.SeedRolesAndUsers(userManager, roleManager);
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
