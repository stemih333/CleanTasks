using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace CleanTodoTasks.WebAPI.Extensions
{
    public static class SerilogExtensions
    {
        public static IServiceCollection AddSerilogServices(this IServiceCollection services, LoggerConfiguration configuration)
        {
            Log.Logger = configuration.CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            return services.AddSingleton(Log.Logger);
        }
    }
}
