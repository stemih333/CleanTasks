using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
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
                    Log.Information("Seeding Todo test data.");
                    TestData.Init(services);
                    Log.Information("Seeding Identity user.");
                }
                else
                {
                    Log.Information("Running Identity migrations.");
                    IdentityDbStartup.RunIdentityMigrations(services);
                    Log.Information("Running TodoDb migrations.");
                    DataAccessStartup.RunTodoDbMigrations(services);
                }
                Log.Information("Seeding Identity admin.");
                IdentityDbStartup.SeedIdentityAdmin(services).Wait();
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
