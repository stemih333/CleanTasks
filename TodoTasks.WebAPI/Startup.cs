﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using TodoTasks.WebAPI.Filters;
using TodoTasks.DataAccess;
using TodoTasks.Application;
using TodoTasks.Application.TodoArea.Commands;
using TodoTasks.Logging;
using TodoTasks.DataAccess.Auth;

namespace TodoTasks.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }
        public IHostingEnvironment Environment;
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ApplicationStartup.ConfigureServices(services);
            FileSaver.FileSaver.ConfigureServices(services);
            AuthStartup.ConfigureJwtApiServices(services, Configuration);
            AuthStartup.ConfigureAuthorizationServices(services);

            var connectionString = Configuration.GetConnectionString("TodoDbContext");

            if(Environment.IsDevelopment())
            {              
                DataAccessStartup.ConfigureDevServices(services);
                AuthStartup.ConfigureDevIdentityServices(services);
            }
            else
            {
                DataAccessStartup.ConfigureServices(services, connectionString);
                AuthStartup.ConfigureIdentityServices(services, connectionString);
            }

            SerilogLogging.ConfigureServices(services, Configuration);

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

            services.AddAuthorization();
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
        }
    }
}
