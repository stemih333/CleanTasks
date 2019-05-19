using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using TodoTasks.WebAPI.Filters;
using Microsoft.Extensions.Logging;
using TodoTasks.DataAccess;
using TodoTasks.Application;
using TodoTasks.Application.TodoArea.Commands;

namespace TodoTasks.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env, ILogger<Startup> logger)
        {
            Configuration = configuration;
            Environment = env;
            _logger = logger;
        }
        private readonly ILogger _logger;

        public IHostingEnvironment Environment;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ApplicationStartup.ConfigureServices(services);
            if(Environment.IsDevelopment())
            {
                DataAccessStartup.ConfigureDevServices(services);
            }
            else
            {
                DataAccessStartup.ConfigureServices(services, Configuration.GetConnectionString("TodoDbContext"));
            }

            services.AddMvc(_ => {
                _.Filters.Add<GlobalExceptionFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddFluentValidation(_ => {
                _.LocalizationEnabled = false;
                _.RegisterValidatorsFromAssemblyContaining<CreateTodoAreaValidation>();
            });
                     
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5000";
                    options.RequireHttpsMetadata = true;
                    options.Audience = "WebAPI";
                });


            services.AddAuthorization();
            _logger.LogInformation("Services configured.");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsProduction())
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
            _logger.LogInformation("Http pipeline configured.");
        }
    }
}
