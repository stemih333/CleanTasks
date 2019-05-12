using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTodoTasks.DataAccess
{
    public class DataAccessStartup
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TodoDbContext>(options => {
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Transient);
        }
    }
}
