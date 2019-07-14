using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using TodoTasks.Application.Interfaces;

namespace TodoTasks.DataAccess
{
    public static class DataAccessStartup
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ITodoDbContext, TodoDbContext>(options => {
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Transient);
        }

        public static void ConfigureDevServices(IServiceCollection services)
        {
            services.AddDbContext<ITodoDbContext, TodoDbContext>(options => options.UseInMemoryDatabase("TodoDb"), ServiceLifetime.Transient);
        }

        public static void RunTodoDbMigrations(IServiceProvider services)
        {
            var context = services.GetService<ITodoDbContext>() as TodoDbContext;

            context.Database.Migrate();
        }


        public static bool DatabaseExists(IServiceProvider services)
        {
            var context = services.GetService<ITodoDbContext>() as TodoDbContext;

            return (context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();
        }
    }
}
