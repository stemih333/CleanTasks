using TodoTasks.RazorGUI.Interfaces;
using TodoTasks.RazorGUI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using TodoTasks.DataAccess.Auth;
using TodoTasks.OpenIdConnectAuth;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Security.Claims;
using TodoTasks.Common.Extensions;
using TodoTasks.FileSaver;
using TodoTasks.Application;

namespace TodoTasks.RazorGUI
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

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddTransient<IAppSessionHandler, AppSessionHandler>();
            services.AddHttpContextAccessor();


            //Environment specific setup
            if (Environment.IsDevelopment())
            {
                //Dev disable all authentication
                SetupDevSecurity(services);
                services.AddMvc(opts =>
                    {
                        opts.Filters.Add(new AllowAnonymousFilter());
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

                services.AddHttpClient<ITodoAreaApiClient, TodoAreaApiClient>((c) =>
                {                  
                    c.BaseAddress = new Uri(Configuration["ApiUrl"]);
                });

                services.AddHttpClient<ITodoApiClient, TodoApiClient>((c) =>
                {                    
                    c.BaseAddress = new Uri(Configuration["ApiUrl"]);
                });
            }
            else
            {
                SetupSecurity(services);
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

                // HttpClient setup
                services.AddHttpClient<ITodoAreaApiClient, TodoAreaApiClient>(async (c) =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                    var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");

                    c.BaseAddress = new Uri(Configuration["ApiUrl"]);
                    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                });

                services.AddHttpClient<ITodoApiClient, TodoApiClient>(async (c) =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                    var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");

                    c.BaseAddress = new Uri(Configuration["ApiUrl"]);
                    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                });
            }
            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                app.Use(async (context, next) =>
                {
                    var identity = new DevIdentity(Users.AdminClaims);
                    identity.AddClaim(new Claim(ClaimTypes.Name, Users.Admin.UserName));
                    context.User = new ClaimsPrincipal(identity);
                    
                    await next();
                });

            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc();
        }

        private void SetupDevSecurity(IServiceCollection services)
        {
            IdentityDbStartup.ConfigureDevIdentityServices(services);
            OpenIdConnectStartup.ConfigureAuthorizationServices(services);
        }

        private void SetupSecurity(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("TodoDbContext");

            IdentityDbStartup.ConfigureIdentityServices(services, connectionString);
            OpenIdConnectStartup.ConfigureOpenIdServices(services, Configuration, Environment.IsLocalTest());
            OpenIdConnectStartup.ConfigureAuthorizationServices(services);
        }
    } 
}
