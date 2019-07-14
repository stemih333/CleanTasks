using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TodoTasks.DataAccess.Auth;

namespace TodoTasks.RazorGUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var env = services.GetService<IHostingEnvironment>();

                if (env.IsDevelopment())
                {
                    AuthStartup.SeedIdentityUser(services).Wait();
                    AuthStartup.SeedIdentityAdmin(services).Wait();
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
