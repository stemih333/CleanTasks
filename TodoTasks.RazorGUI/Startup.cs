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

namespace TodoTasks.RazorGUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            AuthStartup.ConfigureIdentity(services, Configuration.GetConnectionString("TodoDbContext"));
            AuthStartup.ConfigureOpenIdGui(services, Configuration);
            AuthStartup.ConfigureAuthorization(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<IAppSessionHandler, AppSessionHandler>();

            services.AddHttpContextAccessor();

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

            
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = "Cookie";
            //    options.DefaultChallengeScheme = "oidc";
            //})
            //.AddCookie("Cookie", opts => {
            //    opts.ExpireTimeSpan = TimeSpan.FromHours(1);
            //})
            //.AddOpenIdConnect("oidc", options =>
            //{
            //    options.SignInScheme = "Cookie";
            //    options.Authority = config.AuthUrl;
            //    options.ClientId = config.ClientId;
            //    options.ClientSecret = config.ClientSecret;
            //    options.ResponseType = "code id_token";
            //    options.SaveTokens = true;
            //    options.GetClaimsFromUserInfoEndpoint = true;
            //    options.Scope.Add("openid");
            //    options.Scope.Add("profile");
            //    options.Scope.Add("email");
            //    options.Scope.Add("WebAPI");
            //    options.Scope.Add("offline_access");
            //    options.Scope.Add(AuthConstants.PermissionType);
            //    options.Scope.Add(PermissionTypes.TodoAreaPermission);
            //    options.ClaimActions.Add(new JsonKeyArrayClaimAction(AuthConstants.PermissionType, AuthConstants.PermissionType, AuthConstants.PermissionType));
            //    options.ClaimActions.Add(new JsonKeyArrayClaimAction(PermissionTypes.TodoAreaPermission, PermissionTypes.TodoAreaPermission, PermissionTypes.TodoAreaPermission));
            //    options.Events.OnRemoteFailure = ctx =>
            //    {
            //        if(!string.IsNullOrEmpty(ctx.Failure.Message) && ctx.Failure.Message.Contains("access_denied"))
            //        {
            //            ctx.Response.Redirect("/");
            //            ctx.HandleResponse();
            //        }
            //        return Task.CompletedTask;
            //    };

            //    options.Events.OnUserInformationReceived = ctx =>
            //    {
            //        return Task.CompletedTask;
            //    };
            //});
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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
    }

    
}
