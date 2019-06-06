using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace TodoTasks.Logging
{
    public static class SerilogLogging
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            services.AddLogging((builder) =>
            {
                builder.AddSerilog(dispose: true);
            });
        }
    }
}
