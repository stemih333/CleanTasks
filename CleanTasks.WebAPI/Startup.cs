using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using CleanTasks.Application.Attachment.Commands.CreateAttachment;
using CleanTasks.Application.Infrastructure;
using FluentValidation.AspNetCore;
using CleanTasks.Application.TodoArea.Commands;
using CleanTasks.WebAPI.Filters;
using CleanTasks.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;
using CleanTasks.WebAPI.Extensions;
using CleanTasks.Common.Constants;

namespace CleanTasks.WebAPI
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var loggerConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration);

            services.AddSerilogServices(loggerConfig);

            services.AddMediatR(typeof(CreateAttachmentHandler).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuditTrailBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            services.AddMvc(_ => {
                _.Filters.Add<GlobalExceptionFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddFluentValidation(_ => {
                _.LocalizationEnabled = false;
                _.RegisterValidatorsFromAssemblyContaining<CreateTodoAreaValidation>();
            });

            services.AddDbContext<TodoDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("TodoDbContext"));
            }, ServiceLifetime.Transient);               
           
            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireClaim(AuthConstants.PermissionType, AuthConstants.UserAdminPermission));
                options.AddPolicy("UserPolicy", policy =>
                    policy.RequireClaim(AuthConstants.PermissionType, AuthConstants.UserPermission));
                options.AddPolicy("AllUserPolicy", policy =>
                    policy.RequireAssertion(
                        assert =>
                            assert.User.HasClaim(AuthConstants.PermissionType, AuthConstants.UserAdminPermission) ||
                            assert.User.HasClaim(AuthConstants.PermissionType, AuthConstants.UserPermission)));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsProduction())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
