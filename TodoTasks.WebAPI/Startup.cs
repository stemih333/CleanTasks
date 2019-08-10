using Microsoft.AspNetCore.Builder;
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
using TodoTasks.OpenIdConnectAuth;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Security.Claims;
using TodoTasks.Common.Extensions;
using TodoTasks.FileSaver;

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
            
            var connectionString = Configuration.GetConnectionString("TodoDbContext");

            if (Environment.IsDevelopment())
            {
                LocalFileSaver.ConfigureServices(services);
                DataAccessStartup.ConfigureDevServices(services);
                SetupDevSecurity(services);
            }
            else
            {
                if (Environment.IsLocalTest()) LocalFileSaver.ConfigureServices(services);
                else AzureBlobFileSaver.ConfigureServices(services);

                DataAccessStartup.ConfigureServices(services, connectionString);
                SetupSecurity(services, connectionString);
            }

            SerilogLogging.ConfigureServices(services, Configuration);

            services.AddMvc(_ => {
                _.Filters.Add<GlobalExceptionFilter>();
                if (Environment.IsDevelopment()) _.Filters.Add(new AllowAnonymousFilter());
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
            if (env.IsDevelopment())
            {
                app.Use(async (context, next) =>
                {
                    var identity = new DevIdentity(Users.AdminClaims);
                    identity.AddClaim(new Claim(ClaimTypes.Name, Users.Admin.UserName));
                    context.User = new ClaimsPrincipal(identity);

                    await next();
                });

                app.UseStaticFiles(LocalFileSaver.GetStaticFileOptions());
            }
            if (env.IsProduction())
            {
                app.UseHsts();
            }
            
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        // Dev environment uses in memory Identity DB and no authentication service
        private void SetupDevSecurity(IServiceCollection services)
        {
            IdentityDbStartup.ConfigureDevIdentityServices(services);
            OpenIdConnectStartup.ConfigureAuthorizationServices(services);
        }

        private void SetupSecurity(IServiceCollection services, string connectionString)
        {
            IdentityDbStartup.ConfigureIdentityServices(services, connectionString);
            OpenIdConnectStartup.ConfigureJwtApiServices(services, Configuration);
            OpenIdConnectStartup.ConfigureAuthorizationServices(services);
        }
    }
}
