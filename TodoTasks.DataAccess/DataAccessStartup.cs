using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.DataAccess
{
    public class DataAccessStartup
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ITodoDbContext, TodoDbContext>(options => {
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Transient);
        }

        public static void ConfigureDevServices(IServiceCollection services)
        {
            services.AddDbContext<ITodoDbContext, TodoDbContext>(options => options.UseInMemoryDatabase(databaseName: "TodoTasks"), ServiceLifetime.Transient);
        }
    }
}
