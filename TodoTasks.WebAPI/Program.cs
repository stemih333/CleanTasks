using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TodoTasks.DataAccess;
using TodoTasks.DataAccess.Auth;

namespace TodoTasks.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;               
                var env = services.GetService<IHostingEnvironment>();

                if(env.IsDevelopment())
                {
                    TestData.Init(services);
                    AuthStartup.SeedAsync(services).Wait();
                }              
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
